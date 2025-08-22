using StockTrader.Main.VVM.ViewModels;
using System.Windows.Input;

namespace StockTrader.Main.State.Navigators
{
    public enum ViewType
    {
        Home,
        Portfolio,
        Buy,
        Sell,
        Login
    }

    public interface INavigator
    {
        BaseViewModel CurrentViewModel { get; set; }
        event Action StateChanged; 
    }
}
