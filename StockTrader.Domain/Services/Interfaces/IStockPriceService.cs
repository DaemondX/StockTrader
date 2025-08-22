namespace StockTrader.Domain.Services.Interfaces
{
    public interface IStockPriceService
    {
        Task<double> GetPriceAsync(string symbol);
    }
}
