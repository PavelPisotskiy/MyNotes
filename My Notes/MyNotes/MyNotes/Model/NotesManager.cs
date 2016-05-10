using MyNotes.Model.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyNotes.Model
{
    public class NotesManager : INotesManager
    {
        private readonly IDataAccessObject localData;
        protected virtual IDataAccessObject GetLocalData()
        {
            return new XMLDataAccessObject();
        }

        private readonly IDataAccessObject dBData;
        protected virtual IDataAccessObject GetDBData()
        {
            return new DataBaseDataAccessObject();
        }

        private readonly ObservableCollection<Note> notes;
        private readonly ReadOnlyObservableCollection<Note> readonlyNotes;

        private int userId = 0;

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public NotesManager()
        {

            localData = GetLocalData();
            dBData = GetDBData();
            notes = new ObservableCollection<Note>();
            readonlyNotes = new ReadOnlyObservableCollection<Note>(notes);
        }

        public ReadOnlyObservableCollection<Note> Notes
        {
            get
            {
                return readonlyNotes;
            }
        }

        public bool RemoveNote(Note note)
        {
            localData.DeleteNote(note, userId);
            DateTime now = DateTime.UtcNow;

            localData.SetLastModified(userId, now);

            if (userId != 0)
            {
                dBData.DeleteNote(note, userId);
                dBData.SetLastModified(userId, now);
            }
            return notes.Remove(note);
        }

        public bool ChangeNote(string id, string title, string content, string color)
        {
            foreach (Note note in notes)
            {
                if (note.ID.Equals(id))
                {
                    note.Title = title;
                    note.Content = content;
                    note.NoteColor = color;
                    note.LastDateModified = DateTime.Now;

                    DateTime now = DateTime.UtcNow;

                    localData.UpdateNote(note, userId);
                    localData.SetLastModified(userId, now);


                    if (userId != 0)
                    {
                        dBData.UpdateNote(note, userId);
                        dBData.SetLastModified(userId, now);
                    }
                    return true;
                }
            }

            return false;
        }

        public void AddNote(string title, string content, string color)
        {
            Note note = new Note(Guid.NewGuid().ToString(), title, content, color, DateTime.Now, DateTime.Now);
            localData.CreateNote(note, userId);

            DateTime now = DateTime.UtcNow;
            localData.SetLastModified(userId, now);


            if (userId != 0)
            {
                dBData.CreateNote(note, userId);
                dBData.SetLastModified(userId, now);
            }

            notes.Add(note);
        }

        public void UpdateData()
        {
            notes.Clear();

            if (userId != 0)
                Sync();

            foreach (var item in localData.ReadNotes(userId))
            {
                notes.Add(item);
            }
        }

        private void Sync()
        {
            List<Note> notesDB = new List<Note>(dBData.ReadNotes(userId));
            List<Note> notesLocal = new List<Note>(localData.ReadNotes(userId));

            DateTime tempLocalLastModified;
            DateTime tempDbLastModified;

            try
            {
                tempLocalLastModified = localData.GetLastModified(userId);
            }
            catch (Exception)
            {
                tempLocalLastModified = new DateTime();
            }

            try
            {
                tempDbLastModified = dBData.GetLastModified(userId);
            }
            catch (Exception)
            {
                tempDbLastModified = new DateTime();
            }

            DateTime localLastModified = new DateTime(tempLocalLastModified.Year, tempLocalLastModified.Month, tempLocalLastModified.Day, tempLocalLastModified.Hour, tempLocalLastModified.Minute, tempLocalLastModified.Second);
            DateTime dbLastModified = new DateTime(tempDbLastModified.Year, tempDbLastModified.Month, tempDbLastModified.Day, tempDbLastModified.Hour, tempDbLastModified.Minute, tempDbLastModified.Second); ;

            if (localLastModified > dbLastModified)
            {
                foreach (var local in notesLocal)
                {
                    Note dbNote = null;

                    foreach (var db in notesDB)
                    {
                        if (local.ID.Equals(db.ID))
                        {
                            dbNote = local;
                        }
                    }

                    if (dbNote == null)
                    {
                        dBData.CreateNote(local, userId);
                    }
                    else
                    {
                        dBData.UpdateNote(dbNote, userId);
                    }
                }

                foreach (var db in notesDB)
                {
                    Note dbNote = null;
                    foreach (var local in notesLocal)
                    {
                        if (db.ID.Equals(local.ID))
                        {
                            dbNote = db;
                        }
                    }

                    if (dbNote == null)
                        dBData.DeleteNote(db, userId);

                    DateTime now = DateTime.UtcNow;

                    dBData.SetLastModified(userId, now);
                    localData.SetLastModified(userId, now);
                }
            }
            else
            if (localLastModified < dbLastModified)
            {

                foreach (var localNote in notesLocal)
                {
                    localData.DeleteNote(localNote, userId);
                }

                foreach (var item in notesDB)
                {
                    localData.CreateNote(item, userId);
                }

                DateTime now = DateTime.UtcNow;

                dBData.SetLastModified(userId, now);
                localData.SetLastModified(userId, now);
            }
        }


    }
}
