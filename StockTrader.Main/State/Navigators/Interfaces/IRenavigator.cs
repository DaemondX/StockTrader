/*-----------------------------------------------------------------------
// <copyright file="IRenavigator.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the IRenavigator class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the IRenavigator class.
//-----------------------------------------------------------------------*/

namespace StockTrader.Main.State.Navigators
{
    public interface IRenavigator
    {
        /// <summary>
        /// Navigate to the current ViewModel again
        /// </summary>
        void Renavigate();
    }
}
