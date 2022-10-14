// See https://aka.ms/new-console-template for more information
using DataLibrary;

DataRead dataReader = new(Connections.FolderPath);
dataReader.ReadData();
Console.WriteLine("Move data to database?");
Console.ReadKey();

DataToDb dataToDb = new(Connections.ConnectionString, dataReader.JourneysDataTable, dataReader.StationsDataTable);
Console.WriteLine("Creating tables");
dataToDb.CreateTables();
Console.WriteLine("Created");
Console.WriteLine("Moving data");
dataToDb.MoveDataToDb();
Console.WriteLine("Data moved");
Console.WriteLine("Creating indexes");
dataToDb.CreateIndexes();
Console.WriteLine("Done");
Console.WriteLine("Creating stored procedures");
dataToDb.CreateProcedures();
Console.WriteLine("Finished!! Enjoy your fresh database! (Any key to exit)");
Console.ReadKey();

