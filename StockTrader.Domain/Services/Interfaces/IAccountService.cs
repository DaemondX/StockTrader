using StockTrader.Common.Interfaces;
using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services.Interfaces
{
    public interface IAccountService : ICommonRepository<Account>
    {
        Task<Account?> GetByEmail(string email);
        Task<Account?> GetByUserName(string username);
        Task<User?> GetByPCName(string pcName, int id);
    }
}
