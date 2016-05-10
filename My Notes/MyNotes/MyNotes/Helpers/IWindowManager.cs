using MyNotes.Model;
using MyNotes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Helpers
{
    public interface IWindowManager
    {
        bool? ShowNoteEditorWindow(Note note);
        bool? ShowSettingsWindow();
        void ShowMainWindow();
        void ShowRegistrationWindow();

        void CloseNoteEditor();
        void CloseAuthorizationWindow();
    }
}
