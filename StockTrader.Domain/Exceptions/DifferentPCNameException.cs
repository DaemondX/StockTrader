/*-----------------------------------------------------------------------
// <copyright file="DifferentPCNameException.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the DifferentPCNameException class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the DifferentPCNameException class.
//-----------------------------------------------------------------------*/

namespace StockTrader.Domain.Exceptions
{
    public class DifferentPCNameException : Exception
    {
        public string Username { get; }
        public string MachineName { get; }
        public string PCName { get; }

        /// <summary>
        /// Exception thrown when the PC name used during login does not match the one stored during registration.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="machineName"></param>
        /// <param name="pcName"></param>
        public DifferentPCNameException(string userName, string machineName, string pcName)
        {
            Username = userName;
            MachineName = machineName;
            PCName = pcName;
        }
    }
}