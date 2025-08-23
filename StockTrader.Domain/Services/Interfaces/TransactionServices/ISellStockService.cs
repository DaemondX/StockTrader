/*-----------------------------------------------------------------------
// <copyright file="ISellStockService.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the ISellStockService class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the ISellStockService class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Models;
using StockTrader.Domain.Exceptions;

namespace StockTrader.Domain.Services.Interfaces.TransactionServices
{
    public interface ISellStockService
    {
        /// <summary>
        /// Sell a stock from the seller's account.
        /// </summary>
        /// <param name="seller"> The account of the seller. </param>
        /// <param name="symbol"> The symbol sold. </param>
        /// <param name="shares"> The amount of shares to sell. </param>
        /// <returns> The udpated account. </returns>
        /// <exception cref="InvalidSymbolException"> Thrown if the purchased symbol is invalid. </exception>
        /// <exception cref="InsufficientSharesException"> Thrown if the shares are insufficient </exception>
        /// <exception cref="NullReferenceException"> Thrown if we use not reference objects </exception>
        /// <exception cref="Exception"> Thrown if the trancasction fails. </exception>
        Task<Account> SellStockAsync(Account seller, string symbol, int shares);
    }
}
