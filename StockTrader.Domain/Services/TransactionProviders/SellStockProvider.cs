/*-----------------------------------------------------------------------
// <copyright file="SellStockProvider.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the SellStockProvider class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the SellStockProvider class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Models;
using StockTrader.Domain.Services.Interfaces;
using StockTrader.Domain.Services.Interfaces.TransactionServices;

namespace StockTrader.Domain.Services.TransactionProviders
{
    public class SellStockProvider : ISellStockService
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IAccountService _accountService;

        /// <summary>
        /// SellStockProvider constructor
        /// </summary>
        /// <param name="accountService"></param>
        /// <param name="stockPriceService"></param>
        public SellStockProvider(IAccountService accountService, IStockPriceService stockPriceService)
        {
            _accountService = accountService;
            _stockPriceService = stockPriceService;
        }
        /// <summary>
        /// Sell stock for the given account
        /// </summary>
        /// <param name="seller"></param>
        /// <param name="symbol"></param>
        /// <param name="shares"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InsufficientSharesException"></exception>
        public async Task<Account> SellStockAsync(Account seller, string symbol, int sharesForSell)
        {
            // Validate the parameters
            if(seller == null)
            {
                throw new NullReferenceException("Seller is not set");
            }

            if(string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException("Symbol is not set");
            }

            // Validate seller has sufficient shares to sell
            int sharesOwned = GetAccountSharesForSymbol(seller, symbol);
            if (sharesOwned < sharesForSell)
            {
                throw new InsufficientSharesException(symbol, sharesOwned, sharesForSell);
            }

            // Get price of the stock
            double currentStockPrice = await _stockPriceService.GetPriceAsync(symbol);

            // Add the transaction to the account
            seller.AssetTransactions.Add(new AssetTransaction
            {
                Account = seller,
                Asset = new Asset
                {
                    Symbol = symbol,
                    PricePerShares = currentStockPrice
                },
                IsPurchase = false,
                SharesAmount = sharesForSell,
                DateProcessed = DateTime.Now
            });

            // Add the new balance
            seller.Balance += currentStockPrice * sharesForSell;

            // Update the account with the new transaction
            Account? resultUpdatedAccount = await _accountService.UpdateAsync(seller.Id, seller);
            if(resultUpdatedAccount == null)
            {
                throw new Exception("The transaction failed");
            }

            return resultUpdatedAccount;
        }

        private int GetAccountSharesForSymbol(Account seller, string symbol)
        {
            return seller.AssetTransactions.Where(t => t.Asset.Symbol == symbol)
                                           .Sum(a => a.IsPurchase ? a.SharesAmount : -a.SharesAmount);    
        }
    }
}
