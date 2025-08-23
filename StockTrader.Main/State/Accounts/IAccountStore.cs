/*-----------------------------------------------------------------------
// <copyright file="IAccountStore.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the IAccountStore class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the IAccountStore class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Models;

namespace StockTrader.Main.State.Accounts
{
    /// <summary>
    /// Interface for managing the current account state
    /// </summary>
    public interface IAccountStore
    {
        Account? CurrentAccount { get; set; }
        event Action StateChanged;
    }
}
