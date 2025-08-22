using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services.Interfaces
{
    public interface IMajorIndexService
    {
        Task<MajorIndex> GetMajorIndexAsync(MajorIndexType majorIndexType);
    }
}
