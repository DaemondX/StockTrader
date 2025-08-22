using StockTrader.Common.Models;

namespace StockTrader.Domain.Models
{
    public class Account : CommonObject
    {
        public User AccountHolder { get; set; }
        public double Balance { get; set; }
        public ICollection<AssetTransaction> AssetTransactions { get; set; }
    }
}
