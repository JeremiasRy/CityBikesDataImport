using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary;

public class JourneyFormat
{
    public DateTime DepartureDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string? DepartureStationId { get; set; }
    public string?  ReturnStationId { get; set; }

    public int Distance { get; set; }
    public int Duration { get; set; }
}
