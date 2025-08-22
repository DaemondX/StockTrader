using StockTrader.Main.State.Assets;
using System.Collections.ObjectModel;

namespace StockTrader.Main.VVM.ViewModels
{
    public class AssetSummaryViewModel : BaseViewModel
    {
        public TopAssetSummaryViewModel TopAssetSummaryViewModel { get; }
        public AssetSummaryViewModel(AssetStore assetStore)
        {
            TopAssetSummaryViewModel = new TopAssetSummaryViewModel(assetStore);
        }

        public override void Dispose()
        {
            TopAssetSummaryViewModel?.Dispose();

            base.Dispose();
        }
    }
}
