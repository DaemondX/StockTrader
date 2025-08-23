/*-----------------------------------------------------------------------
// <copyright file="InvalidPasswordException.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the InvalidPasswordException class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the InvalidPasswordException class.
//-----------------------------------------------------------------------*/

namespace StockTrader.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public string Username { get; }
        public string Password { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public InvalidPasswordException(string? message, string username, string password) : base(message)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public InvalidPasswordException(string? message, Exception? innerException, string username, string password) : base(message, innerException)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPasswordException"/> class with the specified username
        /// and password.
        /// </summary>
        /// <param name="username">The username associated with the invalid password attempt.</param>
        /// <param name="password">The invalid password that caused the exception.</param>
        public InvalidPasswordException(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
