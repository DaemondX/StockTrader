/*-----------------------------------------------------------------------
// <copyright file="SellStockCommand.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the SellStockCommand class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the SellStockCommand class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Models;
using StockTrader.Domain.Services.Interfaces.TransactionServices;
using StockTrader.Main.State.Accounts;
using StockTrader.Main.VVM.ViewModels;
using System.ComponentModel;

namespace StockTrader.Main.Commands
{
    public class SellStockCommand : AsyncCommandBase
    {
        private readonly SellViewModel _sellViewModel;
        private readonly ISellStockService _sellStockService;
        private readonly IAccountStore _accountStore;

        /// <summary>
        /// SellStockCommand constructor
        /// </summary>
        /// <param name="sellViewModel"></param>
        /// <param name="sellStockService"></param>
        /// <param name="accountStore"></param>
        public SellStockCommand(SellViewModel sellViewModel, 
            ISellStockService sellStockService,
            IAccountStore accountStore)
        {
            _sellViewModel = sellViewModel;
            _sellStockService = sellStockService;
            _accountStore = accountStore;

            // Subscribe to the PropertyChanged event of the BuyViewModel property has changed
            _sellViewModel.PropertyChanged += OnPropertyViewModelChanged;
        }

        /// <summary>
        /// OnPropertyViewModelChanged method to handle the PropertyChanged event of the BuyViewModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPropertyViewModelChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_sellViewModel.CanSellStock))
            {
                OnRaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// CanExecute method to determine if the command can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override bool CanExecute(object? parameter)
            => _sellViewModel.CanSellStock &&
               base.CanExecute(parameter);

        /// <summary>
        /// ExecuteAsync method to execute the command asynchronously
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async override Task ExecuteAsync(object? parameter)
        {
            string sellSymbolToUpper = _sellViewModel.Symbol.ToUpper();

            // Set the status and error messages to empty
            _sellViewModel.SetStatusMessage = string.Empty;
            _sellViewModel.SetErrorMessage = string.Empty;

            try
            {
                // Buy the stock and return the account after the purchase
                Account account = await _sellStockService.SellStockAsync(_accountStore.CurrentAccount, sellSymbolToUpper,
                    Convert.ToInt32(_sellViewModel.SharesToSell));

                // Update the current account with the balance after the purchase, transactions ...
                _accountStore.CurrentAccount = account;

                // Show a message box to inform the user that the purchase was successful
                _sellViewModel.SetStatusMessage = $"Successfully sold {_sellViewModel.SharesToSell} shares of {sellSymbolToUpper}";

                _sellViewModel.SearchResultSymbol = string.Empty;
            }
            catch (InvalidSymbolException)
            {
                // Inform the user that the purchase was not successful
                _sellViewModel.SetErrorMessage = "Symbol does not exist. ";
            }
            catch (InsufficientSharesException ex)
            {
                // Inform the user that the purchase was not successful
                _sellViewModel.SetErrorMessage = $"Account has insufficient shares. You only have {ex.AccountShares} shares.";
            }
            catch (Exception ex)
            {
                // Inform the user that the purchase was not successful
                _sellViewModel.SetErrorMessage = ex.Message;
            }
        }
    }
}
