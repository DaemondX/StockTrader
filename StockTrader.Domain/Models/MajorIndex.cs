/*-----------------------------------------------------------------------
// <copyright file="MajorIndex.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the MajorIndex class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the MajorIndex class.
//-----------------------------------------------------------------------*/

namespace StockTrader.Domain.Models
{
    public enum MajorIndexType
    {
        DowJones,
        Nasdaq,
        SP500
    }

    public class MajorIndex
    {
        public string IndexName { get; set; }
        public double Price { get; set; }
        public double Changes { get; set; }
        public MajorIndexType MajoxIndexType { get; set; }
    }
}
