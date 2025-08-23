/*-----------------------------------------------------------------------
// <copyright file="Account.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the Account class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the Account class.
//-----------------------------------------------------------------------*/

using StockTrader.Common.Models;

namespace StockTrader.Domain.Models
{
    public class Account : CommonObject
    {
        public User AccountHolder { get; set; }
        public double Balance { get; set; }
        public ICollection<AssetTransaction> AssetTransactions { get; set; }
    }
}
