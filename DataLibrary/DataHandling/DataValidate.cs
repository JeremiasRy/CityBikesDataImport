using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace DataLibrary;

public static class DataValidate
{
    static readonly CultureInfo provider = CultureInfo.InvariantCulture;
    public static bool ValidateJourney(string[] dataField, out JourneyFormat journey)
    {
        journey = new JourneyFormat();
        bool validData = true;
        if (DateTime.TryParseExact(dataField[0], "s", provider, DateTimeStyles.None, out DateTime departureDate))
        {
            journey.Date = departureDate;
        } else 
            validData = false;
        if (double.TryParse(dataField[6], NumberStyles.Any, provider, out double distance) && double.TryParse(dataField[7], NumberStyles.Any, provider, out double duration))
        {
            if (distance >= 10 && duration >= 10)
            {
                journey.Duration = (int)duration;
                journey.Distance = (int)distance;
            }
            else
                validData = false;
        }
        else
            validData = false;
        if (dataField[2].Length == 3 && dataField[4].Length == 3)
        {
            journey.DepartureStationId = dataField[2];
            journey.ReturnStationId = dataField[4];
        }
        else
            validData = false;

        return validData;
    }

    public static bool ValidateStation(string[] dataField, out StationFormat station)
    {
        station = new();
        bool validData = true;

        if (dataField[1] != "" && dataField[1].Length == 3)
            station.StationId = dataField[1];
        else
            validData = false;

        if (int.TryParse(dataField[10], out int validCapacity))
            station.Capacity = validCapacity;
        else
            validData = false;

        if (double.TryParse(dataField[11], NumberStyles.Any, provider, out double validY) && double.TryParse(dataField[12], NumberStyles.Any, provider, out double validX))
        {
            station.Altitude = validY;
            station.Latitude = validX;
        } else 
            validData = false;

        if (validData)
        {
            station.Name = dataField[2] == "" ? null : dataField[2];
            station.Address = dataField[5] == "" ? null : dataField[5];
            station.City = dataField[7] == "" ? null : dataField[7];
            station.Operator = dataField[9] == "" ? null : dataField[9];
        }
        return validData;
    }
}
