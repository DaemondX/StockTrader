using Newtonsoft.Json;

namespace StockTrader.FinancialModelingAPI.Results
{
    public class StockPriceResult
    {
        [JsonProperty("companiesPriceList")]
        public List<CompanyPrice> CompaniesPriceList { get; set; }
    }

}
