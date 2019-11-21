using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BeautifulRestApi.Dao;
using BeautifulRestApi.Models;
using Dapper;
using Xunit;

namespace BeautifulRestApi.Test
{
    public class Test
    {
        private const string UserSa = "sa";
        private const string Localhost = "localhost";
        private const string Password = "myPassw0rd";
        private const string Database = "master";

        [Fact]
        private void ExecuteSample()
        {
            UserDao sqlDataAccess = new UserDao(Localhost, Database, UserSa, Password);

            IDbConnection connection = sqlDataAccess.Connection;
            var dbConnection = connection;

            var result = dbConnection.QuerySingle<User>("select [Guid],[Name],[Email] from dbo.[users]");

            Assert.NotNull(result);
        }


        [Fact]
        void FillData()
        {
            DataTable dataTable = new DataTable();

            UserDao sqlDataAccess = new UserDao(Localhost, Database, UserSa, Password);
            using SqlConnection connection = new SqlConnection(sqlDataAccess.ConnectionString);
            connection.Open();

            var da = new SqlDataAdapter("select * from [users]", connection);
            da.Fill(dataTable);


            EnumerableRowCollection<DataRow> enumerableRowCollection = dataTable
                .AsEnumerable()
                .Where(myRow => myRow.Field<int>("RowNo") == 1);
        }

        [Fact]
        void TEst2()
        {
            UserDao sqlDataAccess = new UserDao(Localhost, Database, UserSa, Password);
            using SqlConnection connection = new SqlConnection(sqlDataAccess.ConnectionString);
            connection.Open();


            var query = connection.Query<User>("SELECT * FROM dbo.[Users]");
        }

        [Fact]
        void FirstTest()
        {
            try
            {
                UserDao sqlDataAccess = new UserDao(Localhost, Database, UserSa, Password);

                using SqlConnection connection = new SqlConnection(sqlDataAccess.ConnectionString);
                connection.Open();

                var sqlCommand = new SqlCommand("select guid, name, email from [users]", connection);

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                List<string> users = new List<string>();

                while (sqlDataReader.Read())
                {
                    users.Add(sqlDataReader[0] + ", " + sqlDataReader[1] + ", " + sqlDataReader[2]);
                }

                Assert.True(users.Count > 0);
            }
            catch (SqlException e)
            {
                Assert.True(false, e.Message);
            }
        }

        /*
         * shitty way to do it
         */
        [Fact]
        public void AddUser()
        {
            try
            {
                var sqlDataAccess = new UserDao(Localhost, Database, UserSa, Password);

                var user = new User(Guid.NewGuid(), "test", "test1@gmail.com");

                sqlDataAccess.AddUser(user);

                Assert.True(true);
            }
            catch (SqlException e)
            {
                Assert.True(false, e.Message);
            }
        }

        [Fact]
        public void Print()
        {
            var user = new User(Guid.NewGuid(), "test", "test1@gmail.com");
            var dao = new UserDao(Localhost, Database, UserSa, Password);

            var sqlConnection = dao.Connection;

            const string cmd = "INSERT INTO [users] (guid, name, email) values (@Guid, @Name ,@Email)";

            var execute = sqlConnection.Execute(cmd, user);

            var result =
                sqlConnection.QuerySingle<User>("select [Guid],[Name],[Email] from dbo.[users] where Guid =  @Guid",
                    user);

            Assert.True(user.Equals(result));
        }
    }
}