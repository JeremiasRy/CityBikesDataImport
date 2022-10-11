using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary;

public class StationFormat
{
    public string? StationId { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Operator { get; set; }
    public int Capacity { get; set; }
    public double Latitude { get; set; }
    public double Altitude { get; set; }
}
