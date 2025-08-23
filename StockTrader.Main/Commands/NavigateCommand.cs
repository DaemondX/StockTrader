/*-----------------------------------------------------------------------
// <copyright file="NavigateCommand.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the NavigateCommand class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the NavigateCommand class.
//-----------------------------------------------------------------------*/

using StockTrader.Main.State.Navigators;

namespace StockTrader.Main.Commands
{
    public class NavigateCommand : BaseCommand
    {
        private readonly IRenavigator _renavigator;

        /// <summary>
        /// NavigateCommand constructor
        /// </summary>
        /// <param name="renavigator"></param>
        public NavigateCommand(IRenavigator renavigator)
        {
            _renavigator = renavigator;
        }

        /// <summary>
        /// CanExecute method to determine if the command can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }

        /// <summary>
        /// Execute method to perform the navigation
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object? parameter)
        {
            _renavigator.Renavigate();
        }
    }
}
