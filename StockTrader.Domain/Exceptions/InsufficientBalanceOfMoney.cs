/*-----------------------------------------------------------------------
// <copyright file="InsufficientBalanceOfMoney.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the InsufficientBalanceOfMoney class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the InsufficientBalanceOfMoney class.
//-----------------------------------------------------------------------*/

namespace StockTrader.Domain.Exceptions
{
    public class InsufficientBalanceOfMoney : Exception
    {
        double _balance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="balance"></param>
        public InsufficientBalanceOfMoney(double balance)
        {
            _balance = balance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="message"></param>
        public InsufficientBalanceOfMoney(double balance, string? message) : base(message)
        {
            _balance = balance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public InsufficientBalanceOfMoney(double balance, string? message, Exception? innerException) : base(message, innerException)
        {
            _balance = balance;
        }
    }
}
