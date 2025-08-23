/*-----------------------------------------------------------------------
// <copyright file="UserNotFoundException.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the UserNotFoundException class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the UserNotFoundException class.
//-----------------------------------------------------------------------*/

namespace StockTrader.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string Username { get; }

        /// <summary>
        /// Exception thrown when a user is not found in the system.
        /// </summary>
        /// <param name="username"></param>
        public UserNotFoundException(string username)
        {
            Username = username;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="message"></param>
        public UserNotFoundException(string username, string? message) : base(message)
        {
            Username = username;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public UserNotFoundException(string username, string? message, Exception? innerException) : base(message, innerException)
        {
            Username = username;
        }
    }
}
