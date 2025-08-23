/*-----------------------------------------------------------------------
// <copyright file="AuthentificationProviderTests.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the AuthentificationProviderTests class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the AuthentificationProviderTests class.
//-----------------------------------------------------------------------*/

using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Models;
using StockTrader.Domain.Services.AuthentificationServices;
using StockTrader.Domain.Services.AuthentificationServices.Interfaces;
using StockTrader.Domain.Services.Interfaces;

namespace StockTrader.Domain.Tests.Services.AuthentificationServices
{
    [TestFixture]
    public class AuthentificationProviderTests
    {
        private Mock<IAccountService> _mockAccountService;
        private Mock<IPasswordHasher> _mockPasswordHasher;
        private AuthentificationProvider _authenticationProvider;

        /// <summary>
        /// Set up the test environment before each test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _authenticationProvider = new AuthentificationProvider(_mockAccountService.Object, _mockPasswordHasher.Object);
        }

        /// <summary>
        /// Login with correct password for existing username should return account for correct username
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Login_WithCorrectPasswordForExistingUsername_ShouldReturnAccountForCorrectUsername()
        {
            // Arrange
            string expectedUsername = "testRegister", 
                password = "testRegister123";

            // Setup the mock account service to return an account with the expected username
            _mockAccountService.Setup(service => service.GetByUserName(expectedUsername))
                .ReturnsAsync(new Account { AccountHolder = new User { Username = expectedUsername } });
            // Setup the mock password hasher to return a successful password verification
            _mockPasswordHasher.Setup(service => service.VerifyHashedPassword(It.IsAny<string>(), password))
                .Returns(PasswordVerificationResult.Success);

            // Act
            Account actualAccount = await _authenticationProvider.Login(expectedUsername, password);

            // Assert 
            string actualUsername = actualAccount.AccountHolder.Username;
            Assert.AreEqual(actualUsername, expectedUsername);
        }

        /// <summary>
        /// Login with incorrect password for existing username should throw InvalidPasswordException
        /// </summary>
        [Test]
        public void Login_WithInCorrectPasswordForExistingUsername_ThrowsInvalidPasswordException()
        {
            // Arrange
            string expectedUsername = "testRegister", 
                password = "testRegister";

            // Setup the mock account service to return an account with the expected username
            _mockAccountService.Setup(service => service.GetByUserName(expectedUsername))
                .ReturnsAsync(new Account { AccountHolder = new User { Username = expectedUsername } });
            // Setup the mock password hasher to return a failed password verification
            _mockPasswordHasher.Setup(service => service.VerifyHashedPassword(It.IsAny<string>(), password))
                .Returns(PasswordVerificationResult.Failed);

            // Act
            InvalidPasswordException exception = Assert.ThrowsAsync<InvalidPasswordException>(()    
                =>  _authenticationProvider.Login(expectedUsername, password));

            // Assert
            string actualUsername = exception.Username;
            Assert.AreEqual(actualUsername, expectedUsername);
        }

        /// <summary>
        /// Login with non-existing username should throw UserNotFoundException
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Login_WithNonExistingUsername_ThrowsUserNotFoundException()
        {
            // Arrange
            string expectedUsername = "IAmNotInTheSystemYet123",
                password = "testRegister";

            // Act
            UserNotFoundException globalException = Assert.ThrowsAsync<UserNotFoundException>(() 
                => _authenticationProvider.Login(expectedUsername, password));

            // Assert
            string actualUsername = globalException.Username;
            Assert.AreEqual(actualUsername, expectedUsername);
        }

        /// <summary>
        /// Register with non-matching passwords should return PasswordDoNotMatch
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Register_WithNonMatchingPasswords_ReturnsPasswordDoNotMatch()
        {
            // Arrange
            string password = "IAmNotInTheSystemYet123", 
                confirmPassword = "doesNotMatch";
            RegistrationResult expectedRegistrationResult =  RegistrationResult.PasswordDoNotMatch;

            // Act
            RegistrationResult actualRegistrationResult = await _authenticationProvider.Register(It.IsAny<string>(), It.IsAny<string>(), password, confirmPassword, It.IsAny<double>());

            // Assert
            Assert.AreEqual(expectedRegistrationResult, actualRegistrationResult);
        }

