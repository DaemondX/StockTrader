/*-----------------------------------------------------------------------
// <copyright file="LogOutCommand.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the LogOutCommand class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the LogOutCommand class.
//-----------------------------------------------------------------------*/

using StockTrader.Main.State.Authentificators;
using StockTrader.Main.State.Navigators;
using StockTrader.Main.VVM.ViewModels;
using System.ComponentModel;

namespace StockTrader.Main.Commands
{
    /// <summary>
    /// LogOutCommand is a command that logs out the user from the application
    /// and renavigates to the login view
    /// </summary>
    public class LogOutCommand : BaseCommand
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;
        private readonly MainViewModel _mainViewModel;

        /// <summary>
        /// LogOutCommand constructor
        /// </summary>
        /// <param name="authenticator"></param>
        /// <param name="renavigator"></param>
        /// <param name="mainViewModel"></param>
        public LogOutCommand(IAuthenticator authenticator, IRenavigator renavigator, MainViewModel mainViewModel)
        {
            _authenticator = authenticator;
            _renavigator = renavigator;
            _mainViewModel = mainViewModel;

            mainViewModel.PropertyChanged += OnMainViewModelPropertyChanged;
        }

        /// <summary>
        /// OnMainViewModelPropertyChanged is called when the MainViewModel's property changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMainViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_mainViewModel.IsLoggedIn))
            {
                OnRaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// CanExecute checks if the command can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override bool CanExecute(object? parameter)
         => _authenticator.IsLoggedIn;

        /// <summary>
        /// Execute logs out the user and renavigates to the login view
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object? parameter)
        {
            _authenticator.Logout();
            _renavigator.Renavigate();
        }
    }
}
