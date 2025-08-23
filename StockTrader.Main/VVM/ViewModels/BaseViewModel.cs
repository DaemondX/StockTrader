/*-----------------------------------------------------------------------
// <copyright file="BaseViewModel.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the BaseViewModel class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the BaseViewModel class.
//-----------------------------------------------------------------------*/

using System.ComponentModel;

namespace StockTrader.Main.VVM.ViewModels
{
    // Base delegate to create a ViewModel according to the type
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : BaseViewModel;
    public class BaseViewModel : INotifyPropertyChanged
    {
        public virtual void Dispose() { }

        // Notify the UI that a property has changed
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
