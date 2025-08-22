using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services.Interfaces.TransactionServices
{
    public interface IBuyStockService
    {
        Task<Account> BuyStockAsync(Account buyer, string stockSymbol, int shares);
    }
}
