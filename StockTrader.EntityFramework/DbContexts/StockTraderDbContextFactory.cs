/*-----------------------------------------------------------------------
// <copyright file="StockTraderDbContextFactory.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the StockTraderDbContextFactory class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the StockTraderDbContextFactory class.
//-----------------------------------------------------------------------*/

using Microsoft.EntityFrameworkCore;

namespace StockTrader.EntityFramework.DbContexts
{
    public class StockTraderDbContextFactory
    {
        // Action delegate that takes DbContextOptionsBuilder as parameter
        private readonly Action<DbContextOptionsBuilder> _optionsAction;

        /// <summary>
        /// StockTraderDbContextFactory constructor
        /// </summary>
        /// <param name="optionsAction"></param>
        public StockTraderDbContextFactory(Action<DbContextOptionsBuilder> optionsAction)
        {
            _optionsAction = optionsAction;
        }

        /// <summary>
        /// Create a new instance of StockTraderDbContext
        /// </summary>
        /// <param name="args"></param>
        /// <returns> StockTraderDbContext with connection to the local database StockTraderDb </returns>
        public StockTraderDbContext CreateDbContext()
        {
            // Create a new instance of DbContextOptionsBuilder<StockTraderDbContext>
            var options = new DbContextOptionsBuilder<StockTraderDbContext>();
            // Call the delegate with the options
            _optionsAction(options);

            return new StockTraderDbContext(options.Options);
        }
    }
}
