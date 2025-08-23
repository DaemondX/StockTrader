/*-----------------------------------------------------------------------
// <copyright file="EqualValueToParameterConverter.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the EqualValueToParameterConverter class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the EqualValueToParameterConverter class.
//-----------------------------------------------------------------------*/

using StockTrader.Main.VVM.ViewModels;
using System.Globalization;
using System.Windows.Data;

namespace StockTrader.Main.Converters
{
    public class EqualValueToParameterConverter : IValueConverter
    {
        /// <summary>
        /// Convert a value to a boolean indicating if it equals the parameter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is BaseViewModel baseViewModel && 
                parameter != null)
            {
                if(parameter.ToString()
                    .Equals(baseViewModel
                        .GetType().Name))
                {
                       return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Convert back is not implemented
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
