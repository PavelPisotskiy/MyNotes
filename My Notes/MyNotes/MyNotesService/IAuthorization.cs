using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyNotesService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAuthorization" in both code and config file together.
    [ServiceContract]
    public interface IAuthorization
    {
        [OperationContract]
        int SignIn(string email, string password);
    }
}
