/*-----------------------------------------------------------------------
// <copyright file="AddStoresHostBuilderExtensions.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the AddStoresHostBuilderExtensions class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the AddStoresHostBuilderExtensions class.
//-----------------------------------------------------------------------*/

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
        /// <summary>
        /// Adds the stores to the host builder
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
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
