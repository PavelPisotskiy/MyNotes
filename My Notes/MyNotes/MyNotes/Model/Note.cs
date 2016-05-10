using System;
using System.ComponentModel;

namespace MyNotes.Model
{
    public class Note : INotifyPropertyChanged, IDataErrorInfo
    {
        private string id;
        private string title;
        private string content;
        private string color;
        private DateTime lastDateModified;
        private DateTime createdDate;

        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
                OnPropertyChanged("Content");
            }
        }

        public string ID
        {
            get
            {
                return id;
            }
        }

        public DateTime LastDateModified
        {
            get
            {
                return lastDateModified;
            }

            set
            {
                lastDateModified = value;
                OnPropertyChanged("LastDateModified");
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }

            set
            {
                createdDate = value;
                OnPropertyChanged("CreatedDate");
            }
        }

        public string NoteColor
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
                OnPropertyChanged("NoteColor");
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName.Equals("Title"))
                {
                    if (string.IsNullOrEmpty(Title))
                        result = Localization.LocalizeResource.EnterTitle;
                }
                if (columnName.Equals("Content"))
                {
                    if (string.IsNullOrEmpty(Content))
                        result = Localization.LocalizeResource.EnterContent;
                }
                return result;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Note(string id, string title, string content, string color,DateTime createdDate, DateTime lastDateModified)
        {
            this.id = id;
            this.lastDateModified = lastDateModified;
            this.createdDate = createdDate;
            this.title = title;
            this.content = content;
            this.color = color;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (object.ReferenceEquals(this, obj))
                return true;

            if (obj is Note)
            {
                return Equals(obj as Note);
            }

            return false;
        }

        public bool Equals(Note note)
        {
            if (note == null)
                return false;

            if (object.ReferenceEquals(this, note))
                return true;

            return string.Compare(this.ID, note.ID) == 0 &&
                string.Compare(this.Title, note.Title) == 0 &&
                string.Compare(this.Content, note.Content) == 0 &&
                string.Compare(this.color, note.color) == 0 &&
                this.CreatedDate.Equals(note.CreatedDate) &&
                this.LastDateModified.Equals(note.LastDateModified);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public Note Clone()
        {
            return new Note(this.id, this.title, this.content, this.color, this.createdDate, this.lastDateModified);
        }
    }
}
