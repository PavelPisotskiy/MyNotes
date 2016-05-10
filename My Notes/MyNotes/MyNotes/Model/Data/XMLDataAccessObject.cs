using System;
using System.Collections.Generic;
using MyNotes.Model;
using System.IO;
using System.Xml;
using System.Globalization;

namespace MyNotes.Model.Data
{
    public class XMLDataAccessObject : IDataAccessObject
    {
        FileInfo file;

        XmlDocument xmlDoc = new XmlDocument();

        public XMLDataAccessObject()
        {
            file = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MyNotes\\Notes.xml");

            if (!file.Exists)
            {
                if (!Directory.Exists(file.DirectoryName))
                {
                    Directory.CreateDirectory(file.DirectoryName);
                }
                CreateFile();
            }

            xmlDoc.Load(file.FullName);
        }

        /// <summary>
        /// Добавляет заметку в начало xml файла
        /// </summary>
        /// <param name="note">Заметка которая добавляется в начало xml файла</param>
        public void CreateNote(Note note, int userId)
        {
            XmlNode root = xmlDoc.DocumentElement;

            XmlNode newNote = xmlDoc.CreateElement("Note");

            XmlNode id = xmlDoc.CreateElement("ID");
            id.InnerText = note.ID;
            newNote.AppendChild(id);

            XmlNode createdDate = xmlDoc.CreateElement("Created");
            createdDate.InnerText = note.CreatedDate.ToString("dd.MM.yyyy HH:mm:ss");
            newNote.AppendChild(createdDate);

            XmlNode modifiedDate = xmlDoc.CreateElement("Modified");
            modifiedDate.InnerText = note.LastDateModified.ToString("dd.MM.yyyy HH:mm:ss");
            newNote.AppendChild(modifiedDate);

            XmlNode color = xmlDoc.CreateElement("Color");
            color.InnerText = note.NoteColor;
            newNote.AppendChild(color);

            XmlNode title = xmlDoc.CreateElement("Title");
            title.InnerText = note.Title;
            newNote.AppendChild(title);

            XmlNode content = xmlDoc.CreateElement("Content");
            content.InnerText = note.Content;
            newNote.AppendChild(content);

            root.AppendChild(newNote);

            xmlDoc.Save(file.FullName);
        }

        /// <summary>
        /// Удаляет заметку из xml файла
        /// </summary>
        /// <param name="note">Заметка которая удаляется из xml файла</param>
        public void DeleteNote(Note note, int userId)
        {
            XmlNode root = xmlDoc.DocumentElement;
            foreach (XmlNode item in root.ChildNodes)
            {
                string id = item.ChildNodes[0].InnerText;
                if (id.Equals(note.ID))
                {
                    root.RemoveChild(item);
                    break;
                }

            }
            xmlDoc.Save(file.FullName);
        }

        public DateTime GetLastModified(int userId)
        {
            return Properties.Settings.Default.LastModified;
        }

        /// <summary>
        /// Возвращает список всех заметок находящихся в xml файле
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Note> ReadNotes(int userId)
        {
            xmlDoc.Load(file.FullName);
            List<Note> notes = new List<Note>();
            XmlNode root = xmlDoc.DocumentElement;

            foreach (XmlNode item in root.ChildNodes)
            {
                string id = item.ChildNodes[0].InnerText;
                DateTime created = DateTime.ParseExact(item.ChildNodes[1].InnerText, "dd.MM.yyyy HH:mm:ss", CultureInfo.GetCultureInfoByIetfLanguageTag(Properties.Settings.Default.CultureName));
                DateTime modified = DateTime.ParseExact(item.ChildNodes[2].InnerText, "dd.MM.yyyy HH:mm:ss", CultureInfo.GetCultureInfoByIetfLanguageTag(Properties.Settings.Default.CultureName));
                string color = item.ChildNodes[3].InnerText;
                string title = item.ChildNodes[4].InnerText;
                string content = item.ChildNodes[5].InnerText;

                notes.Add(new Note(id, title, content, color, created, modified));
            }

            return notes;
        }

        public void SetLastModified(int userId, DateTime now)
        {
            Properties.Settings.Default.LastModified = now;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Обновляет данные заметки в xml файле
        /// </summary>
        /// <param name="note">Заметка с обновленными данными(Без изменения ID)</param>
        public void UpdateNote(Note note, int userId)
        {
            XmlNode root = xmlDoc.DocumentElement;
            foreach (XmlNode item in root.ChildNodes)
            {
                string id = item.ChildNodes[0].InnerText;
                if (id.Equals(note.ID))
                {
                    item.ChildNodes[2].InnerText = note.LastDateModified.ToString("dd.MM.yyyy HH:mm:ss");
                    item.ChildNodes[3].InnerText = note.NoteColor;
                    item.ChildNodes[4].InnerText = note.Title;
                    item.ChildNodes[5].InnerText = note.Content;
                    break;
                }

            }
            xmlDoc.Save(file.FullName);
        }

        private void CreateFile()
        {
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            XmlElement root = xmlDoc.CreateElement("Notes");
            xmlDoc.AppendChild(root);
            xmlDoc.Save(file.FullName);
        }
    }
}
