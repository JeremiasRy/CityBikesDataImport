// See https://aka.ms/new-console-template for more information
using DataLibrary;

DataMove data = new(@"C:\DataForPreAssigment");

data.ReadData();
Console.WriteLine("Move data to database?");
Console.ReadKey();
