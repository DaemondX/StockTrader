/*-----------------------------------------------------------------------
// <copyright file="StockPriceResult.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the StockPriceResult class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the StockPriceResult class.
//-----------------------------------------------------------------------*/

using Newtonsoft.Json;

namespace StockTrader.FinancialModelingAPI.Results
{
    /// <summary>
    /// StockPriceResult class to hold the result of stock prices from the Financial Modeling API
    /// </summary>
    public class StockPriceResult
    {
        [JsonProperty("companiesPriceList")]
        public List<CompanyPrice> CompaniesPriceList { get; set; }
    }

}
