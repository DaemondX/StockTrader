using StockTrader.Main.State.Navigators;

namespace StockTrader.Main.VVM.ViewModels.Factories
{
    public interface IStockTraderViewModelFactory
    {
        BaseViewModel CreateViewModel(ViewType viewType);
    }
}
