using System.Data;
using System.Data.SqlClient;

namespace DataLibrary;

public class DataToDb
{
    readonly SqlConnection _connection;
    readonly DataTable _journeys;
    readonly DataTable _stations;

    public void MoveDataToDb()
    {
        _connection.Open();

        using SqlBulkCopy bulkCopy = new(_connection);

        bulkCopy.DestinationTableName = "[dbo].[journeys]";
        bulkCopy.WriteToServer(_journeys);

        bulkCopy.DestinationTableName = "[dbo].[stations]";
        bulkCopy.WriteToServer(_stations);

        _connection.Close();
    }

    public void CreateIndexes()
    {
        SqlCommand command = _connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "[dbo].[Sp_CreateIndexes]";
        _connection.Open();
        command.ExecuteNonQuery();
        _connection.Close();
    }
    public DataToDb(string connectionString, DataTable journeys, DataTable stations)
    {
        _connection = new SqlConnection(connectionString);
        _journeys = journeys;
        _stations = stations;
    }
}
