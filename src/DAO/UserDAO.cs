using System;
using System.Data;
using System.Data.SqlClient;
using BeautifulRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Dao
{
    public class UserDao : SqlDataAccess
    {
        public void AddUser(User user)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();
        }
        public SqlCommand BindUser(User user, SqlConnection connection)
        {
            const string cmd = "INSERT INTO [users] (guid, name, email) values (@Id, @Name ,@Email)";

            SqlCommand sqlCommand = new SqlCommand(cmd, connection);
            sqlCommand.Parameters.Add("@Id", SqlDbType.Text);
            sqlCommand.Parameters.Add("@Name", SqlDbType.Text);
            sqlCommand.Parameters.Add("@Email", SqlDbType.Text);

            sqlCommand.Parameters["@Id"].Value = user.Guid;
            sqlCommand.Parameters["@Name"].Value = user.Name;
            sqlCommand.Parameters["@Email"].Value = user.Email;

            return sqlCommand;
        }


      
        public UserDao(string server, string database, string uid, string password) : base(server, database, uid,
            password)
        {
            Connection = CreateConnection(server, database, uid, password);
        }
    }
}