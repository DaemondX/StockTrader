/*-----------------------------------------------------------------------
// <copyright file="AccountStore.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the AccountStore class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the AccountStore class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Models;

namespace StockTrader.Main.State.Accounts
{
    /// <summary>
    /// A store for managing the current account state.
    /// </summary>
    public class AccountStore : IAccountStore
    {
        private Account? _currentAccount;
        public Account? CurrentAccount
        {
            get => _currentAccount;
            set
            {
                _currentAccount = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
    }
}
