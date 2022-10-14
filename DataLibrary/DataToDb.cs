using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataLibrary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

        bulkCopy.DestinationTableName = "dbo.journeys";
        bulkCopy.WriteToServer(_journeys);

        bulkCopy.DestinationTableName = "dbo.stations";
        bulkCopy.WriteToServer(_stations);

        _connection.Close();
    }

    public void CreateTables()
    {
        SqlCommand command = _connection.CreateCommand();
        command.CommandText = SqlCommandStrings.CreateTables;
        command.CommandType = CommandType.Text;
        _connection.Open();
        command.ExecuteNonQuery();
        _connection.Close();
    }

    public void CreateIndexes()
    {
        SqlCommand command = _connection.CreateCommand();
        command.CommandText = SqlCommandStrings.CreateIndexes;
        command.CommandType = CommandType.Text;
        _connection.Open();
        command.ExecuteNonQuery();
        _connection.Close();
    }
    public void CreateProcedures()
    {
        _connection.Open();
        SqlCommand command = _connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = SqlCommandStrings.CreateGetStationsProcedure;
        command.ExecuteNonQuery();
        command.CommandText = SqlCommandStrings.CreateGetJourneysProcedure;
        command.ExecuteNonQuery();
        command.CommandText = SqlCommandStrings.CreateGetStationNameProcedure;
    }
    public DataToDb(string connectionString, DataTable journeys, DataTable stations)
    {
        _connection = new SqlConnection(connectionString);
        _journeys = journeys;
        _stations = stations;
    }
}
