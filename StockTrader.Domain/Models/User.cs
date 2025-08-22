using StockTrader.Common.Models;

namespace StockTrader.Domain.Models
{
    public class User : CommonObject
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.Now;
    }
}
