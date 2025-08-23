/*-----------------------------------------------------------------------
// <copyright file="BaseRepository.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the BaseRepository class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the BaseRepository class.
//-----------------------------------------------------------------------*/

using Microsoft.EntityFrameworkCore;
using StockTrader.Common.Interfaces;
using StockTrader.Common.Models;
using StockTrader.EntityFramework.DbContexts;
using StockTrader.EntityFramework.Repositories.Common;

namespace StockTrader.EntityFramework.Repositories
{
    /* 
     * Base repository class that implements the IBaseRepository interface
     * Contains CRUD operations for the entity and some additional methods
     */
    public class BaseRepository<T> : ICommonRepository<T> where T : CommonObject
    {
        // DesignTimeStockTraderDbContextFactory instance
        private readonly StockTraderDbContextFactory _contextFactory;
        // SharedRepository instance
        private readonly SharedRepository<T> _sharedRepository;

        /// <summary>
        /// BaseRepository constructor that takes a DesignTimeStockTraderDbContextFactory instance
        /// </summary>
        /// <param name="contextFactory"></param>
        public BaseRepository(StockTraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _sharedRepository = new SharedRepository<T>(contextFactory);
        }

        /// <summary>
        /// CreateAsync method that adds an entity to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> CreateAsync(T entity)
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
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                try
                {
                    IEnumerable<T> entities = await context.Set<T>()
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
        public async Task<T?> GetByIdAsync(int id)
        {
            using(var context = _contextFactory.CreateDbContext())
            {
                try
                {
                    T? entity = await context.Set<T>()
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
        /// Async method that updates an entity in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T?> UpdateAsync(int id, T entity)
         => await _sharedRepository.UpdateAsync(id, entity);
    }
}
