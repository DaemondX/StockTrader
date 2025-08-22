using StockTrader.Domain.Models;
using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Services.AuthentificationServices.Interfaces;

namespace StockTrader.Main.State.Authentificators
{
    public interface IAuthenticator
    {
        Account? CurrentAccount { get; }
        bool IsLoggedIn { get; }

        event Action StateChanged;

        Task<RegistrationResult> RegisterAsync(string email, string username, string password, string confirmPassword, double startingBalance);

        /// <summary>
        /// Login to the application
        /// </summary>
        /// <param name="username"> The user name </param>
        /// <param name="password"> The user password </param>
        /// <exception cref="UserNotFoundException"> Throw if the user does not exist </exception>
        /// <exception cref="InvalidPasswordException"> Throw if the password is invalid </exception>
        /// <exception cref="Exception"> Throw if the login is failed </exception>
        Task LoginAsync(string username, string password);
        void Logout();
    }
}
