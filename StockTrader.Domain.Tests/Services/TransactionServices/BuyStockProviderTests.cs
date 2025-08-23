/*-----------------------------------------------------------------------
// <copyright file="BuyStockProviderTests.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the BuyStockProviderTests class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the BuyStockProviderTests class.
//-----------------------------------------------------------------------*/

using Moq;
using NUnit.Framework;
using StockTrader.Common.Interfaces;
using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Models;
using StockTrader.Domain.Services.Interfaces;
using StockTrader.Domain.Services.TransactionProviders;

namespace StockTrader.Domain.Tests.Services.TransactionServices
{
    [TestFixture]
    public class BuyStockProviderTests
    {
        private Mock<IStockPriceService> _mockStockPriceService;
        private Mock<ICommonRepository<Account>> _mockAccountRepository;
        private BuyStockProvider _buyStockProvider;

        /// <summary>
        /// Set up the test environment before each test
        /// </summary>
        [SetUp] 
        public void SetUp() 
        {
            _mockStockPriceService = new Mock<IStockPriceService>();
            _mockAccountRepository = new Mock<ICommonRepository<Account>>();
            _buyStockProvider = new BuyStockProvider(_mockStockPriceService.Object, _mockAccountRepository.Object);
        }

        /// <summary>
        /// BuyStock should throw ArgumentNullException when buyer is null
        /// </summary>
        [Test]
        public void BuyStock_WithNullBuyer_ThrowsArgumentNullException()
        {
            // Arrange
            Account? buyer = null;
            string stockSymbol = "MSFT", 
                   expectedExceptionMessage = "Buyer is not set";
            int shares = 10;

            // Act 
            ArgumentNullException actualException = Assert.ThrowsAsync<ArgumentNullException>(()
                => _buyStockProvider.BuyStockAsync(buyer, stockSymbol, shares));

            // Assert
            StringAssert.Contains(expectedExceptionMessage, actualException.Message);
        }

        /// <summary>
        /// BuyStock should throw ArgumentNullException when stock is null
        /// </summary>
        [Test]
        public void BuyStock_WithNullStock_ThrowsArgumentNullException()
        {
            // Arrange
            Account buyer = new Account 
            { 
                AccountHolder = new User 
                { 
                    Username = "test", Email = "test@email", PasswordHash = "password" 
                }
            };
            string expectedExceptionMessage = "Stock symbol is not set";
            int shares = 10;

            // Act 
            ArgumentNullException actualException = Assert.ThrowsAsync<ArgumentNullException>(()
                => _buyStockProvider.BuyStockAsync(buyer, It.IsAny<string>(), shares));

            // Assert
            StringAssert.Contains(expectedExceptionMessage, actualException.Message);
        }

        /// <summary>
        /// ByStock should throw ArgumentOutOfRangeException when shares is less than 1
        /// </summary>
        [Test]
        public void BuyStock_WithSharesLessThan1_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            Account buyer = new Account
            {
                AccountHolder = new User
                {
                    Username = "test",
                    Email = "test@email",
                    PasswordHash = "password"
                }
            };
            string expectedExceptionMessage = "Shares must be greater than 0", 
                   stock = "MSFT";
            int invalidShares = 0;

            // Act 
            ArgumentOutOfRangeException actualException = Assert.ThrowsAsync<ArgumentOutOfRangeException>(()
                => _buyStockProvider.BuyStockAsync(buyer, stock, invalidShares));

            // Assert
            StringAssert.Contains(expectedExceptionMessage, actualException.Message);
        }

        /// <summary>
        /// BuyStock should throw InsufficientBalanceOfMoney when the result balance is less than 1
        /// </summary>
        [Test]
        public void BuyStock_WithResultBalanceLessThan1_ThrowsInsufficientBalanceOfMoney()
        {
            // Arrange
            Account buyer = new Account
            {
                AccountHolder = new User
                {
                    Username = "test",
                    Email = "test@email",
                    PasswordHash = "password"
                },

                Balance = 50

            };
            string expectedExceptionMessage = "Result balance has to be more than 0",
                   stock = "MSFT";
            int validShares = 1;
            double returnPrice = 100.0;

            _mockStockPriceService.Setup(setup => setup.GetPriceAsync(stock)).ReturnsAsync(returnPrice);

            // Act 
            InsufficientBalanceOfMoney actualException = Assert.ThrowsAsync<InsufficientBalanceOfMoney>(()
                => _buyStockProvider.BuyStockAsync(buyer, stock, validShares));

            // Assert
            StringAssert.Contains(expectedExceptionMessage, actualException.Message);
        }

        /// <summary>
        /// BuyStock should return the buyer when the parameters are correct
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task BuyStock_WithCorrectParameters_ReturnBuyer()
        {
            // Arrange
            Account buyer = new Account
            {
                AccountHolder = new User
                {
                    Username = "test",
                    Email = "test@email.com",
                    PasswordHash = "password"
                },

                Balance = 5000,
                AssetTransactions = new List<AssetTransaction>()

            };
            string expectedExceptionMessage = "Result balance has to be more than 0",
                   stock = "MSFT";
            int validShares = 3;
            double returnPrice = 100.0, expectedBalance = 5000 - (validShares * returnPrice);
            // Setup the mock to return the price
            _mockStockPriceService.Setup(setup => setup.GetPriceAsync(stock)).ReturnsAsync(returnPrice);
            // Setup the mock to return the buyer
            _mockAccountRepository.Setup(setup => setup.UpdateAsync(It.IsAny<int>(), buyer)).ReturnsAsync(buyer);

            // Act 
            Account returnAccount = await _buyStockProvider.BuyStockAsync(buyer, stock, validShares);

            // Assert
            Assert.IsNotNull(returnAccount);
            Assert.AreEqual(returnAccount.Balance, expectedBalance);
        }
    }
}
