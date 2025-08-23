/*-----------------------------------------------------------------------
// <copyright file="ViewModelDelegateRenavigator.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the ViewModelDelegateRenavigator class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the ViewModelDelegateRenavigator class.
//-----------------------------------------------------------------------*/

using StockTrader.Main.VVM.ViewModels;

namespace StockTrader.Main.State.Navigators
{
    public class ViewModelDelegateRenavigator<TViewModel> : IRenavigator where TViewModel : BaseViewModel
    {
        // For state current view model
        private readonly INavigator _navigator;
        // For creating new view model according to the type
        private readonly CreateViewModel<TViewModel> _createViewModelDelegate;

        /// <summary>
        /// ViewModelDelegateRenavigator constructor
        /// </summary>
        /// <param name="navigator"></param>
        /// <param name="createViewModelDelegate"></param>
        public ViewModelDelegateRenavigator(INavigator navigator, CreateViewModel<TViewModel> createViewModelDelegate)
        {
            _navigator = navigator;
            _createViewModelDelegate = createViewModelDelegate;
        }

        /// <summary>
        /// Method to renavigate the current view model according to the type of TViewModel where TViewModel : BaseViewModel
        /// </summary>
        public void Renavigate()
        {
            _navigator.CurrentViewModel = _createViewModelDelegate();
        }
    }
}
