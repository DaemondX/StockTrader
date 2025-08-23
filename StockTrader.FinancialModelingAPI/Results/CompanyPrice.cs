/*-----------------------------------------------------------------------
// <copyright file="CompanyPrice.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the CompanyPrice class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the CompanyPrice class.
//-----------------------------------------------------------------------*/

using Newtonsoft.Json;

namespace StockTrader.FinancialModelingAPI.Results
{
    /// <summary>
    /// Contains the price information for a company.
    /// </summary>
    public class CompanyPrice
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }
    }
}
