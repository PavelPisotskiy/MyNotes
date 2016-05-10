using MyNotes.Helpers;
using MyNotes.Localization;
using MyNotes.Model;
using MyNotes.Model.Data;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MyNotes.ViewModel
{
    public class SettingsWindowViewModel : BaseViewModel
    {
        ObservableCollection<Language> appLanguages = new ObservableCollection<Language>();
        Language selectedLanguage;

        IMessageService messageService;

        RelayCommand signOutCommand;

        public SettingsWindowViewModel()
        {
            this.messageService = ServiceLocator.Instance.Resolve<IMessageService>();
            
            appLanguages.Add(new Language("Русский", "ru", new BitmapImage(new Uri("pack://application:,,,/Localization/ImageFlags/Russia.png", UriKind.Absolute))));
            appLanguages.Add(new Language("Українська", "uk", new BitmapImage(new Uri("pack://application:,,,/Localization/ImageFlags/Ukraine.png", UriKind.Absolute))));
            appLanguages.Add(new Language("English", "en", new BitmapImage(new Uri("pack://application:,,,/Localization/ImageFlags/UnitedStates.png", UriKind.Absolute))));

            foreach (Language lang in appLanguages)
            {
                if (Properties.Settings.Default.CultureName == lang.LetfLanguageTag)
                {
                    selectedLanguage = lang;
                }
            }

        }

        public ObservableCollection<Language> ApplicationLanguages
        {
            get
            {
                return appLanguages;
            }
        }

        public Language SelectedLanguage
        {
            get
            {
                return selectedLanguage;
            }
            set
            {
                selectedLanguage = value;
                OnPropertyChanged("SelectedLanguage");
                SaveLanguage();
            }
        }

        private void SaveLanguage()
        {
            Properties.Settings.Default.CultureName = selectedLanguage.LetfLanguageTag;
            Properties.Settings.Default.Save();

            messageService.ShowMessage(Localization.LocalizeResource.MessageSaveLanguage);
        }

        public ICommand SignOutCommand
        {
            get
            {
                if (signOutCommand == null)
                    signOutCommand = new RelayCommand(SignOutCommandExecute);
                return signOutCommand;
            }
        }

        private void SignOutCommandExecute(object obj)
        {
            XMLDataAccessObject ob = new XMLDataAccessObject();
            foreach (var item in ob.ReadNotes(0))
            {
                ob.DeleteNote(item, 0);
            }

            Properties.Settings.Default.Email = string.Empty;
            Properties.Settings.Default.Password = string.Empty;
            Properties.Settings.Default.LastModified = new DateTime();
            Properties.Settings.Default.Save();

            Application.Current.Shutdown();
        }
    }
}
