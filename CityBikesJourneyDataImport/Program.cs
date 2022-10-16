// See https://aka.ms/new-console-template for more information
using DataLibrary;

string _connectionString = @""; //Connection string comes here
string _folderPath = @""; //Folder path comes here

DataRead dataReader = new(_folderPath);
dataReader.ReadData();
Console.WriteLine("Move data to database?");
Console.ReadKey();

DataToDb dataToDb = new(_connectionString, dataReader.JourneysDataTable, dataReader.StationsDataTable);
Console.WriteLine("Moving data");
dataToDb.MoveDataToDb();
Console.WriteLine("Data moved");
Console.WriteLine("Creating indexes");
dataToDb.CreateIndexes();
Console.WriteLine("Finished!! Enjoy your fresh database! (Any key to exit)");
Console.ReadKey();

