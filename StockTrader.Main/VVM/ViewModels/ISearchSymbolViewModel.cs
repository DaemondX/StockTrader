/*-----------------------------------------------------------------------
// <copyright file="ISearchSymbolViewModel.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the ISearchSymbolViewModel class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the ISearchSymbolViewModel class.
//-----------------------------------------------------------------------*/

using System.ComponentModel;
namespace StockTrader.Main.VVM.ViewModels
{
    public interface ISearchSymbolViewModel : INotifyPropertyChanged
    {
        double PricePerShare { set; }
        string SearchResultSymbol { set; }
        string SetErrorMessage { set; }
        string SetStatusMessage { set; }
        string SharesOwned { set;}
        string Symbol { get;  }
        bool CanSearchSymbol { get; }
    }
}