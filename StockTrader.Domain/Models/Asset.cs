/*-----------------------------------------------------------------------
// <copyright file="Asset.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the Asset class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the Asset class.
//-----------------------------------------------------------------------*/

namespace StockTrader.Domain.Models
{
    public class Asset
    {
        public string Symbol { get; set; }
        public double PricePerShares { get; set; }
    }
}
