using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockTrader.Main.State.Accounts;
using StockTrader.Main.State.Assets;
using StockTrader.Main.State.Authenticators;
using StockTrader.Main.State.Authentificators;
using StockTrader.Main.State.Navigators;

namespace StockTrader.Main.HostBuilders
{
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IAccountStore, AccountStore>();
                services.AddSingleton<AssetStore>();
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<IAuthenticator, Authenticator>();
            });

            return host;
        }
    }
}
