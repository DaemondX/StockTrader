using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockTrader.Common.Interfaces;
using StockTrader.Domain.Models;
using StockTrader.Domain.Services.AuthentificationServices;
using StockTrader.Domain.Services.AuthentificationServices.Interfaces;
using StockTrader.Domain.Services.Interfaces;
using StockTrader.Domain.Services.Interfaces.TransactionServices;
using StockTrader.Domain.Services.TransactionProviders;
using StockTrader.EntityFramework.Repositories;
using StockTrader.FinancialModelingAPI.Services;

namespace StockTrader.Main.HostBuilders
{

    /*
                   *    **************       Services      **************** 
    */
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                // Register the ICommonRepository<Account> and AccountRepository like Singleton service
                services.AddSingleton<ICommonRepository<Account>, AccountRepository>();
                // Register the IAccountService and AccountRepository like Singleton service
                services.AddSingleton<IAccountService, AccountRepository>();
                // Register the ICommonRepository<Account> and AccountRepository like Singleton service
                services.AddSingleton<IAuthenticationServices, AuthentificationProvider>();
                // Register IStockPriceService and StockPriceProvider like Singleton service
                services.AddSingleton<IStockPriceService, StockPriceProvider>();
                // Register IBuyStockService and BuyStockProvider like Singleton service
                services.AddSingleton<IBuyStockService, BuyStockProvider>();
                // Register the IMajorIndexService and MajorIndexProvider like Singleton service
                services.AddSingleton<IMajorIndexService, MajorIndexProvider>();
                // Register the PasswordHasher like Singleton service for hashing
                services.AddSingleton<IPasswordHasher, PasswordHasher>();
                services.AddSingleton<ISellStockService, SellStockProvider>();
            });

            return host;
        }
    }
}
