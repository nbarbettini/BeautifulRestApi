using System.Data;
using System.Data.SqlClient;

namespace BeautifulRestApi.Dao
{
    public interface IDatabaseHandler
    {
        SqlConnection CreateConnection(string dataSource, string initialCatalog, string userId, string userpassword);

        void CloseConnection();

        IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection);

        IDataAdapter CreateAdaptor(IDbCommand iDbCommand);

        IDbDataParameter CreateParameter(IDbCommand command);
    }
}