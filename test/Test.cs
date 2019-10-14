using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BeautifulRestApi.DAO;
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
                    new SqlDataAccess("localhost", "master", "sa", "reallyStrongPwd123");
                var dbConnection = sqlDataAccess.CreateConnection();

                using (SqlConnection connection = new SqlConnection(sqlDataAccess.ConnectionString))
                {
                    connection.Open();

                    var sqlCommand = new SqlCommand("select uuid, name, email from [user]", connection);

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    List<string> users = new List<string>();

                    while (sqlDataReader.Read())
                    {
                        users.Add(sqlDataReader[0].ToString() + ", " + sqlDataReader[1].ToString() + ", " +
                                  sqlDataReader[2].ToString());
                    }
                }
            }
            catch (SqlException e)
            {
                Assert.True(false, "Unable to connect to database");
            }
        }
    }
}