/*-----------------------------------------------------------------------
// <copyright file="AddConfigurationHostBuilderExtensions.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the AddConfigurationHostBuilderExtensions class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the AddConfigurationHostBuilderExtensions class.
//-----------------------------------------------------------------------*/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace StockTrader.Main.HostBuilders
{
    public static class AddConfigurationHostBuilderExtensions
    {
        /// <summary>
        /// Extension method to add the appsettings.json file and the environment variables to the configuration
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder AddConfiguration(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration(c =>
            {
                // Add the appsettings.json file and the environment variables to the configuration
                c.AddJsonFile("appsettings.json");
                c.AddEnvironmentVariables();
            });

            return hostBuilder;
        }
    }
}