        /// <summary>
        /// Register with empty email or username or password should return UsernameOrEmailOrPasswordIsEmpty
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Register_WithEmptyEmailOrUsernameOrPassword_ReturnsUsernameOrEmailOrPasswordIsEmpty()
        {
            // Arrange
            string email = "email", 
                username = "username", 
                password = "password";
            RegistrationResult expectedRegistrationResultEmailOrPasswordOrUsername = RegistrationResult.UsernameOrEmailOrPasswordIsEmpty;

            // Act
            RegistrationResult actualRegistrationResultEmailIsEmpty = await _authenticationProvider.Register("", username, password, password, It.IsAny<double>());
            RegistrationResult actualRegistrationResultUserNameIsEmpty = await _authenticationProvider.Register(email, "", password, password, It.IsAny<double>());
            RegistrationResult actualRegistrationResultPasswordIsEmpty = await _authenticationProvider.Register(email, username, "", "", It.IsAny<double>());

            // Assert
            Assert.AreEqual(expectedRegistrationResultEmailOrPasswordOrUsername, actualRegistrationResultEmailIsEmpty);
            Assert.AreEqual(expectedRegistrationResultEmailOrPasswordOrUsername, actualRegistrationResultUserNameIsEmpty);
            Assert.AreEqual(expectedRegistrationResultEmailOrPasswordOrUsername, actualRegistrationResultPasswordIsEmpty);
        }

        /// <summary>
        /// Register with email that already exists should return EmailAlreadyExists
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Register_WithEmailAlreadyExists_ReturnsEmailAlreadyExists()
        {
            // Arrange
            string email = "testRegister@test.com",
                  username = "username",
               password = "password";
            // Setup the expected result for the test
            RegistrationResult expectedRegistrationResult = RegistrationResult.EmailAlreadyExists;
            // Setup the mock account service to return an account with the expected email
            _mockAccountService.Setup(setup => setup.GetByEmail(email))
                .ReturnsAsync(new Account { AccountHolder = new User { Email = email } });

            // Act
            RegistrationResult actualRegistrationResult = await _authenticationProvider.Register(email, username, password, password, It.IsAny<double>());

            // Assert
            Assert.AreEqual(expectedRegistrationResult, actualRegistrationResult);
        }

        /// <summary>
        /// Register with username that already exists should return UsernameAlreadyExists
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Register_WithUsernameAlreadyExists_ReturnsUsernameAlreadyExists()
        {
            // Arrange
            string email = "test@test123.com",
                   username = "testRegister",
                   password = "password";
            // Setup the expected result for the test
            RegistrationResult expectedRegistrationResult = RegistrationResult.UsernameAlreadyExists;
            // Setup the mock account service to return an account with the expected username
            _mockAccountService.Setup(setup => setup.GetByUserName(username))
                .ReturnsAsync(new Account { AccountHolder = new User { Username = username } });

            // Act
            RegistrationResult actualRegistrationResult = await _authenticationProvider.Register(email, username, password, password, It.IsAny<double>());

            // Assert
            Assert.AreEqual(expectedRegistrationResult, actualRegistrationResult);
        }

        /// <summary>
        /// Register with starting balance less than 0 should return StartingBalanceMustBePositive
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Register_WithStartBalanceLessThan0_ReturnsStartBalanceMustBePositive()
        {
            // Arrange
            double notEnoughStartBalance = -1;
            string email = "email",
               username = "username",
               password = "password";
            RegistrationResult expectedRegistrationResult = RegistrationResult.StartingBalanceMustBePositive;

            // Act
            RegistrationResult actualRegistrationResult = await _authenticationProvider.Register(email, username, password, password, notEnoughStartBalance);

            // Assert
            Assert.AreEqual(expectedRegistrationResult, actualRegistrationResult);
        }

        /// <summary>
        /// Register with new user and correct parameters should return Success
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Register_WithNewUserAndCorrectParameters_ReturnsSuccess()
        {
            // Arrange
            string email = "new@user0.com",
                   username = "newUser0",
                   password = "password",
                   confirmPassword = "password";
            double startBalance = 50;
            // Setup the expected result for the test
            RegistrationResult expectedRegistrationResult = RegistrationResult.Success;

            // Act
            RegistrationResult actualRegistrationResult = await _authenticationProvider.Register(email, username, password, confirmPassword, startBalance);

            // Assert
            Assert.AreEqual(expectedRegistrationResult, actualRegistrationResult);
        }
    }
}
