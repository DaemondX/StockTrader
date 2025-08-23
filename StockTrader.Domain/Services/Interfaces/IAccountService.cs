/*-----------------------------------------------------------------------
// <copyright file="IAccountService.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the IAccountService class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the IAccountService class.
//-----------------------------------------------------------------------*/

using StockTrader.Common.Interfaces;
using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services.Interfaces
{
    public interface IAccountService : ICommonRepository<Account>
    {
        /// <summary>
        /// Get an account by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<Account?> GetByEmail(string email);
        /// <summary>
        /// Get an account by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<Account?> GetByUserName(string username);
        /// <summary>
        /// Get a user by PC name and ID
        /// </summary>
        /// <param name="pcName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User?> GetByPCName(string pcName, int id);
    }
}
