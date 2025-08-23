/*-----------------------------------------------------------------------
// <copyright file="IStockPriceService.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the IStockPriceService class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the IStockPriceService class.
//-----------------------------------------------------------------------*/

namespace StockTrader.Domain.Services.Interfaces
{
    public interface IStockPriceService
    {
        /// <summary>
        /// Get the current price of a stock by its symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task<double> GetPriceAsync(string symbol);
    }
}
