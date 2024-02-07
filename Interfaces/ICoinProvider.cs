using CryptoBTC.Models;

namespace CryptoBTC.Interfaces
{
    public interface ICoinProvider
    {
        Task<List<CandlesBTC>> GetCoinCandles(DateTime? from, DateTime? to);
        double GetAvgCoinCandles(DateTime from, DateTime to);
    }
}
