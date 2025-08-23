/*-----------------------------------------------------------------------
// <copyright file="LoginCommand.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the LoginCommand class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the LoginCommand class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Exceptions;
using StockTrader.Main.State.Authentificators;
using StockTrader.Main.State.Navigators;
using StockTrader.Main.VVM.ViewModels;

namespace StockTrader.Main.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        /// <summary>
        /// Login command constructor
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <param name="authenticator"></param>
        /// <param name="renavigator"></param>
        public LoginCommand(LoginViewModel loginViewModel, 
            IAuthenticator authenticator,
            IRenavigator renavigator)
        {
            _loginViewModel = loginViewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;
        }

        /// <summary>
        /// Login command constructor
        /// </summary>
        /// <param name="loginViewModel"></param>
        public LoginCommand(LoginViewModel loginViewModel)
        {
            _loginViewModel = loginViewModel;
        }

        /// <summary>
        /// Execute the login command
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(object? parameter)
        {
            if (parameter != null)
            {
                try
                {
                    // Verify the login credentials
                    await _authenticator.LoginAsync(_loginViewModel.Username, parameter.ToString());
                    // If the login is successful, navigate to the Home view
                    _renavigator.Renavigate();
                }
                catch (UserNotFoundException)
                {
                    _loginViewModel.SetErrorMessageViewModel = "Username does not exist";
                }
                catch (InvalidPasswordException)
                {
                    _loginViewModel.SetErrorMessageViewModel = "Password is incorrect";
                }
                catch (DifferentPCNameException ex)
                {
                    _loginViewModel.SetErrorMessageViewModel = $"Login failed. This account is registered on another PC: {ex.PCName}";
                }
                catch (Exception)
                {
                    _loginViewModel.SetErrorMessageViewModel = "Login failed";
                }
            }
        }
    }
}
