/*-----------------------------------------------------------------------
// <copyright file="INavigator.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the INavigator class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the INavigator class.
//-----------------------------------------------------------------------*/

using StockTrader.Main.VVM.ViewModels;

namespace StockTrader.Main.State.Navigators
{
    /// <summary>
    /// View types for navigation
    /// </summary>
    public enum ViewType
    {
        Home,
        Portfolio,
        Buy,
        Sell,
        Login
    }

    /// <summary>
    /// Interface for navigator to handle view model navigation
    /// </summary>
    public interface INavigator
    {
        BaseViewModel CurrentViewModel { get; set; }
        event Action StateChanged; 
    }
}
