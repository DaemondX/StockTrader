/*-----------------------------------------------------------------------
// <copyright file="User.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the User class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the User class.
//-----------------------------------------------------------------------*/

using StockTrader.Common.Models;

namespace StockTrader.Domain.Models
{
    public class User : CommonObject
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PCName { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.Now;
    }
}
