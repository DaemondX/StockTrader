/*-----------------------------------------------------------------------
// <copyright file="HomeViewModel.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the HomeViewModel class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the HomeViewModel class.
//-----------------------------------------------------------------------*/

namespace StockTrader.Main.VVM.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public AssetSummaryViewModel AssetSummaryViewModel { get; }
        public MajorIndexListingViewModel MajorIndexListingViewModel { get;  }

        /// <summary>
        /// HomeViewModel constructor
        /// </summary>
        /// <param name="majorIndexViewModel"></param>
        /// <param name="assetSummaryViewModel"></param>
        public HomeViewModel(MajorIndexListingViewModel majorIndexViewModel, AssetSummaryViewModel assetSummaryViewModel)
        {
            MajorIndexListingViewModel = majorIndexViewModel;
            AssetSummaryViewModel = assetSummaryViewModel;
        }

        // Dispose the view models
        public override void Dispose()
        {
            AssetSummaryViewModel?.Dispose();
            MajorIndexListingViewModel?.Dispose();

            base.Dispose();
        }
    }
}
