using MyNotes.Model;
using System;
using System.Collections.Generic;

namespace MyNotes.Model.Data
{
    public interface IDataAccessObject
    {
        void CreateNote(Note note, int userId);
        IEnumerable<Note> ReadNotes(int userId);
        void UpdateNote(Note note, int userId);
        void DeleteNote(Note note, int userId);
        DateTime GetLastModified(int userId);
        void SetLastModified(int userId, DateTime now);
    }
}
