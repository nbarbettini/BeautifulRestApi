using System.Data;
using System.Data.SqlClient;

namespace BeautifulRestApi.DAO
{
    public class SqlDataAccess : IDatabaseHandler
    {
        private string server;
        private string database;
        private string uid;
        private string password;


        public string ConnectionString { get; set; }


        private IDbConnection Connection { get; set; }


        public SqlDataAccess(string server, string database, string uid, string password)
        {
            this.server = server;
            this.database = database;
            this.uid = uid;
            this.password = password;
        }

        public IDbConnection CreateConnection()
        {
            ConnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" +
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