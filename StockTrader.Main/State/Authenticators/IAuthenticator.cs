/*-----------------------------------------------------------------------
// <copyright file="IAuthenticator.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the IAuthenticator class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the IAuthenticator class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Models;
using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Services.AuthentificationServices.Interfaces;

namespace StockTrader.Main.State.Authentificators
{
    public interface IAuthenticator
    {
        Account? CurrentAccount { get; }
        bool IsLoggedIn { get; }

        event Action StateChanged;

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <param name="startingBalance"></param>
        /// <returns></returns>
        Task<RegistrationResult> RegisterAsync(string email, string username, string password, string confirmPassword, double startingBalance);

        /// <summary>
        /// Login to the application
        /// </summary>
        /// <param name="username"> The user name </param>
        /// <param name="password"> The user password </param>
        /// <exception cref="UserNotFoundException"> Throw if the user does not exist </exception>
        /// <exception cref="InvalidPasswordException"> Throw if the password is invalid </exception>
        /// <exception cref="Exception"> Throw if the login is failed </exception>
        Task LoginAsync(string username, string password);

        /// <summary>
        /// Logout the current user
        /// </summary>
        void Logout();
    }
}
