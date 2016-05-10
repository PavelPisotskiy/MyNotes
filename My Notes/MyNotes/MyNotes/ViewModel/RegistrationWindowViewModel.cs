using MyNotes.Helpers;
using MyNotes.MyNotesServiceReference;
using System.ComponentModel;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace MyNotes.ViewModel
{
    public class RegistrationWindowViewModel : BaseViewModel, IDataErrorInfo
    {
        IMessageService messageService;
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string surname;

        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
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
            set
            {
                password = value;
                OnPropertyChanged("Password");
                OnPropertyChanged("ConfirmedPassword");
            }
        }

        private string confirmedPassword;

        public string ConfirmedPassword
        {
            get { return confirmedPassword; }
            set
            {
                confirmedPassword = value;
                OnPropertyChanged("Password");
                OnPropertyChanged("ConfirmedPassword");
            }
        }

        RelayCommand registerCommand;

        public ICommand RegisterCommand
        {
            get
            {
                if (registerCommand == null)
                    registerCommand = new RelayCommand(RegisterCommandExecute, RegisterCommandCanExecute);
                return registerCommand;
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case "Name":
                        {
                            if (string.IsNullOrEmpty(Name))
                                result = Localization.LocalizeResource.EnterName;
                        }
                        break;
                    case "Surname":
                        {
                            if (string.IsNullOrEmpty(Surname))
                                result = Localization.LocalizeResource.EnterSurname;
                        }
                        break;
                    case "Email":
                        {
                            if (string.IsNullOrEmpty(Email))
                                result = Localization.LocalizeResource.EnterEmail;
                            else if (!Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                                result = Localization.LocalizeResource.InvalidEmail;

                        }
                        break;
                    case "Password":
                        {
                            if (string.IsNullOrEmpty(Password))
                                result = Localization.LocalizeResource.EnterPassword;
                        }
                        break;
                    case "ConfirmedPassword":
                        {
                            if (!string.IsNullOrEmpty(Password) && !Password.Equals(ConfirmedPassword))
                                result = Localization.LocalizeResource.PasswordDoNotMatch;
                        }
                        break;

                    default:
                        return null;
                }

                return result;

            }
        }

        private void RegisterCommandExecute(object obj)
        {
            try
            {
                RegistrationClient c = new RegistrationClient();
                IRegistration registration = c.ChannelFactory.CreateChannel();
                int result = registration.NewUserRegistration(name, surname, email, Encryptor.MD5Hash(password));
                if (result != 0)
                {
                    messageService.ShowMessage("Вы успешно зарегестрированы.");
                }
            }
            catch (CommunicationException ex)
            {
                if (ex.Message.Contains(email))
                {
                    messageService.ShowError("Пользователь с таким Email уже существует.");
                }
                else
                {
                    messageService.ShowError(ex.Message);
                }

            }
        }

        private bool RegisterCommandCanExecute(object obj)
        {
            return !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surname) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confirmedPassword) && confirmedPassword.Equals(password);
        }

        public RegistrationWindowViewModel()
        {
            this.messageService = ServiceLocator.Instance.Resolve<IMessageService>();
        }
    }
}
