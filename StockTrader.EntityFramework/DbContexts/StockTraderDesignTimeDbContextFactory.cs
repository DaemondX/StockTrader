using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace StockTrader.EntityFramework.DbContexts
{
    public class StockTraderDesignTimeDbContextFactory : IDesignTimeDbContextFactory<StockTraderDbContext>
    {
        public StockTraderDbContext CreateDbContext(string[] args)
        {
            // Get configuration from the appsettings.json file
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Set the base path to the current directory
                .AddJsonFile("appsettings.json"); // Add the appsettings.json file

            // Get the connection string from the configuration
            string? connectionString = configurationBuilder.Build().GetConnectionString("DefaultConnection"); 
            if(configurationBuilder == null)
            {
                throw new Exception("Could not find the connection string 'DefaultConnection'");
            }

            // Create a new instance of DbContextOptionsBuilder with the connection string
            var optionsBuilder = new DbContextOptionsBuilder<StockTraderDbContext>()
                .UseSqlServer(connectionString); // Use the SQLite connection string

            // Return a new instance of StockTraderDbContext with the options
            return new StockTraderDbContext(optionsBuilder.Options);

        }
    }
}
