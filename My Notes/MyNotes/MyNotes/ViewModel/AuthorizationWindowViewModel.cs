using MyNotes.Helpers;
using MyNotes.Model;
using MyNotes.MyNotesAuthorizationService;
using System.ServiceModel;
using System.Windows.Input;

namespace MyNotes.ViewModel
{
    public class AuthorizationWindowViewModel : BaseViewModel
    {
        IMessageService messageService;

        IWindowManager windowManager;

        public AuthorizationWindowViewModel()
        {
            this.messageService = ServiceLocator.Instance.Resolve<IMessageService>();
            this.windowManager = ServiceLocator.Instance.Resolve<IWindowManager>();
        }

        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        RelayCommand singInCommand;

        public ICommand SignInCommand
        {
            get
            {
                if (singInCommand == null)
                    singInCommand = new RelayCommand(SignInCommandExecute, SignInCommandCanExecute);
                return singInCommand;
            }
        }

        private void SignInCommandExecute(object obj)
        {
            try
            {
                AuthorizationClient client = new AuthorizationClient();
                IAuthorization authorization = client.ChannelFactory.CreateChannel();
                int id = authorization.SignIn(Email, Encryptor.MD5Hash(password));
                if (id == 0)
                {
                    messageService.ShowExclamation("Не верный пароль или электронная почта.");
                }
                else
                {
                    ServiceLocator.Instance.Resolve<INotesManager>().UserId = id;
                    windowManager.ShowMainWindow();
                    windowManager.CloseAuthorizationWindow();
                    Properties.Settings.Default.Email = email;
                    Properties.Settings.Default.Password = Encryptor.MD5Hash(password);
                    Properties.Settings.Default.Save();
                }

            }
            catch (CommunicationException ex)
            {
                messageService.ShowError(ex.Message);
            }

        }

        private bool SignInCommandCanExecute(object obj)
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(password);
        }

        RelayCommand createAccountCommand;

        public ICommand CreateAccountCommand
        {
            get
            {
                if (createAccountCommand == null)
                    createAccountCommand = new RelayCommand(CreateAccountCommandExecute);
                return createAccountCommand;
            }
        }

        private void CreateAccountCommandExecute(object obj)
        {
            windowManager.ShowRegistrationWindow();
        }
    }
}
