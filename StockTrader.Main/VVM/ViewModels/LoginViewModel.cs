using StockTrader.Main.Commands;
using StockTrader.Main.State.Authentificators;
using StockTrader.Main.State.Navigators;
using System.Windows.Input;

namespace StockTrader.Main.VVM.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IRenavigator _renavigator;

        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; }
        public string SetErrorMessageViewModel
        {
            set => ErrorMessageViewModel.Message = value;
        }
        public bool HasErrorMessage 
            => ErrorMessageViewModel.HasMessage;

        public bool CanLogin => !string.IsNullOrEmpty(Username);
        public ICommand LoginCommand { get; }
        public ICommand ViewRegisterCommand { get; }

        public LoginViewModel(IAuthenticator authenticator, 
            IRenavigator homeViewRenavigator, 
            IRenavigator registerRenavigator)
        {

            ErrorMessageViewModel = new MessageViewModel();

            _renavigator = homeViewRenavigator;
            LoginCommand = new LoginCommand(this, authenticator, _renavigator);
            ViewRegisterCommand = new NavigateCommand(registerRenavigator);
        }
    }
}
