using MyNotes.Model;
using MyNotes.View;
using MyNotes.ViewModel;
using System;
using System.Windows;

namespace MyNotes.Helpers
{
    class WindowManager : IWindowManager
    {
        NoteEditorWindow window;
        public void CloseNoteEditor()
        {
            if (window != null)
                window.Close();
        }

        public bool? ShowNoteEditorWindow(Note note)
        {
            window = new NoteEditorWindow();
            window.Owner = Application.Current.MainWindow;
            NoteEditorViewModel vm = (NoteEditorViewModel)window.DataContext;
            if(note == null)
            {
                vm.Note = new Note("", "", "", "White", DateTime.Now, DateTime.Now);
                vm.CurrentMode = NoteEditorViewModel.Mode.Creating;
            }else
            {
                vm.Note = note;
                vm.CurrentMode = NoteEditorViewModel.Mode.Editing;
            }

            return window.ShowDialog();
        }

        public bool? ShowSettingsWindow()
        {
            SettingsWindow window = new SettingsWindow();
            window.Owner = Application.Current.MainWindow;

            return window.ShowDialog();
        }

        public void CloseAuthorizationWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(AuthorizationWindow))
                {
                    window.Close();break;
                }
            }

            
        }

        public void ShowMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }

        public void ShowRegistrationWindow()
        {
            RegistrationWindow window = new RegistrationWindow();
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }
    }
}
