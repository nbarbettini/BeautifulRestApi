using System.Data;
using System.Data.SqlClient;


namespace BeautifulRestApi.Dao
{
    public class SqlDataAccess : IDatabaseHandler
    {
        public string Server { get; }
        public string Database { get; }
        public string Uid { get; }
        public string Password { get; }


        public string ConnectionString { get; set; }


        public SqlConnection Connection { get; set; }


        public SqlDataAccess(string server, string database, string uid, string password)
        {
            Server = server;
            Database = database;
            Uid = uid;
            Password = password;
            Connection = CreateConnection(server, database, uid, password);
        }


        public SqlConnection CreateConnection(string dataSource, string database, string uid, string password)
        {
            ConnectionString = "SERVER=" + dataSource + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" +
                               "PASSWORD=" + password;
            Connection = new SqlConnection(ConnectionString);

            return Connection;
        }

        public void CloseConnection()
        {
            Connection.Close();
        }

        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            return new SqlCommand()
            {
                CommandText = commandText,
                Connection = (SqlConnection) connection,
                CommandType = commandType
            };
        }

        public IDataAdapter CreateAdaptor(IDbCommand iDbCommand)
        {
            return new SqlDataAdapter((SqlCommand) iDbCommand);
        }

        public IDbDataParameter CreateParameter(IDbCommand command)
        {
            SqlCommand sqlCommand = (SqlCommand) command;

            return sqlCommand.CreateParameter();
        }
    }
}