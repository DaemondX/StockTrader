/*-----------------------------------------------------------------------
// <copyright file="AccountRepository.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the AccountRepository class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the AccountRepository class.
//-----------------------------------------------------------------------*/

using Microsoft.EntityFrameworkCore;
using StockTrader.Domain.Models;
using StockTrader.Domain.Services.Interfaces;
using StockTrader.EntityFramework.DbContexts;
using StockTrader.EntityFramework.Repositories.Common;

namespace StockTrader.EntityFramework.Repositories
{
    public class AccountRepository : IAccountService
    {
        // DesignTimeStockTraderDbContextFactory instance
        private readonly StockTraderDbContextFactory _contextFactory;

        // SharedRepository instance
        private readonly SharedRepository<Account> _sharedRepository;

        /// <summary>
        /// AccountRepository constructor that initializes the context factory and shared repository
        /// </summary>
        /// <param name="contextFactory"></param>
        public AccountRepository(StockTraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _sharedRepository = new SharedRepository<Account>(contextFactory);
        }

        /// <summary>
        /// CreateAsync method that adds an entity to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Account> CreateAsync(Account entity)
            => await _sharedRepository.CreateAsync(entity);

        /// <summary>
        /// Async method that deletes an entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if was successfully</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteAsync(int id)
            => await _sharedRepository.DeleteAsync(id);

        /// <summary>
        /// Async method that gets all entities from the database without any conditions
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                try
                {
                    // Eager Loading
                    IEnumerable<Account> entities = await context.Accounts
                        .Include(a => a.AccountHolder)
                        .Include(a => a.AssetTransactions)
                        .ToListAsync();

                    return entities;
                }
                catch (DbUpdateException)
                {
                    // maybe log the exception ...
                    throw new Exception("An error occurred while updating the database.");
                }
                catch (Exception)
                {
                    // maybe log the exception- ...
                    throw new Exception("An error occurred while creating an entity.");
                }
            }
        }


        /// <summary>
        /// Async method that gets an entity by id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>entity when it exists, otherwise null</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Account?> GetByIdAsync(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                try
                {
                    // Eager Loading
                    Account? entity = await context.Accounts
                        .Include(a => a.AccountHolder)
                        .Include(a => a.AssetTransactions)
                        .FirstOrDefaultAsync(e => e.Id == id);

                    if (entity != null)
                    {
                        return entity;
                    }

                    return null;

                }
                catch (DbUpdateException)
                {
                    // maybe log the exception ...
                    throw new Exception("An error occurred while updating the database.");
                }
                catch (Exception)
                {
                    // maybe log the exception- ...
                    throw new Exception("An error occurred while creating an entity.");
                }
            }
        }

        /// <summary>
        /// Get an account by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Account?> GetByEmail(string email)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                try
                {
                    // Eager Loading
                    Account? entity = await context.Accounts
                        .Include(a => a.AccountHolder)
                        .Include(a => a.AssetTransactions)
                        .FirstOrDefaultAsync(e => e.AccountHolder.Email == email);  

                    if (entity != null)
                    {
                        return entity;
                    }

                    return null;

                }
                catch (Exception)
                {
                    // maybe log the exception- ...
                    throw new Exception("An error occurred while searching an entity by email.");
                }
            }
        }

        /// <summary>
        /// Get an account by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Account?> GetByUserName(string username)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                try
                {
                    // Eager Loading
                    Account? entity = await context.Accounts
                        .Include(a => a.AccountHolder)
                        .Include(a => a.AssetTransactions)
                        .FirstOrDefaultAsync(e => e.AccountHolder.Username == username);

                    if (entity != null)
                    {
                        return entity;
                    }

                    return null;

                }
                catch (Exception)
                {
                    // maybe log the exception- ...
                    throw new Exception("An error occurred while searching an entity by username.");
                }
            }
        }

        /// <summary>
        /// Get a user by PC name and id
        /// </summary>
        /// <param name="pcName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<User?> GetByPCName(string pcName, int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                try
                {
                    // Eager Loading
                    User? entity = await context.Users
                        .FirstOrDefaultAsync(e => e.PCName == pcName && e.Id == id);

                    if (entity != null)
                    {
                        return entity;
                    }
                    return null;
                }
                catch (Exception)
                {
                    // maybe log the exception- ...
                    throw new Exception("An error occurred while searching an entity by PCName.");
                }
            }
        }

        /// <summary>
        /// UpdateAsync method that updates an entity in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Account?> UpdateAsync(int id, Account entity)
         => await _sharedRepository.UpdateAsync(id, entity);
    }
}
