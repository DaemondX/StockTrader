/*-----------------------------------------------------------------------
// <copyright file="MainViewModel.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the MainViewModel class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the MainViewModel class.
//-----------------------------------------------------------------------*/

using StockTrader.Main.Commands;
using StockTrader.Main.State.Authentificators;
using StockTrader.Main.State.Navigators;
using StockTrader.Main.VVM.ViewModels.Factories;
using System.Windows.Input;

namespace StockTrader.Main.VVM.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IStockTraderViewModelFactory _viewModelFactory;
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigatorToLogin;

        // Navigator is uses for binding the current view model to the UI
        public BaseViewModel CurrentViewModel 
            => _navigator.CurrentViewModel;

        // According authenticator in the Window, 
        // I show navigation bar, so is public
        public bool IsLoggedIn 
            => _authenticator.IsLoggedIn;

        // UserName
        public string UserName
        {
            get => IsLoggedIn ? _authenticator.CurrentAccount.AccountHolder.Username : string.Empty;
        }

        // UpdateCurrentViewModelCommand is uses for updating the current view model,
        // is binding in the MainWindow, so is public
        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand LogOutCommand { get; }

        /// <summary>
        /// MainViewModel constructor
        /// </summary>
        /// <param name="navigator"></param>
        /// <param name="authenticator"></param>
        /// <param name="viewModelFactory"></param>
        /// <param name="renavigatorToLogin"></param>
        public MainViewModel(INavigator navigator, 
            IAuthenticator authenticator,
            IStockTraderViewModelFactory viewModelFactory,
            IRenavigator renavigatorToLogin)
        {
            _viewModelFactory = viewModelFactory;
            _navigator = navigator;
            _authenticator = authenticator;
            _renavigatorToLogin = renavigatorToLogin;

            // Subscribe to the StateChanged event of the navigator
            _navigator.StateChanged += Navigator_StateChanged;
            // Subscribe to the StateChanged event of the authenticator
            _authenticator.StateChanged += Authenticator_StateChanged;

            // Set the UpdateCurrentViewModelCommand with the navigator and the view model factory
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);

            // Set the LogOutCommand with the authenticator and the renavigato
            LogOutCommand = new LogOutCommand(authenticator, _renavigatorToLogin, this);

            // Set the default view model to LoginViewModel
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
            _renavigatorToLogin = renavigatorToLogin;

        }

        // When the state of the authenticator changes, update the IsLoggedIn property
        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(UserName));
        }

        // When the state of the navigator changes, update the CurrentViewModel property
        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        // Unsubscribe from the StateChanged event of the navigator and the authenticator
        public override void Dispose()
        {
            _navigator.StateChanged -= Navigator_StateChanged;
            _authenticator.StateChanged -= Authenticator_StateChanged;
            base.Dispose();
        }
    }
}
