using MyNotes.Helpers;
using MyNotes.Localization;
using MyNotes.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System;
using MyNotes.MyNotesAuthorizationService;
using System.ServiceModel;

namespace MyNotes.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ReadOnlyObservableCollection<Note> notes;

        private readonly INotesManager notesManager;
        private readonly IMessageService messageService;
        private readonly IWindowManager windowManager;
        private ICollectionView notesView;

        private RelayCommand openAddNoteWindowCommand;
        private RelayCommand openEditNoteWindowCommand;
        private RelayCommand deleteNoteCommand;
        private RelayCommand openSettingsCommand;
        private RelayCommand loadedCommand;
        private RelayCommand synchronizationCommand;

        private Note selectedNote;
        private string searchText = "";
        
        public MainWindowViewModel()
        {
            this.messageService = ServiceLocator.Instance.Resolve<IMessageService>();
            this.notesManager = ServiceLocator.Instance.Resolve<INotesManager>();
            this.windowManager = ServiceLocator.Instance.Resolve<IWindowManager>();

            
            notes = notesManager.Notes;

            notesView = CollectionViewSource.GetDefaultView(notes);
            (notesView as ListCollectionView).CustomSort = new SortedByCreatedDate();
            notesView.Filter = CustomerFilter;
        }

        public ReadOnlyObservableCollection<Note> Notes
        {
            get
            {
                return notes;
            }
        }



        public ICommand OpenAddNoteWindowCommand
        {
            get
            {
                if (openAddNoteWindowCommand == null)
                    openAddNoteWindowCommand = new RelayCommand(OpenAddNoteWindowCommandExecute);
                return openAddNoteWindowCommand;
            }
        }

        private void OpenAddNoteWindowCommandExecute(object obj)
        {
            windowManager.ShowNoteEditorWindow(null);
        }

        public ICommand DeleteNoteCommand
        {
            get
            {
                if (deleteNoteCommand == null)
                    deleteNoteCommand = new RelayCommand(DeleteNoteCommandExecute, DeleteNoteCommandCanExecute);

                return deleteNoteCommand;
            }
        }

        private void DeleteNoteCommandExecute(object obj)
        {
            MessageBoxResult result = messageService.ShowQuestion(LocalizeResource.MessageAboutDeletingNotes, MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                notesManager.RemoveNote(selectedNote);
            }
        }

        private bool DeleteNoteCommandCanExecute(object obj)
        {
            return SelectedNote != null;
        }

        public ICommand OpenEditNoteWindowCommand
        {
            get
            {
                if (openEditNoteWindowCommand == null)
                    openEditNoteWindowCommand = new RelayCommand(OpenEditNoteWindowCommandExecute, OpenEditNoteWindowCommandCanExecute);

                return openEditNoteWindowCommand;
            }
        }

        private void OpenEditNoteWindowCommandExecute(object obj)
        {
            windowManager.ShowNoteEditorWindow(SelectedNote.Clone());
        }

        private bool OpenEditNoteWindowCommandCanExecute(object obj)
        {
            return SelectedNote != null;
        }

        public ICommand OpenSettingsCommand
        {
            get
            {
                if (openSettingsCommand == null)
                    openSettingsCommand = new RelayCommand(OpenSettingsCommandExecute);

                return openSettingsCommand;
            }
        }

        private void OpenSettingsCommandExecute(object obj)
        {
            windowManager.ShowSettingsWindow();
        }

        public Note SelectedNote
        {
            get
            {
                return selectedNote;
            }
            set
            {
                selectedNote = value;
                OnPropertyChanged("SelectedNote");
            }
        }

        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
                notesView.Refresh();
            }
        }

        private bool CustomerFilter(object item)
        {
            Note note = item as Note;
            return note.Content.ToLower().Contains(searchText.ToLower()) || note.Title.ToLower().Contains(searchText.ToLower());
        }

        public ICommand LoadedCommand
        {
            get
            {
                if (loadedCommand == null)
                    loadedCommand = new RelayCommand(WindowLoaded);
                return loadedCommand;
            }
        }

        private void WindowLoaded(object obj)
        {
            if (!IsInDesignMode)
                notesManager.UpdateData();
        }

        public ICommand SynchronizationCommand
        {
            get
            {
                if (synchronizationCommand == null)
                    synchronizationCommand = new RelayCommand(SynchronizationCommandExecute);
                return synchronizationCommand;
            }
        }

        private void SynchronizationCommandExecute(object obj)
        {
            string email = MyNotes.Properties.Settings.Default.Email;
            string password = MyNotes.Properties.Settings.Default.Password;

            if (notesManager.UserId != 0)
            {
                notesManager.UpdateData();
            }else if(!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    AuthorizationClient client = new AuthorizationClient();
                    IAuthorization authorization = client.ChannelFactory.CreateChannel();
                    int id = authorization.SignIn(email, password);
                    notesManager.UserId = id;
                    notesManager.UpdateData();
                }
                catch (CommunicationException ex)
                {
                    messageService.ShowError(ex.Message);
                }
                
            }
        }
    }
}
