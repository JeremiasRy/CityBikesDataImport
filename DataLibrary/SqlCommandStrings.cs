using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary;

public static class SqlCommandStrings
{
    public static readonly string CreateTables = @"
    DROP TABLE IF EXISTS journeys
    DROP TABLE IF EXISTS stations
    CREATE TABLE stations(
    StationId char(3) PRIMARY KEY NOT NULL,
    Name nvarchar(50),
    Address nvarchar(50),
    Kaupunki nvarchar(50),
    Operator nvarchar(50),
    Capacity int,
    Latitude float,
    Altitude float,
    )

    CREATE TABLE journeys(
    DepartureDate date,
    DepartureStationId char(3) FOREIGN KEY references dbo.stations,
    ReturnStationId char(3) FOREIGN KEY references dbo.stations,
    Distance int,
    Duration int,
    Id int PRIMARY KEY IDENTITY(1,1)
    )";

    public readonly static string CreateIndexes = @"
    CREATE NONCLUSTERED INDEX [DistanceIndex] ON [dbo].[journeys]
    (
    	[Distance] ASC
    )
    INCLUDE([DepartureDate],[DepartureStationId],[ReturnStationId],[Duration])

    CREATE NONCLUSTERED INDEX [DurationIndex] ON [dbo].[journeys]
    (
    	[Duration] ASC
    )
    INCLUDE([DepartureDate],[DepartureStationId],[ReturnStationId],[Distance])

    CREATE NONCLUSTERED INDEX [DepartureDateIndex] ON [dbo].[journeys]
    (
    	[DepartureDate] ASC
    )
    INCLUDE([Duration],[DepartureStationId],[ReturnStationId],[Distance])

    CREATE NONCLUSTERED INDEX [DepartureStationIndex] ON [dbo].[journeys]
    (
    	[DepartureStationId] ASC
    )
    INCLUDE([Duration],[DepartureDate],[ReturnStationId],[Distance])

    CREATE NONCLUSTERED INDEX [ReturnStationIndex] ON [dbo].[journeys]
    (
    	[ReturnStationId] ASC
    )
    INCLUDE([Duration],[DepartureStationId],[DepartureDate],[Distance]) ";
}
