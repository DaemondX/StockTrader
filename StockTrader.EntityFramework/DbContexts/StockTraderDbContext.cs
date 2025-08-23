/*-----------------------------------------------------------------------
// <copyright file="StockTraderDbContext.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the StockTraderDbContext class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the StockTraderDbContext class.
//-----------------------------------------------------------------------*/

using Microsoft.EntityFrameworkCore;
using StockTrader.Domain.Models;

namespace StockTrader.EntityFramework.DbContexts
{
    public class StockTraderDbContext : DbContext
    {
        /*** DbSet is a collection of entities that can be queried from the database ***/

        // DbSet<User> Users will be used to query the Users table
        public DbSet<User> Users { get; set; }

        // DbSet<Account> Accounts will be used to query the Accounts table
        public DbSet<Account> Accounts { get; set; }

        // DbSet<AssetTransaction> AssetTransactions will be used to query the AssetTransactions table
        public DbSet<AssetTransaction> AssetTransactions { get; set; }

        /// <summary>
        /// StockTraderDbContext constructor
        /// </summary>
        /// <param name="options"></param>
        public StockTraderDbContext(DbContextOptions<StockTraderDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Override the OnModelCreating method to configure the model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Owned types (Stock) are special types that are tightly bound to their parent
            // entity (AssetTransaction) and do not have their own identity
            // (they do not have their own key).
            modelBuilder.Entity<AssetTransaction>().OwnsOne(a => a.Asset);

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<AssetTransaction>().HasKey(a => a.Id);
            modelBuilder.Entity<Account>().HasKey(a => a.Id);

            base.OnModelCreating(modelBuilder);

        }
    }
}