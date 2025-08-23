/*-----------------------------------------------------------------------
// <copyright file="SearchSymbolCommand.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the SearchSymbolCommand class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the SearchSymbolCommand class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Services.Interfaces;
using StockTrader.Main.State.Assets;
using StockTrader.Main.VVM.ViewModels;
using System.ComponentModel;

namespace StockTrader.Main.Commands
{
    public class SearchSymbolCommand : AsyncCommandBase
    {
        private readonly AssetStore _assetStore;
        private readonly ISearchSymbolViewModel _viewModel;
        private readonly IStockPriceService _stockPriceService;

        /// <summary>
        /// Search for a stock symbol and update the ViewModel with the price and shares owned
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="stockPriceService"></param>
        /// <param name="assetStore"></param>
        public SearchSymbolCommand(ISearchSymbolViewModel viewModel, 
                                   IStockPriceService stockPriceService,
                                   AssetStore assetStore)
        {
            _viewModel = viewModel;
            _stockPriceService = stockPriceService;
            _assetStore = assetStore;

            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        /// <summary>
        /// ViewModel property changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_viewModel.CanSearchSymbol))
            {
                OnRaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Can execute if the ViewModel allows searching and the base can execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override bool CanExecute(object? parameter)
        {
            return _viewModel.CanSearchSymbol &&
                base.CanExecute(parameter);
        }


        /// <summary>
        /// Execute the search for the stock symbol asynchronously
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(object? parameter)
        {
            string symbolToUpper = _viewModel.Symbol.ToUpper();

            try
            {
                // Get the price of the stock symbol from the API
                // and update the ViewModel
                _viewModel.SetStatusMessage = string.Empty;
                _viewModel.SetErrorMessage = string.Empty;
                _viewModel.PricePerShare = await _stockPriceService.GetPriceAsync(symbolToUpper);
                _viewModel.SearchResultSymbol = symbolToUpper;
                _viewModel.SharesOwned = Convert.ToString(_assetStore.GetAmountOwnedBySymbol(symbolToUpper));

            }
            catch (InvalidSymbolException)
            {
                _viewModel.SetErrorMessage = "Symbol does not exist";
            }
            catch(NullReferenceException)
            {
                _viewModel.SetErrorMessage = "Stock does not exist";
            }
            catch (Exception)
            {
                _viewModel.SetErrorMessage = "Failed to load stock information";
            }
        }
    }
}
