/*-----------------------------------------------------------------------
// <copyright file="UpdateCurrentViewModelCommand.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the UpdateCurrentViewModelCommand class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the UpdateCurrentViewModelCommand class.
//-----------------------------------------------------------------------*/

using StockTrader.Main.State.Navigators;
using StockTrader.Main.VVM.ViewModels.Factories;

namespace StockTrader.Main.Commands
{
    public class UpdateCurrentViewModelCommand : BaseCommand
    {
        private readonly INavigator _navigator;
        private readonly IStockTraderViewModelFactory _viewModelFactory;

        /// <summary>
        /// UpdateCurrentViewModelCommand constructor to initialize the navigator and view model factory
        /// </summary>
        /// <param name="navigator"></param>
        /// <param name="viewModelFactory"></param>
        public UpdateCurrentViewModelCommand(INavigator navigator, IStockTraderViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        /// <summary>
        /// Method to execute the command to update the current view model based on the parameter
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object? parameter)
        {
            if(parameter is ViewType viewType)
            {
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}
