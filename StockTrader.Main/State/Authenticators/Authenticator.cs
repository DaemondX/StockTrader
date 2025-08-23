/*-----------------------------------------------------------------------
// <copyright file="Authenticator.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the Authenticator class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the Authenticator class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Models;
using StockTrader.Domain.Services.AuthentificationServices.Interfaces;
using StockTrader.Main.State.Accounts;
using StockTrader.Main.State.Authentificators;

namespace StockTrader.Main.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        // Service from domain layer to authenticate the user with the database
        private readonly IAuthenticationServices _authentificationService;
        // Store the one current account for whole application
        private readonly IAccountStore _accountStore;

        /// <summary>
        /// Authenticator constructor
        /// </summary>
        /// <param name="authentificationService"></param>
        /// <param name="accountStore"></param>
        public Authenticator(IAuthenticationServices authentificationService, IAccountStore accountStore)
        {
            _authentificationService = authentificationService;
            _accountStore = accountStore;
        }

        /// <summary>
        /// Current logged in account
        /// </summary>
        public Account? CurrentAccount
        {
            get => _accountStore.CurrentAccount;
            private set
            {
                // Set the current account 
                _accountStore.CurrentAccount = value;
                // Notify the subscribers that the state has changed
                OnStateChanged();
            }
        }

        /// <summary>
        /// OnStateChanged method to notify the subscribers that the state has changed
        /// </summary>
        private void OnStateChanged()
        {
            StateChanged?.Invoke();
        }

        /// <summary>
        /// Indicate whether the user is logged in or not
        /// </summary>
        public bool IsLoggedIn
            => CurrentAccount != null;

        public event Action StateChanged;

        /// <summary>
        /// Async Login the user with the given username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task LoginAsync(string username, string password)
        {
            // If the login is successful,
            // the CurrentAccount will be set to the account that was logged in
            CurrentAccount = await _authentificationService.Login(username, password);
        }

        /// <summary>
        /// Logout the current user
        /// </summary>
        public void Logout()
         => CurrentAccount = null;

        /// <summary>
        /// Async method to register the user with the given email, username, password, confirmPassword and startBalance
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <param name="startBalance"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<RegistrationResult> RegisterAsync(string email, string username, string password, string confirmPassword, double startingBalance)
         => await _authentificationService.Register(email, username, password, confirmPassword, startingBalance);   
    }
}
