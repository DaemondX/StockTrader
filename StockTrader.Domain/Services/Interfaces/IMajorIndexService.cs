/*-----------------------------------------------------------------------
// <copyright file="IMajorIndexService.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the IMajorIndexService class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the IMajorIndexService class.
//-----------------------------------------------------------------------*/

using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services.Interfaces
{
    public interface IMajorIndexService
    {
        /// <summary>
        /// Get the major index by its type
        /// </summary>
        /// <param name="majorIndexType"></param>
        /// <returns></returns>
        Task<MajorIndex> GetMajorIndexAsync(MajorIndexType majorIndexType);
    }
}
