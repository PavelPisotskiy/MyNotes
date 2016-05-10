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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Notes" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Notes.svc or Notes.svc.cs at the Solution Explorer and start debugging.
    public class Notes : INotes
    {
        public void AddNote(int userId, string id, string title, string content, string color, DateTime created, DateTime modified)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyNotesConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = string.Format("INSERT INTO Notes(UserId, Id, Title, Content, Color, Created, Modified) VALUES (@UserId, @Id, @Title, @Content, @Color, @Created, @Modified)");
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("@UserId", SqlDbType.Int);
            command.Parameters.Add("@Id", SqlDbType.VarChar);
            command.Parameters.Add("@Title", SqlDbType.NVarChar);
            command.Parameters.Add("@Content", SqlDbType.NVarChar);
            command.Parameters.Add("@Color", SqlDbType.VarChar);
            command.Parameters.Add("@Created", SqlDbType.DateTime);
            command.Parameters.Add("@Modified", SqlDbType.DateTime);

            command.Parameters["@UserId"].Value = userId;
            command.Parameters["@Id"].Value = id;
            command.Parameters["@Title"].Value = title;
            command.Parameters["@Content"].Value = content;
            command.Parameters["@Color"].Value = color;
            command.Parameters["@Created"].Value = created;
            command.Parameters["@Modified"].Value = modified;

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteNote(int userId, string id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyNotesConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = string.Format("DELETE FROM Notes WHERE UserId = @UserId AND Id=@Id");
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("@UserId", SqlDbType.Int);
            command.Parameters.Add("@Id", SqlDbType.VarChar);

            command.Parameters["@UserId"].Value = userId;
            command.Parameters["@Id"].Value = id;

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void EditNote(int userId, string id, string title, string content, string color, DateTime created, DateTime modified)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyNotesConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = string.Format("UPDATE Notes SET Title=@Title, Content=@Content, Color=@Color, Modified=@Modified WHERE UserId=@UserId AND Id=@Id");
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("@UserId", SqlDbType.Int);
            command.Parameters.Add("@Id", SqlDbType.VarChar);
            command.Parameters.Add("@Title", SqlDbType.NVarChar);
            command.Parameters.Add("@Content", SqlDbType.NVarChar);
            command.Parameters.Add("@Color", SqlDbType.VarChar);
            command.Parameters.Add("@Modified", SqlDbType.DateTime);

            command.Parameters["@UserId"].Value = userId;
            command.Parameters["@Id"].Value = id;
            command.Parameters["@Title"].Value = title;
            command.Parameters["@Content"].Value = content;
            command.Parameters["@Color"].Value = color;
            command.Parameters["@Modified"].Value = modified;

            command.ExecuteNonQuery();
            connection.Close();
        }

        public DateTime GetLastModified(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyNotesConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = string.Format("SELECT LastModified FROM Users WHERE Id = @Id");
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("@Id", SqlDbType.Int);
            command.Parameters["@Id"].Value = userId;

            DateTime date = (DateTime)command.ExecuteScalar();
            connection.Close();
            return date;
        }

        public DataSet GetNotes(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyNotesConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = string.Format("SELECT * FROM Notes WHERE UserId = @UserId");
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@UserId", SqlDbType.Int);
            command.Parameters["@UserId"].Value = userId;

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataSet dataSet = new DataSet("Notes");
            adapter.Fill(dataSet, "Notes");

            connection.Close();

            return dataSet;
        }

        public void SetLastModified(int userId, DateTime now)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyNotesConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = string.Format("UPDATE Users SET LastModified=@LastModified WHERE Id=@Id");
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("@Id", SqlDbType.Int);
            command.Parameters.Add("@LastModified", SqlDbType.DateTime);

            command.Parameters["@Id"].Value = userId;
            command.Parameters["@LastModified"].Value = now;

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
