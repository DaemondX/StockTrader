using StockTrader.Domain.Models;

namespace StockTrader.Main.State.Accounts
{
    public interface IAccountStore
    {
        Account? CurrentAccount { get; set; }
        event Action StateChanged;
    }
}
