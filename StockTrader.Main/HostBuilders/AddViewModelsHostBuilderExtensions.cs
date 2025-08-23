/*-----------------------------------------------------------------------
// <copyright file="AddViewModelsHostBuilderExtensions.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the AddViewModelsHostBuilderExtensions class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the AddViewModelsHostBuilderExtensions class.
//-----------------------------------------------------------------------*/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockTrader.Domain.Services.Interfaces;
using StockTrader.Domain.Services.Interfaces.TransactionServices;
using StockTrader.Main.State.Accounts;
using StockTrader.Main.State.Assets;
using StockTrader.Main.State.Authentificators;
using StockTrader.Main.State.Navigators;
using StockTrader.Main.VVM.ViewModels;
using StockTrader.Main.VVM.ViewModels.Factories;

namespace StockTrader.Main.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        /// <summary>
        /// Add the ViewModels to the HostBuilder
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {

                // Register the StockTraderViewModelFactory like Singleton service
                services.AddSingleton<IStockTraderViewModelFactory, StockTraderViewModelFactory>();
                services.AddTransient<BuyViewModel>();
                services.AddTransient<PortfolioViewModel>();
                services.AddTransient<SellViewModel>();
                services.AddTransient<AssetSummaryViewModel>();
                services.AddTransient(CreateHomeViewModel);
                // Register the MainViewModel as a Scoped service
                //services.AddTransient<MainViewModel>(services);
                
                services.AddTransient<MainViewModel>(services =>
                {
                    return new MainViewModel(
                        services.GetRequiredService<INavigator>(),
                        services.GetRequiredService<IAuthenticator>(),
                        services.GetRequiredService<IStockTraderViewModelFactory>(),
                        services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
                });


                services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();

                // Register the CreateViewModel<HomeViewModel> like Singleton service
                // for creating HomeViewModel according the delegate function
                services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());

                // Register the CreateViewModel<PortfolioViewModel> like Singleton service
                // for creating PortfolioViewModel according the delegate function
                services.AddSingleton<CreateViewModel<PortfolioViewModel>>(services => () => services.GetRequiredService<PortfolioViewModel>());

                services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services));

                // Register the CreateViewModel<BuyViewModel> like Singleton service
                services.AddSingleton<CreateViewModel<BuyViewModel>>(servies => () => servies.GetRequiredService<BuyViewModel>());


                //serviceCollection.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
                services.AddSingleton(services =>
                    new ViewModelDelegateRenavigator<HomeViewModel>(
                        services.GetRequiredService<INavigator>(),
                        services.GetRequiredService<CreateViewModel<HomeViewModel>>())
                );

                //serviceCollection.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
                services.AddSingleton(services =>
                    new ViewModelDelegateRenavigator<RegisterViewModel>(
                        services.GetRequiredService<INavigator>(),
                        services.GetRequiredService<CreateViewModel<RegisterViewModel>>()));

                // Register the CreateViewModel<LoginViewModel> like Singleton service
                // for creating LoginViewModel according the delegate function
                services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services));

                // Register the CreateViewModel<BuyViewModel> like Singleton service
                services.AddSingleton<CreateViewModel<BuyViewModel>>(services => () => CreateBuyViewModel(services));
      

                // Register the CreateViewModel<SellViewModel> like Singleton service
                services.AddSingleton<CreateViewModel<SellViewModel>>(servies => () => servies.GetRequiredService<SellViewModel>());
    
            });

            return host;
        }

        private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
        {
            return new RegisterViewModel(
                            services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(),
                            services.GetRequiredService<IAuthenticator>());
        }
        private static BuyViewModel CreateBuyViewModel(IServiceProvider services)
        {
            return new BuyViewModel(services.GetRequiredService<IStockPriceService>(),
                                    services.GetRequiredService<IBuyStockService>(),
                                    services.GetRequiredService<IAccountStore>(),
                                    services.GetRequiredService<AssetStore>());
        }

        private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
        {
            return new LoginViewModel(
                    services.GetRequiredService<IAuthenticator>(),
                    services.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(),
                    services.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>());
        }

        private static PortfolioViewModel CreatePortfolioViewModel(IServiceProvider services)
        {
           return new PortfolioViewModel(services.GetRequiredService<AssetStore>());
        }

        private static HomeViewModel CreateHomeViewModel(IServiceProvider services)
        {
            return new HomeViewModel(
                MajorIndexListingViewModel.CreateMajorIndexViewModel(
                        services.GetRequiredService<IMajorIndexService>()),
                services.GetRequiredService<AssetSummaryViewModel>());
        }
    }
}
