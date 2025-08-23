/*-----------------------------------------------------------------------
// <copyright file="RegisterCommand.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the RegisterCommand class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the RegisterCommand class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Services.AuthentificationServices.Interfaces;
using StockTrader.Main.State.Authentificators;
using StockTrader.Main.State.Navigators;
using StockTrader.Main.VVM.ViewModels;
using System.ComponentModel;

namespace StockTrader.Main.Commands
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly RegisterViewModel _registerViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _loginRenavigator;

        /// <summary>
        /// RegisterCommand constructor
        /// </summary>
        /// <param name="registerViewModel"></param>
        /// <param name="authenticator"></param>
        /// <param name="loginRenavigator"></param>
        public RegisterCommand(RegisterViewModel registerViewModel, 
            IAuthenticator authenticator,
            IRenavigator loginRenavigator)
        {
            _registerViewModel = registerViewModel;
            _authenticator = authenticator;
            _loginRenavigator = loginRenavigator;

            _registerViewModel.PropertyChanged += OnPropertyViewModelChanged;
        }

        /// <summary>
        /// OnPropertyViewModelChanged method to handle the PropertyChanged event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPropertyViewModelChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_registerViewModel.Username) ||
                e.PropertyName == nameof(_registerViewModel.Email) ||
                e.PropertyName == nameof(_registerViewModel.Password) ||
                e.PropertyName == nameof(_registerViewModel.StartingBalance) ||
                e.PropertyName == nameof(_registerViewModel.ConfirmPassword))
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
        {
            return _registerViewModel.CanTryRegister && 
                   base.CanExecute(parameter);  
        }

        /// <summary>
        /// ExecuteAsync method to execute the command asynchronously
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(object? parameter)
        {
            // Clear the error message
            _registerViewModel.SetErrorMessageViewModel = string.Empty;

            try
            {
                // Try to register the new user
                RegistrationResult result = await _authenticator.RegisterAsync(_registerViewModel.Email,
                                       _registerViewModel.Username,
                                       _registerViewModel.Password,
                                       _registerViewModel.ConfirmPassword,
                                       Convert.ToDouble(_registerViewModel.StartingBalance));

                // Action for the result of the registration
                ActionForRegistrationResult(result, _loginRenavigator);
            }
            catch (Exception)
            {
                _registerViewModel.SetErrorMessageViewModel = "Registration failed";
            }
        }

        /// <summary>
        /// ActionForRegistrationResult method to handle the result of the registration
        /// </summary>
        /// <param name="result"></param>
        /// <param name="registerRenavigator"></param>
        private void ActionForRegistrationResult(RegistrationResult result, IRenavigator registerRenavigator)
        {
            switch (result)
            {
                case RegistrationResult.Success:
                     registerRenavigator.Renavigate();
                    break;
                case RegistrationResult.PasswordDoNotMatch:
                    _registerViewModel.SetErrorMessageViewModel = "Password does not match witih confirm password.";
                    break;
                case RegistrationResult.EmailAlreadyExists:
                    _registerViewModel.SetErrorMessageViewModel = "An account for this email already exists.";
                    break;
                case RegistrationResult.UsernameAlreadyExists:
                    _registerViewModel.SetErrorMessageViewModel = "An account for this username already exists";
                    break;
                case RegistrationResult.StartingBalanceMustBePositive:
                    _registerViewModel.SetErrorMessageViewModel = "Starting balance must be positive";
                    break;
                default:
                    _registerViewModel.SetErrorMessageViewModel = "Registration failed";
                    break;
            }
        }
    }
}
