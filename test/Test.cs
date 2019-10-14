using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BeautifulRestApi.DAO;
using BeautifulRestApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;

namespace BeautifulRestApi.Tests
{
    public class Test
    {
        [Fact]
        void FirstTest()
        {
            try
            {
                SqlDataAccess sqlDataAccess =
                    new SqlDataAccess("localhost", "main", "sa", "reallyStrongPwd123");
                var dbConnection = sqlDataAccess.CreateConnection();

                using (SqlConnection connection = new SqlConnection(sqlDataAccess.ConnectionString))
                {
                    connection.Open();

                    var sqlCommand = new SqlCommand("select guid, name, email from [user]", connection);

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    List<string> users = new List<string>();

                    while (sqlDataReader.Read())
                    {
                        users.Add(sqlDataReader[0].ToString() + ", " + sqlDataReader[1].ToString() + ", " +
                                  sqlDataReader[2].ToString());
                    }

                    Assert.True(users.Count > 0);
                }

                dbConnection.Close();
            }
            catch (SqlException e)
            {
                Assert.True(false, "Unable to connect to database");
            }
        }

        [Fact]
        public void AddUser()

        {
            try
            {
                UserDAO sqlDataAccess =
                    new UserDAO("localhost", "main", "sa", "reallyStrongPwd123");
                var dbConnection = sqlDataAccess.CreateConnection();

                using (SqlConnection connection = new SqlConnection(dbConnection.ConnectionString))
                {
                    var user = new User(Guid.NewGuid().ToString(), "Sam I am", "test1@gmail.com");

                    sqlDataAccess.AddUser(user);

                    Assert.True(true);
                }
            }
            catch (SqlException e)
            {
                Assert.True(false, "Unable to connect to database");

            }
        }
    }
}