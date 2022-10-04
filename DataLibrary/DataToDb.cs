using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary;

public class DataToDb
{
    readonly string _connectionString;
    readonly DataTable _journeys;
    readonly DataTable _stations;

    public void MoveDataToDb()
    {
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();
            using (SqlBulkCopy bulkCopy = new (connection))
            {
                bulkCopy.DestinationTableName = "dbo.Journeys";
                bulkCopy.WriteToServer(_journeys);
                bulkCopy.DestinationTableName = "dbo.Stations";
                bulkCopy.WriteToServer(_stations);
            }

        }
    }
    public DataToDb(string connectionString, DataTable journeys, DataTable stations)
    {
        _connectionString = connectionString;
        _journeys = journeys;
        _stations = stations;
    }
}
