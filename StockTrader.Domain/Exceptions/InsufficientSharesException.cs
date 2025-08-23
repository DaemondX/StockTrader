/*-----------------------------------------------------------------------
// <copyright file="InsufficientSharesException.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the InsufficientSharesException class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the InsufficientSharesException class.
//-----------------------------------------------------------------------*/

namespace StockTrader.Domain.Exceptions
{
    public class InsufficientSharesException : Exception
    {
        public string Symbol { get; }
        public int AccountShares { get; }
        public int RequiredShares { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="accountShares"></param>
        /// <param name="requiredShares"></param>
        public InsufficientSharesException(string symbol, int accountShares, int requiredShares)
        {
            Symbol = symbol;
            AccountShares = accountShares;
            RequiredShares = requiredShares;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="accountShares"></param>
        /// <param name="requiredShares"></param>
        /// <param name="message"></param>
        public InsufficientSharesException(string symbol, int accountShares, int requiredShares, string? message) : base(message)
        {
            Symbol = symbol;
            AccountShares = accountShares;
            RequiredShares = requiredShares;
        }

        /// <summary>
        /// Represents an exception that is thrown when an account does not have sufficient shares to complete an
        /// operation.
        /// </summary>
        /// <param name="symbol">The symbol of the stock or asset for which the operation was attempted.</param>
        /// <param name="accountShares">The number of shares currently available in the account.</param>
        /// <param name="requiredShares">The number of shares required to complete the operation.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="innerException">The exception that caused the current exception, or <see langword="null"/> if no inner exception is
        /// specified.</param>
        public InsufficientSharesException(string symbol, int accountShares, int requiredShares, string? message, Exception? innerException) : base(message, innerException)
        {
            Symbol = symbol;
            AccountShares = accountShares;
            RequiredShares = requiredShares;
        }
    }
}
