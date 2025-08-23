/*-----------------------------------------------------------------------
// <copyright file="MajorIndexListingViewModel.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the MajorIndexListingViewModel class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the MajorIndexListingViewModel class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Models;
using StockTrader.Domain.Services.Interfaces;
using StockTrader.Main.Commands;
using System.Windows.Input;

namespace StockTrader.Main.VVM.ViewModels
{
    public class MajorIndexListingViewModel : BaseViewModel
    {
        private MajorIndex _dowJones;
        private MajorIndex _nasdaq;
        private MajorIndex _sP500;
        public MajorIndex DowJones 
        { 
            get => _dowJones;
            set
            {
                _dowJones = value;
                OnPropertyChanged(nameof(DowJones));
            }
        }
        public MajorIndex Nasdaq 
        { 
            get => _nasdaq;
            set
            {
                _nasdaq = value;
                OnPropertyChanged(nameof(Nasdaq));
            }
        }
        public MajorIndex SP500 
        {
            get => _sP500;
            set
            {
                _sP500 = value;
                OnPropertyChanged(nameof(SP500));
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public ICommand LoadMajorIndexesCommand { get; }
        public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
        {
            LoadMajorIndexesCommand = new LoadMajorIndexesCommand(this, majorIndexService);
        }
        /// <summary>
        /// Factory method to create the MajorIndexViewModel and load the major indexes
        /// </summary>
        /// <param name="majorIndexService"></param>
        /// <returns></returns>
        public static MajorIndexListingViewModel CreateMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexListingViewModel majorIndexViewModel = new MajorIndexListingViewModel(majorIndexService);

            // Load the major indexes 
            majorIndexViewModel.LoadMajorIndexesCommand.Execute(null);

            return majorIndexViewModel;
        }
    }
}
