/*-----------------------------------------------------------------------
// <copyright file="IStockTraderViewModelFactory.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the IStockTraderViewModelFactory class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the IStockTraderViewModelFactory class.
//-----------------------------------------------------------------------*/

using StockTrader.Main.State.Navigators;

namespace StockTrader.Main.VVM.ViewModels.Factories
{
    public interface IStockTraderViewModelFactory
    {
        /// <summary>
        /// Create a ViewModel based on the ViewType
        /// </summary>
        /// <param name="viewType"></param>
        /// <returns></returns>
        BaseViewModel CreateViewModel(ViewType viewType);
    }
}
