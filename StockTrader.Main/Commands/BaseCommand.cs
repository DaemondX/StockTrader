/*-----------------------------------------------------------------------
// <copyright file="BaseCommand.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the BaseCommand class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the BaseCommand class.
//-----------------------------------------------------------------------*/

using System.Windows.Input;

namespace StockTrader.Main.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// OnRaiseCanExecuteChanged method to raise the CanExecuteChanged event
        /// </summary>
        public void OnRaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// CanExecute method to determine if the command can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public virtual bool CanExecute(object? parameter)
            => true;

        /// <summary>
        /// Execute method to execute the command
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object? parameter);
    }
}
