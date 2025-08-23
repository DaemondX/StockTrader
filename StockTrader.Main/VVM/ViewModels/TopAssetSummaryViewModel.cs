/*-----------------------------------------------------------------------
// <copyright file="TopAssetSummaryViewModel.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the TopAssetSummaryViewModel class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the TopAssetSummaryViewModel class.
//-----------------------------------------------------------------------*/

using StockTrader.Main.State.Assets;

namespace StockTrader.Main.VVM.ViewModels
{
    public class TopAssetSummaryViewModel : BaseViewModel
    {
        private readonly AssetStore _assetStore;
        public double AccountBalance
            => _assetStore.AccountBalance;

        public AssetListingViewModel AssetListingViewModel { get; }

        /// <summary>
        /// TopAssetSummaryViewModel constructor
        /// </summary>
        /// <param name="assetStore"></param>
        public TopAssetSummaryViewModel(AssetStore assetStore)
        {
            _assetStore = assetStore;
            AssetListingViewModel = new AssetListingViewModel(_assetStore, 3);

            assetStore.StateChanged += AssetStoreChanged;
        }

        private void AssetStoreChanged()
        {
            // Raise the Property changed event for the AccountBalance property
            OnPropertyChanged(nameof(AccountBalance));
        }

        public override void Dispose()
        {
            _assetStore.StateChanged -= AssetStoreChanged;
            AssetListingViewModel?.Dispose();
            base.Dispose();
        }
    }
}
