/*-----------------------------------------------------------------------
// <copyright file="Navigator.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the Navigator class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the Navigator class.
//-----------------------------------------------------------------------*/

using StockTrader.Main.VVM.ViewModels;

namespace StockTrader.Main.State.Navigators
{
    public class Navigator : INavigator
    {
        // This is the current view model that is being displayed
        private BaseViewModel _currentViewModel;

        // Property to get or set the current view model
        public BaseViewModel CurrentViewModel 
        {   
            get => _currentViewModel;
            set
            {
                // Dispose the current view model, if it is not null
                _currentViewModel?.Dispose();

                _currentViewModel = value;
                // When the current view model is changed, the event is triggered
                OnStateChanged();
            }
        }

        /// <summary>
        /// OnStateChanged method to invoke the StateChanged event
        /// </summary>
        private void OnStateChanged()
        {
            StateChanged?.Invoke(); 
        }

        public event Action StateChanged;
    }
}
