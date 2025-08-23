/*-----------------------------------------------------------------------
// <copyright file="AssetViewModel.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the AssetViewModel class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the AssetViewModel class.
//-----------------------------------------------------------------------*/

namespace StockTrader.Main.VVM.ViewModels
{
    public class AssetViewModel : BaseViewModel
    {
        public string Symbol { get; init; } = string.Empty;
        public int Shares { get; init; } = 0;
        public double PricePerShare { get; init; } = 0;
        public double AssetValue 
            =>  Shares * PricePerShare;
    }
}
