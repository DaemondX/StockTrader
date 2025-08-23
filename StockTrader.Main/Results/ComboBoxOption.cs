/*-----------------------------------------------------------------------
// <copyright file="ComboBoxOption.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the ComboBoxOption class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the ComboBoxOption class.
//-----------------------------------------------------------------------*/

namespace StockTrader.Main.Results
{
    /// <summary>
    /// Class representing an option in a ComboBox with text, image source, and a number.
    /// </summary>
    public class ComboBoxOption
    {
        public string Text { get; set; }
        public string ImageSource { get; set; }
        public int Number { get; set; }
    }
}
