using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CryptoBTC.Data;
using CryptoBTC.Models;
using CryptoBTC.Interfaces;
using Microsoft.Extensions.Configuration.UserSecrets;
using CandleAverage = CryptoBTC.Models.CandleAverage;
using Microsoft.AspNetCore.Cors;
using RestSharp;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoBTC.Controllers
{
    public class CryptoController : Controller
    {
        private readonly CryptoContext _context;
        private readonly ICoinProvider _provider;
        private  List<CandlesBTC> _candleBTC;
        private readonly ILogger<CryptoController> _logger;
        public CryptoController(CryptoContext context,ICoinProvider coinProvider, ILogger<CryptoController> logger)
        {
            _context = context;
            _provider = coinProvider;
            _logger = logger;

        }
        /// <summary>
        /// Initial action method
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View();
        }
        /// <summary>
        /// Method to get last 31 days of data
        /// </summary>
        /// <returns></returns>
        public  async Task<JsonResult> GetCoins()
        {
            try
            {
                DateTime today = DateTime.Now;
                DateTime last31DaysDate = today.AddDays(-31);

                #region Region to fetch data from API

                var client = new RestClient("https://trade.cex.io/");
                var request = new RestRequest("api/rest-public/get_candles", Method.Post);
                request.AddHeader("Accept", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json; CHARSET=UTF-8");
                string epochToday = ((today.Ticks - 621355968000000000) / 10000000).ToString() + "000";
                string epochLast31 = ((last31DaysDate.Ticks - 621355968000000000) / 10000000).ToString() + "000";

                var param = new
                {
                    pair = "BTC-USD",
                    fromISO = epochLast31,
                    toISO = epochToday,
                    dataType = "bestAsk",
                    resolution = "1d"
                };
                request.AddObject(param);


                var response = await client.ExecutePostAsync(request);
                #endregion
                if (response.IsSuccessful)
                {
                    var data = JsonConvert.DeserializeObject<List<CandlesBTC>>(response.Content);
                    _candleBTC = data.FindAll(c => c.TimestampISO.Date >= last31DaysDate && c.TimestampISO.Date <= today);
                    var lastUpdate = _context.UpdateTracker.OrderByDescending(entity => entity.LasteUpdated).FirstOrDefault();
                    GetLastUpdated(today);
                    return Json(new { _candleBTC, lu = TempData["LastUpdated"] });
                }
                else
                {
                    _candleBTC = await _provider.GetCoinCandles(last31DaysDate, today);
                    GetLastUpdated(today);
                    return Json(new { _candleBTC, lu = TempData["LastUpdated"] });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(DateTime.Now.ToShortTimeString() + "Method - " + "GetCoins ::" + "Message - " + ex.Message);
                return Json(null);
            }
        }
        /// <summary>
        /// Get Last Updated Date with Time
        /// </summary>
        /// <param name="tdy"></param>
        /// <returns></returns>
        private async Task GetLastUpdated(DateTime tdy)
        {
            try { 
            var lastUpdate = _context.UpdateTracker.OrderByDescending(entity => entity.LasteUpdated).FirstOrDefault();

                if (lastUpdate == null)
                {
                    _logger.LogInformation(DateTime.Now.ToShortTimeString() + "Method - " + "Index() ::" + "Message - " + "last updated date is not available");
                    UpdateTracker model = new()
                    {
                        LasteUpdated = tdy
                    };
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    TempData["LastUpdated"] = tdy;
                    _logger.LogInformation(DateTime.Now.ToShortTimeString() + "Method - " + "Index() ::" + "Message - " + "last updated date is updated as current date time");
                }
                else
                {
                    TimeSpan dateDiff = tdy - lastUpdate.LasteUpdated;
                    if (dateDiff.TotalHours >= 24)
                    {
                        _logger.LogInformation(DateTime.Now.ToShortTimeString() + "Method - " + "Index() ::" + "Message - " + "last updated hour is exceeds 24 hours");
                        UpdateTracker model = new()
                        {
                            LasteUpdated = tdy
                        };
                        _context.Add(model);
                        await _context.SaveChangesAsync();
                        TempData["LastUpdated"] = tdy;
                    }
                    else
                    {
                        _logger.LogInformation(DateTime.Now.ToShortTimeString() + "Method - " + "Index() ::" + "Message - " + "last updated hour is less than 24 hours");
                        TempData["LastUpdated"] = lastUpdate.LasteUpdated;
                    }
                }
                
            }
            catch(Exception ex)
            {
                _logger.LogError(DateTime.Now.ToShortTimeString() + "Method - " + "GetLastUpdated ::" + "Message - " + ex.Message);

            }
        }
        /// <summary>
        /// This method is to calculate average
        /// </summary>
        /// <param name="avg"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Calculate(CandleAverage avg)
        {
            try
            {
                var data = _provider.GetAvgCoinCandles(avg.FromDate, avg.ToDate);
                return Json(new { success = true, responseText = "Successfully calculated the Average!",average = data.ToString("0.00") });
            }
            catch (Exception ex)
            {
                _logger.LogError(DateTime.Now.ToShortTimeString() + "Method - " + "Calculate::" + "Message - " + ex.Message);
                return Json(new { success = true, responseText = "Something Went Wrong!", average=0 });
            }
        }
        /// <summary>
        /// This is to refresh the coin data
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> UpdateTimer()
        {
            try
            {
                UpdateTracker model = new()
                {
                    LasteUpdated = DateTime.Now
                };
                _context.Add(model);
                _context.SaveChanges();
                TempData["LastUpdated"] = DateTime.Now;
                _candleBTC = await _provider.GetCoinCandles(DateTime.Now.AddDays(-31), DateTime.Now);
                return Json(new { _candleBTC, lu = TempData["LastUpdated"] });
            }
            catch(Exception ex)
            {
                _logger.LogError(DateTime.Now.ToShortTimeString() + "Method - " + "UpdateTimer::" + "Message - " + ex.Message);
                return Json(null);
            }
        }

    }
}

