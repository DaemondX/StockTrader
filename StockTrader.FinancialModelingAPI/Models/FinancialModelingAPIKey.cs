/*-----------------------------------------------------------------------
// <copyright file="FinancialModelingAPIKey.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the FinancialModelingAPIKey class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the FinancialModelingAPIKey class.
//-----------------------------------------------------------------------*/

namespace StockTrader.FinancialModelingAPI.Models
{
    public class FinancialModelingAPIKey
    {
        public string Key { get; }

        /// <summary>
        /// FinancialModelingAPIKey constructor
        /// </summary>
        /// <param name="key"></param>
        public FinancialModelingAPIKey(string key)
        {
            Key = key;
        }
    }
}
