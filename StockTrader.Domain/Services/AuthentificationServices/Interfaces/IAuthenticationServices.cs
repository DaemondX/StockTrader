/*-----------------------------------------------------------------------
// <copyright file="IAuthenticationServices.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the IAuthenticationServices class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the IAuthenticationServices class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Models;
using StockTrader.Domain.Exceptions;

namespace StockTrader.Domain.Services.AuthentificationServices.Interfaces
{
    /// <summary>
    /// Enumeration for registration result
    /// </summary>
    public enum RegistrationResult
    {
        Success,
        PasswordDoNotMatch,
        EmailAlreadyExists,
        UsernameAlreadyExists,
        UsernameOrEmailOrPasswordIsEmpty,
        StartingBalanceMustBePositive
    }

    /// <summary>
    /// Defines methods for user authentication and account management, including user registration and login.
    /// </summary>
    /// <remarks>This interface provides functionality for registering new users and authenticating existing
    /// users. Implementations of this interface should handle validation, error handling, and any necessary persistence
    /// or external service interactions required for authentication.</remarks>
    public interface IAuthenticationServices
    {
        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <param name="startingBalance"></param>
        /// <returns></returns>
        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword, double startingBalance);

        /// <summary>
        /// Get an account for user's credentials
        /// </summary>
        /// <param name="username"> The user name </param>
        /// <param name="password"> The user password </param>
        /// <returns> The account for the user </returns>
        /// <exception cref="UserNotFoundException"> Throw if the user does not exist </exception>
        /// <exception cref="InvalidPasswordException"> Throw if the password is invalid </exception>
        /// <exception cref="Exception"> Throw if the login is failed </exception>
        Task<Account> Login(string username, string password);
    }
}
