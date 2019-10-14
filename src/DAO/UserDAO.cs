using System.Data;
using System.Data.SqlClient;
using BeautifulRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.DAO
{
    public class UserDAO : SqlDataAccess
    {
        public UserDAO(string server, string database, string uid, string password) : base(server, database, uid,
            password)
        {
        }


        public void AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string cmd = "INSERT INTO [user] ('guid', 'name', 'email') values ('@ID', '@NAME' ,'@Email')";

                SqlCommand sqlCommand = new SqlCommand(cmd, connection);
                sqlCommand.Parameters.Add("@ID", SqlDbType.Text);
                sqlCommand.Parameters.Add("@Name", SqlDbType.Text);
                sqlCommand.Parameters.Add("@Email", SqlDbType.Text);

                sqlCommand.Parameters["@ID"].Value = user.Guid;
                sqlCommand.Parameters["@Name"].Value = user.Name;
                sqlCommand.Parameters["@Email"].Value = user.Email;

                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}