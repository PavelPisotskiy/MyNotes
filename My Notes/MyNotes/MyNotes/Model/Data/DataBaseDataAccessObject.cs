using MyNotes.NotesService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Model.Data
{
    class DataBaseDataAccessObject : IDataAccessObject
    {
        public void CreateNote(Note note, int userId)
        {
            NotesClient client = new NotesClient();
            INotes notes = client.ChannelFactory.CreateChannel();
            try
            {
                notes.AddNote(userId, note.ID, note.Title, note.Content, note.NoteColor, note.CreatedDate, note.LastDateModified);
            }
            catch (Exception)
            {
                
            }
            
        }

        public void DeleteNote(Note note, int userId)
        {
            NotesClient client = new NotesClient();
            INotes notes = client.ChannelFactory.CreateChannel();
            try
            {
                notes.DeleteNote(userId, note.ID);
            }
            catch (Exception)
            {
                
            }
            
        }

        public DateTime GetLastModified(int userId)
        {
            NotesClient client = new NotesClient();
            INotes notes = client.ChannelFactory.CreateChannel();
            try
            {
                return notes.GetLastModified(userId);
            }
            catch (Exception)
            {
                return new DateTime();
            }
            
        }

        public IEnumerable<Note> ReadNotes(int userId)
        {

            try
            {
                NotesClient client = new NotesClient();
                INotes notes = client.ChannelFactory.CreateChannel();
                DataSet dataSet = notes.GetNotes(userId);
                DataTable table = dataSet.Tables["Notes"];
                List<Note> notesList = new List<Note>();
                foreach (DataRow item in table.Rows)
                {
                    notesList.Add(new Note(item["Id"].ToString(), item["Title"].ToString(), item["Content"].ToString(), item["Color"].ToString(), (DateTime)item["Created"], (DateTime)item["Modified"]));
                }
                return notesList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new List<Note>();
            }
            
        }

        public void SetLastModified(int userId, DateTime now)
        {
            NotesClient client = new NotesClient();
            INotes notes = client.ChannelFactory.CreateChannel();
            try
            {
                notes.SetLastModified(userId, now);
            }
            catch (Exception)
            {
                
            }
            
        }

        public void UpdateNote(Note note, int userId)
        {
            NotesClient client = new NotesClient();
            INotes notes = client.ChannelFactory.CreateChannel();
            try
            {
                notes.EditNote(userId, note.ID, note.Title, note.Content, note.NoteColor, note.CreatedDate, note.LastDateModified);
            }
            catch (Exception)
            {
                
            }
            
        }
    }
}
