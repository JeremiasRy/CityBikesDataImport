// See https://aka.ms/new-console-template for more information
using DataLibrary;
using System.Data.SqlClient;

DataRead dataReader = new(Connections.FolderPath);
dataReader.ReadData();
Console.WriteLine("Move data to database?");
Console.ReadKey();

DataToDb dataToDb = new(Connections.ConnectionString, dataReader.JourneysDataTable, dataReader.StationsDataTable);
dataToDb.MoveDataToDb();

Console.WriteLine("Hello");
Console.ReadKey();

