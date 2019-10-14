using System.Data;

namespace BeautifulRestApi.DAO
{
    public interface IDatabaseHandler
    {
        IDbConnection CreateConnection();

        void CloseConnection();

        IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection);

        IDataAdapter CreateAdaptor(IDbCommand iDbCommand);

        IDbDataParameter CreateParameter(IDbCommand command);
    }
}