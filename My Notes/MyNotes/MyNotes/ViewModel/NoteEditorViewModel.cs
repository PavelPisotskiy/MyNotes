using MyNotes.Helpers;
using MyNotes.Model;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace MyNotes.ViewModel
{
    public class NoteEditorViewModel : BaseViewModel
    {
        public enum Mode
        {
            Creating,
            Editing
        }

        private readonly List<string> colors = new List<string>()
        {
            "White",
            "#FFFFD668",
            "#FF92F4AD",
            "#FF92D0F4",
            "#FFF391C8",
            "#FFC37EF3"
        };

        public List<string> Colors
        {
            get
            {
                return colors;
            }
        }
        string title = string.Empty;
        public string Title
        {
            get
            {
                return title;
            }
            private set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private readonly INotesManager notesManager;
        private Mode mode;
        private Note note;
        private ICommand saveNoteCommand;

        private IWindowManager windowManager;

        private bool isChanged;

        public Mode CurrentMode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
            }
        }

        public NoteEditorViewModel()
        {
            this.notesManager = ServiceLocator.Instance.Resolve<INotesManager>();
            this.windowManager = ServiceLocator.Instance.Resolve<IWindowManager>();
            note = new Note("", "", "", "White", DateTime.Now, DateTime.Now);
        }

        private void Note_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            isChanged = true;
            note.PropertyChanged -= Note_PropertyChanged;
        }

        public Note Note
        {
            get
            {
                return note;
            }
            set
            {
                note = value;
                OnPropertyChanged("Note");
            }
        }

        public ICommand SaveNoteCommand
        {
            get
            {
                if (saveNoteCommand == null)
                    saveNoteCommand = new RelayCommand(SaveNoteCommandExecute, SaveNoteCommandCanExecute);

                return saveNoteCommand;
            }
        }

        private void SaveNoteCommandExecute(object obj)
        {
            if (mode == Mode.Creating)
            {
                notesManager.AddNote(note.Title, note.Content, note.NoteColor);
            }
            else
            {
                notesManager.ChangeNote(note.ID, note.Title, note.Content, note.NoteColor);
            }
            windowManager.CloseNoteEditor();
        }

        private bool SaveNoteCommandCanExecute(object obj)
        {
            return !string.IsNullOrEmpty(note.Title) && !string.IsNullOrEmpty(note.Content) && isChanged;
        }

        RelayCommand loadedCommand;

        public ICommand LoadedCommand
        {
            get
            {
                if (loadedCommand == null)
                    loadedCommand = new RelayCommand(LoadedCommandExecute);
                return loadedCommand;
            }
        }

        private void LoadedCommandExecute(object obj)
        {
            note.PropertyChanged += Note_PropertyChanged;

            if (mode == Mode.Creating)
                Title = Localization.LocalizeResource.TitleNoteEditorWindow_Adding;
            else
                Title = Localization.LocalizeResource.TitleNoteEditorWindow_Editing;

            isChanged = false;
        }
    }


}
