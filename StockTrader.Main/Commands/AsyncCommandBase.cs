/*-----------------------------------------------------------------------
// <copyright file="AsyncCommandBase.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the AsyncCommandBase class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the AsyncCommandBase class.
//-----------------------------------------------------------------------*/

using System.Windows.Input;

namespace StockTrader.Main.Commands
{
    public abstract class AsyncCommandBase : ICommand
    {
        public bool _isExecuting;
        public bool IsExecuting
        {
            get => _isExecuting;
            set
            {
                _isExecuting = value;
                OnRaiseCanExecuteChanged();
            }
        }

        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// OnRaiseCanExecuteChanged method to raise the CanExecuteChanged event
        /// </summary>
        public void OnRaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, System.EventArgs.Empty);
        }

        /// <summary>
        /// CanExecute method to determine if the command can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public virtual bool CanExecute(object? parameter)
        {
            return !IsExecuting;
        }

        /// <summary>
        /// Execute method to execute the command asynchronously
        /// </summary>
        /// <param name="parameter"></param>
        public async void Execute(object? parameter)
        {
            IsExecuting = true;

            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception)
            {

                throw;
            }

            IsExecuting = false;
        }

        /// <summary>
        /// ExecuteAsync method to be implemented by derived classes to execute the command logic
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public abstract Task ExecuteAsync(object? parameter);
    }
}
