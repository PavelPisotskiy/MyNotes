using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Configuration;

namespace MyNotesService
{

    public class Registration : IRegistration
    {
        public int NewUserRegistration(string name, string surname, string email, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyNotesConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = string.Format("INSERT INTO Users (Name, Surname, Email, Password, LastModified) VALUES (@Name, @Surname, @Email, @Password, @LastModified)");
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar);
            command.Parameters.Add("@Surname", SqlDbType.NVarChar);
            command.Parameters.Add("@Email", SqlDbType.VarChar);
            command.Parameters.Add("@Password", SqlDbType.NVarChar);
            command.Parameters.Add("@LastModified", SqlDbType.DateTime);

            command.Parameters["@Name"].Value = name;
            command.Parameters["@Surname"].Value = surname;
            command.Parameters["@Email"].Value = email.ToLower();
            command.Parameters["@Password"].Value = password;
            command.Parameters["@LastModified"].Value = DateTime.Now;

            int rowsAffected = command.ExecuteNonQuery();

            connection.Close();

            return rowsAffected;
        }
    }
}
