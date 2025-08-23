/*-----------------------------------------------------------------------
// <copyright file="AssetTransaction.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the AssetTransaction class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the AssetTransaction class.
//-----------------------------------------------------------------------*/

using StockTrader.Common.Models;

namespace StockTrader.Domain.Models
{
    public class AssetTransaction : CommonObject
    {
        public Account Account { get; set; }
        public bool IsPurchase { get; set; }
        public Asset Asset { get; set; }
        public int SharesAmount { get; set; }
        public DateTime DateProcessed { get; set; }
    }
}
