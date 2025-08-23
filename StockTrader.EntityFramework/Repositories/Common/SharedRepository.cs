/*-----------------------------------------------------------------------
// <copyright file="SharedRepository.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the SharedRepository class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the SharedRepository class.
//-----------------------------------------------------------------------*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StockTrader.Common.Models;
using StockTrader.EntityFramework.DbContexts;

namespace StockTrader.EntityFramework.Repositories.Common
{
    public class SharedRepository<T> where T : CommonObject
    {
        // DesignTimeStockTraderDbContextFactory instance
        private readonly StockTraderDbContextFactory _contextFactory;

        /// <summary>
        /// SharedRepository constructor
        /// </summary>
        /// <param name="contextFactory"></param>
        public SharedRepository(StockTraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// CreateAsync method that adds an entity to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> CreateAsync(T entity)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Add the entity to the database
                        EntityEntry<T> createdResult = await context.Set<T>()
                            .AddAsync(entity);

                        // Save the changes
                        await context.SaveChangesAsync();
                        // Commit the transaction
                        await transaction.CommitAsync();

                        // return the entity
                        return createdResult.Entity;
                    }
                    catch (DbUpdateException ex)
                    {
                        await transaction.RollbackAsync();
                        // maybe log the exception
                        throw new Exception("An error occurred while updating the database.");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        // maybe log the exception
                        throw new Exception("An error occurred while creating an entity.");
                    }
                }
            }
        }

        /// <summary>
        /// Async method that deletes an entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if was successfully</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteAsync(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Find the entity by id
                        T? entity = await context.Set<T>()
                            .FirstOrDefaultAsync(e => e.Id == id);

                        if (entity != null)
                        {
                            // Remove the entity from the database
                            context.Set<T>().Remove(entity);
                            await context.SaveChangesAsync();

                            await transaction.CommitAsync();

                            return true;
                        }

                        return false;
                    }
                    catch (DbUpdateException)
                    {
                        await transaction.RollbackAsync();
                        // maybe log the exception ...
                        throw new Exception("An error occurred while updating the database.");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        // maybe log the exception- ...
                        throw new Exception("An error occurred while creating an entity.");
                    }
                }
            }
        }

        /// <summary>
        /// UpdateAsync method that updates an entity in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<T?> UpdateAsync(int id, T entity)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        T? findEntity = await context.Set<T>()
                            .FirstOrDefaultAsync(e => e.Id == id);

                        if (findEntity != null)
                        {
                            // Detach the entity from the context
                            context.Entry(findEntity).State = EntityState.Detached;

                            entity.Id = id;

                            EntityEntry<T> updatedResult = context.Set<T>()
                                    .Update(entity);


                            await context.SaveChangesAsync();
                            await transaction.CommitAsync();

                            return updatedResult.Entity;
                        }

                        return null;
                    }
                    catch (DbUpdateException)
                    {
                        await transaction.RollbackAsync();
                        // maybe log the exception ...
                        throw new Exception("An error occurred while updating the database.");
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        // maybe log the exception- ...
                        throw new Exception("An error occurred while creating an entity.");
                    }
                }
            }
        }
    }
}
