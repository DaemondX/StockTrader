/*-----------------------------------------------------------------------
// <copyright file="AssetSummaryViewModel.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the AssetSummaryViewModel class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the AssetSummaryViewModel class.
//-----------------------------------------------------------------------*/

using StockTrader.Main.State.Assets;

namespace StockTrader.Main.VVM.ViewModels
{
    public class AssetSummaryViewModel : BaseViewModel
    {
        public TopAssetSummaryViewModel TopAssetSummaryViewModel { get; }

        /// <summary>
        /// AssetSummaryViewModel constructor
        /// </summary>
        /// <param name="assetStore"></param>
        public AssetSummaryViewModel(AssetStore assetStore)
        {
            TopAssetSummaryViewModel = new TopAssetSummaryViewModel(assetStore);
        }

        // Dispose pattern to clean up resources
        public override void Dispose()
        {
            TopAssetSummaryViewModel?.Dispose();

            base.Dispose();
        }
    }
}
