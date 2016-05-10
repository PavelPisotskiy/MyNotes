using System.Collections.ObjectModel;

namespace MyNotes.Model
{
    public interface INotesManager
    {
        ReadOnlyObservableCollection<Note> Notes { get; }
        bool RemoveNote(Note note);
        bool ChangeNote(string id, string title, string content, string color);
        void AddNote(string title, string content, string color);
        void UpdateData();

        int UserId
        {
            get;
            set;
        }
    }
}
