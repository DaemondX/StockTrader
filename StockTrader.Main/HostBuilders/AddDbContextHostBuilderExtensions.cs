/*-----------------------------------------------------------------------
// <copyright file="AddDbContextHostBuilderExtensions.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the AddDbContextHostBuilderExtensions class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the AddDbContextHostBuilderExtensions class.
//-----------------------------------------------------------------------*/

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockTrader.EntityFramework.DbContexts;
using System.Configuration;

namespace StockTrader.Main.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        /// <summary>
        /// Extension method to add the DbContext to the host builder
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                // Get the connection string from the configuration file and register the StockTraderDbContext
                string? connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ConfigurationErrorsException("DefaultConnection is not set");
                }
                // Set Action for the DbContextOptionsBuilder with the connection string
                Action<DbContextOptionsBuilder> optionsAction = options =>
                        options.UseSqlServer(connectionString);

                services.AddDbContext<StockTraderDbContext>(optionsAction);
                // Register for the dbContext factory
                services.AddSingleton<StockTraderDbContextFactory>(new StockTraderDbContextFactory(optionsAction));
            });

            return host;
        }
    }    
}
