/*-----------------------------------------------------------------------
// <copyright file="AssetListingViewModel.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the AssetListingViewModel class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the AssetListingViewModel class.
//-----------------------------------------------------------------------*/

using StockTrader.Main.State.Assets;
using System.Collections.ObjectModel;

namespace StockTrader.Main.VVM.ViewModels
{
    public class AssetListingViewModel : BaseViewModel
    {
        private readonly int _assetsCount;
        private readonly AssetStore _assetStore;
        private readonly ObservableCollection<AssetViewModel> _assets;

        public IEnumerable<AssetViewModel> Assets
            => _assets;

        /// <summary>
        /// AssetListingViewModel constructor
        /// </summary>
        /// <param name="assetStore"></param>
        /// <param name="assetsCount"></param>
        public AssetListingViewModel(AssetStore assetStore, int assetsCount = -1)
        {
            _assetStore = assetStore;
            _assetsCount = assetsCount;
            _assets = new ObservableCollection<AssetViewModel>();

            _assetStore.StateChanged += OnStateChanged;

            ResetAssets();
        }

        // When the state changes, we need to update the assets
        private void OnStateChanged()
        {
            ResetAssets();
        }

        // Reset the assets collection
        private void ResetAssets()
        {
            // Get all assets and add them to the collection
            IEnumerable<AssetViewModel> selectedAssets = _assetStore.GetAssetsOrderByDescending(_assetsCount);

            DisposeAssets();
            _assets.Clear();

            foreach (var asset in selectedAssets)
            {
                _assets.Add(asset);
            }
        }

        // Dispose all assets in the collection
        private void DisposeAssets()
        {
            foreach(AssetViewModel assetViewModel in _assets)
            {
                assetViewModel.Dispose();
            }
        }

        // Dispose the view model
        public override void Dispose()
        {
            _assetStore.StateChanged -= OnStateChanged;
            DisposeAssets();
            base.Dispose();
        }
    }
}
