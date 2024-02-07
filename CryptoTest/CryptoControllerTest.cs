using CryptoBTC.Business;
using CryptoBTC.Data;
using CryptoBTC.Models;
using System.Collections.Generic;

namespace CryptoTest
{
    public class CryptoControllerTest
    {
        private CoinProvider _testingUnit = null;
        private CryptoContext _context = null;
        public CryptoControllerTest()
        {
            if(_testingUnit==null)
            {
                _testingUnit = new CoinProvider(_context);
            }
        }
        [Fact]
        public void AverageTest()
        {
            DateTime _frm = DateTime.Now.AddDays(1000);
            DateTime _to = DateTime.Now.AddDays(900);
            var result = _testingUnit.GetAvgCoinCandles(_frm, _to);

            Assert.Equal(0, result);
        }
        [Fact]
        public async void GetCoinCandlesTest()
        {
            DateTime? _frm = null;
            DateTime? _to = null;
            var result = _testingUnit.GetCoinCandles(_frm, _to);
            int count = result.Result.Count;
            Assert.Equal( 0, count);
        }
    }
}