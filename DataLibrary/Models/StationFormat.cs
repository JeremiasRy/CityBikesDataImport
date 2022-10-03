using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary;

public class StationFormat
{
    public int Id { get; set; }
    public string? StationId { get; set; }
    public string? Nimi { get; set; }
    public string? Namn { get; set; }
    public string? Name { get; set; }

    public string? Osoite { get; set; }
    public string? Address { get; set; }

    public string? Kaupunki { get; set; }
    public string? Stad { get; set; }

    public string? Operator { get; set; }
    public int Capacity { get; set; }

    public double X { get; set; }
    public double Y { get; set; }
}
