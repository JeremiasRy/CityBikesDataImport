using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace DataLibrary;
public class DataRead
{
    readonly string path;
    public int InvalidItems { get; set; } = 0;
    public DataTable JourneysDataTable { get; set; } = new();
    public DataTable StationsDataTable { get; set; } = new();
    public void ReadData()
    {
        List<JourneyFormat> _journeys = new();
        List<StationFormat> _stations = new();
        string[] _fileNames = Directory.GetFiles(path);

        Console.WriteLine("Found {0} file{1}:", _fileNames.Length, _fileNames.Length > 1 ? "s" : "");
        foreach (string _file in _fileNames)
            Console.WriteLine(_file);

        Console.WriteLine("Proceed? (Esc to exit)");
        if (Console.ReadKey().Key == ConsoleKey.Escape)
            return;

        foreach (string _file in _fileNames)
        {
            Console.WriteLine("Parsing file {0}", _file);
            using TextFieldParser textFieldParser = new(_file);

            textFieldParser.Delimiters = new string[] { "," };
            textFieldParser.HasFieldsEnclosedInQuotes = true;

            var topOfFile = textFieldParser.ReadFields();

            if (topOfFile == null)
                continue;
            if (topOfFile.Length == 8)
            {
                while (!textFieldParser.EndOfData)
                {
                    var currentRow = textFieldParser.ReadFields();
                    if (currentRow is null)
                        continue;

                    if (DataValidate.ValidateJourney(currentRow, out JourneyFormat journey))
                        _journeys.Add(journey);
                    else
                        InvalidItems++;
                }
            }
            else if (topOfFile.Length == 13)
            {
                while (!textFieldParser.EndOfData)
                {
                    var currentRow = textFieldParser.ReadFields();
                    if (currentRow is null)
                        continue;

                    if (DataValidate.ValidateStation(currentRow, out StationFormat station))
                        _stations.Add(station);
                    else
                        InvalidItems++;
                }
            }
        }
        Console.WriteLine("Creating datatables..");
        JourneysDataTable = ListToDataTable(_journeys.OrderBy(journey => journey.Date).ToList());
        StationsDataTable = ListToDataTable(_stations);
        Console.WriteLine("Journeys: {0}, Stations: {1}, Invalid entries: {2}", JourneysDataTable.Rows.Count, StationsDataTable.Rows.Count, InvalidItems);
    }

    public static DataTable ListToDataTable<T>(List<T> items)
    {
        DataTable dt = new(typeof(T).Name);
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (PropertyInfo property in properties)
            dt.Columns.Add(property.Name);

        foreach (var item in items)
        {
            var values = new object[properties.Length];
            for (int i = 0; i < values.Length; i++)
            {
                var value = properties[i].GetValue(item, null);
                if (value is not null)
                    values[i] = value;
            }
            dt.Rows.Add(values);
        }
        items.Clear();
        if (dt.TableName == "JourneyFormat")
            dt.Columns.Remove("Date");
        return dt;
    }

    public DataRead(string pathToFolder)
    {
           path = pathToFolder;
    }
}
