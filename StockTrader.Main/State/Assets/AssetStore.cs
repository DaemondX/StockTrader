/*-----------------------------------------------------------------------
// <copyright file="AssetStore.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the AssetStore class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the AssetStore class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Models;
using StockTrader.Domain.Services.Interfaces;
using StockTrader.Main.State.Accounts;
using StockTrader.Main.VVM.ViewModels;

namespace StockTrader.Main.State.Assets
{
    public class AssetStore
    {
        private readonly IAccountStore _accountStore;
        private readonly IStockPriceService _stockPriceService;

        // AccountBalance property
        public double AccountBalance
            => _accountStore.CurrentAccount?.Balance ?? 0;

        // AssetTransactions property
        public IEnumerable<AssetTransaction> AssetTransactions
            => _accountStore.CurrentAccount?.AssetTransactions ?? new List<AssetTransaction>();

        public event Action StateChanged;

        /// <summary>
        /// AssetStore constructor
        /// </summary>
        /// <param name="accountStore"></param>
        /// <param name="stockPriceService"></param>
        public AssetStore(IAccountStore accountStore, IStockPriceService stockPriceService)
        {
            _accountStore = accountStore;
            _stockPriceService = stockPriceService;

            _accountStore.StateChanged += OnStateChanged;
        }

        /// <summary>
        /// OnStateChanged method to invoke the StateChanged event
        /// </summary>
        private void OnStateChanged()
        {
            StateChanged?.Invoke();
        }

        /// <summary>
        /// When takeCount is -1, it will return all assets
        /// </summary>
        /// <param name="takeCount"> Amount of asset </param>
        /// <returns> All assets ordered by descending </returns>
        public IEnumerable<AssetViewModel> GetAssetsOrderByDescending(int count)
        {

            IEnumerable<AssetViewModel> returnAssetViewModel = AssetTransactions.GroupBy(s => s.Asset.Symbol)
                    .Select(g => new AssetViewModel()
                    {
                        Symbol = g.Key,
                        Shares = g.Sum(a => a.IsPurchase ? a.SharesAmount : -a.SharesAmount)
                    })
                    .Where(a => a.Shares > 0)
                    .OrderByDescending(s => s.Shares);

            return count == -1 ? returnAssetViewModel : returnAssetViewModel.Take(count);
        }

        /// <summary>
        /// When takeCount is -1, it will return all assets ordered by descending according to the money
        /// </summary>
        /// <param name="takeCount"> Amount of asset </param>
        /// <returns> All assets ordered by descending according to the money </returns>
        public async Task<IEnumerable<AssetViewModel>> GetAssetsAccordingByMoneyByDescending(int takeCount)
        {
            IEnumerable<AssetViewModel> assetViewModelAccordingByMoney = await Task.WhenAll(
                AssetTransactions.GroupBy(b => b.Asset.Symbol)
                 .Select(async g => new AssetViewModel()
                 {
                     Symbol = g.Key,
                     Shares = g.Sum(a => a.IsPurchase ? a.SharesAmount : -a.SharesAmount),
                     PricePerShare = await _stockPriceService.GetPriceAsync(g.Key)
                 }));

            return takeCount == -1 ? assetViewModelAccordingByMoney.OrderByDescending(s => s.AssetValue)
                                   : assetViewModelAccordingByMoney.OrderByDescending(s => s.AssetValue).Take(takeCount);
        }

        /// <summary>
        /// Get the amount of owned asset by symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public int GetAmountOwnedBySymbol(string symbol)
        {
            return AssetTransactions.GroupBy(s => s.Asset.Symbol)
                   .Where(g => g.Key == symbol)
                   .Select(g => new AssetViewModel()
                   {
                       Symbol = g.Key,
                       Shares = g.Sum(a => a.IsPurchase ? a.SharesAmount : -a.SharesAmount)
                   })
                   .Sum(s => s.Shares);
        }
    }
}
