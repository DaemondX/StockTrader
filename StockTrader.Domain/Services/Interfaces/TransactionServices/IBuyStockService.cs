/*-----------------------------------------------------------------------
// <copyright file="IBuyStockService.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the IBuyStockService class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the IBuyStockService class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services.Interfaces.TransactionServices
{
    public interface IBuyStockService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buyer"></param>
        /// <param name="stockSymbol"></param>
        /// <param name="shares"></param>
        /// <returns></returns>
        Task<Account> BuyStockAsync(Account buyer, string stockSymbol, int shares);
    }
}
