using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyNotesService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Authorization" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Authorization.svc or Authorization.svc.cs at the Solution Explorer and start debugging.
    public class Authorization : IAuthorization
    {
        public int SignIn(string email, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyNotesConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = string.Format("SELECT Id FROM Users WHERE Email = @Email AND Password = @Password");
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@Email", SqlDbType.VarChar);
            command.Parameters.Add("@Password", SqlDbType.VarChar);

            command.Parameters["@Email"].Value = email.ToLower();
            command.Parameters["@Password"].Value = password;

            int result = 0;
            try
            {
                result = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception)
            {
                connection.Close();
                return 0; 
            }
            
            connection.Close();
            return result;
        }
    }
}
