/*-----------------------------------------------------------------------
// <copyright file="AddFinanceAPIHostBuilderExtensions.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the AddFinanceAPIHostBuilderExtensions class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the AddFinanceAPIHostBuilderExtensions class.
//-----------------------------------------------------------------------*/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockTrader.FinancialModelingAPI;
using StockTrader.FinancialModelingAPI.Models;
using System.Configuration;

namespace StockTrader.Main.HostBuilders
{
    public static class AddFinanceAPIHostBuilderExtensions
    {
        /// <summary>
        /// Add the FinancialModelingHttpClient to the host builder
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        /// <exception cref="ConfigurationErrorsException"></exception>
        public static IHostBuilder AddFinanceAPI(this IHostBuilder host)
        {
            host.ConfigureServices((context,services) =>
            {
                // Get the API key from the configuration file and register the FinancialModelingHttpClientFactory
                string? apiKey = context.Configuration.GetValue<string>("FINANCE_API_KEY");
                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new ConfigurationErrorsException("financialApiKey is not set");
                }

                services.AddSingleton(new FinancialModelingAPIKey(apiKey));

                services.AddHttpClient<FinancialModelingHttpClient>(client =>
                {
                    client.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
                });
            });

            return host;
        }
    }
}
