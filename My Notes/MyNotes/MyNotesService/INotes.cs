using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyNotesService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INotes" in both code and config file together.
    [ServiceContract]
    public interface INotes
    {
        [OperationContract]
        DataSet GetNotes(int userId);

        [OperationContract]
        void DeleteNote(int userId, string id);

        [OperationContract]
        void AddNote(int userId, string id, string title, string content, string color, DateTime created, DateTime modified);

        [OperationContract]
        void EditNote(int userId, string id, string title, string content, string color, DateTime created, DateTime modified);

        [OperationContract]
        DateTime GetLastModified(int userId);

        [OperationContract]
        void SetLastModified(int userId, DateTime now);
    }
}
