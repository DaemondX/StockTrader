using Microsoft.EntityFrameworkCore;

namespace StockTrader.EntityFramework.DbContexts
{
    public class StockTraderDbContextFactory
    {
        // Action delegate that takes DbContextOptionsBuilder as parameter
        private readonly Action<DbContextOptionsBuilder> _optionsAction;

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
