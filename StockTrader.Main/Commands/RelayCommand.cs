/*-----------------------------------------------------------------------
// <copyright file="RelayCommand.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the RelayCommand class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the RelayCommand class.
//-----------------------------------------------------------------------*/

using System.Windows.Input;

namespace StockTrader.Main.Commands
{
    public class RelayCommand : ICommand
    {

        private Action<object> execute;
        private Func<object, bool> canExecute;
        private ICommand? setPurchasesCommand;

        /// <summary>
        /// CanExecuteChanged event to signal when the ability to execute the command changes.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// RelayCommand constructor to initialize the command with execute and canExecute delegates.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// RelayCommand constructor to initialize the command with another ICommand.
        /// </summary>
        /// <param name="setPurchasesCommand"></param>
        public RelayCommand(ICommand? setPurchasesCommand)
        {
            this.setPurchasesCommand = setPurchasesCommand;
        }

        /// <summary>
        /// CanExecute method to determine if the command can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        /// <summary>
        /// Execute method to perform the command action.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
