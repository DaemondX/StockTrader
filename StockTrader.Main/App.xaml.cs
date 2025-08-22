using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockTrader.EntityFramework.DbContexts;
using StockTrader.Main.HostBuilders;
using StockTrader.Main.VVM.ViewModels;
using System.Configuration;
using System.Windows;

namespace StockTrader.Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ConfigurationErrorsException"></exception>
        private static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                .AddFinanceAPI()
                .AddDbContext()
                .AddServices()
                .AddStores()
                .AddViewModels();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            // Start the host
            _host.Start();

            // Migrate the database 
            //StockTraderDbContextFactory contextFactory = _host.Services.GetRequiredService<StockTraderDbContextFactory>();
            //using(var context = contextFactory.CreateDbContext())
            //{
            //    context.Database.Migrate();
            //}

            // Create the startup window
            Window window = new MainWindow()
            {
                DataContext = _host.Services.GetRequiredService<MainViewModel>()
            };

            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            // Stop the host
            await _host.StopAsync();
            // Dispose the host
            _host.Dispose();

            base.OnExit(e);
        }
    }
}