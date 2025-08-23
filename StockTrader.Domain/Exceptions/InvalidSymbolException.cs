/*-----------------------------------------------------------------------
// <copyright file="InvalidSymbolException.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the InvalidSymbolException class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the InvalidSymbolException class.
//-----------------------------------------------------------------------*/

namespace StockTrader.Domain.Exceptions
{
    public class InvalidSymbolException : Exception
    {
        public string Symbol { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSymbolException"/> class with the specified invalid
        /// symbol.
        /// </summary>
        /// <param name="symbol">The symbol that caused the exception. This value cannot be null or empty.</param>
        public InvalidSymbolException(string symbol)
        {
            Symbol = symbol;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="message"></param>
        public InvalidSymbolException(string symbol, string? message) : base(message)
        {
            Symbol = symbol;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public InvalidSymbolException(string symbol, string? message, Exception? innerException) : base(message, innerException)
        {
            Symbol = symbol;
        }
    }
}
