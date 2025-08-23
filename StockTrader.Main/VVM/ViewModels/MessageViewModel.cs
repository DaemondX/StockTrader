/*-----------------------------------------------------------------------
// <copyright file="MessageViewModel.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the MessageViewModel class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the MessageViewModel class.
//-----------------------------------------------------------------------*/

namespace StockTrader.Main.VVM.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        private string _messsage = string.Empty;
        public string Message
        {
            get => _messsage;
            set
            {
                _messsage = value;
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(HasMessage));
            }
        }

        public bool HasMessage
            => !string.IsNullOrEmpty(Message);

    }
}
