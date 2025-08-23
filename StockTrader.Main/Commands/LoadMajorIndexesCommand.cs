/*-----------------------------------------------------------------------
// <copyright file="LoadMajorIndexesCommand.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the LoadMajorIndexesCommand class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the LoadMajorIndexesCommand class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Models;
using StockTrader.Domain.Services.Interfaces;
using StockTrader.Main.VVM.ViewModels;

namespace StockTrader.Main.Commands
{
    public class LoadMajorIndexesCommand : AsyncCommandBase
    {
        private readonly MajorIndexListingViewModel _majorIndexListingViewModel;
        private readonly IMajorIndexService _majorIndexService;

        /// <summary>
        /// LoadMajorIndexesCommand constructor
        /// </summary>
        /// <param name="majorIndexListingViewModel"></param>
        /// <param name="majorIndexService"></param>
        public LoadMajorIndexesCommand(MajorIndexListingViewModel majorIndexListingViewModel, IMajorIndexService majorIndexService)
        {
            _majorIndexListingViewModel = majorIndexListingViewModel;
            _majorIndexService = majorIndexService;
        }

        /// <summary>
        /// Execute the command to load major indexes
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(object? parameter)
        {
            _majorIndexListingViewModel.IsLoading = true;

            await Task.WhenAll(LoadDowJones(),
                         LoadNasdaq(),
                         LoadSP500());

            _majorIndexListingViewModel.IsLoading = false;
        }

        /// <summary>
        /// Load Dow Jones index
        /// </summary>
        /// <returns></returns>
        private async Task LoadDowJones()
        {
            _majorIndexListingViewModel.DowJones = await _majorIndexService.GetMajorIndexAsync(MajorIndexType.DowJones);
        }

        /// <summary>
        /// Load Nasdaq index
        /// </summary>
        /// <returns></returns>
        private async Task LoadNasdaq()
        {
            _majorIndexListingViewModel.Nasdaq = await _majorIndexService.GetMajorIndexAsync(MajorIndexType.Nasdaq);
        }

        /// <summary>
        /// Load S&P 500 index
        /// </summary>
        /// <returns></returns>
        private async Task LoadSP500()
        {
            _majorIndexListingViewModel.SP500 = await _majorIndexService.GetMajorIndexAsync(MajorIndexType.SP500);
        }
    }
}
