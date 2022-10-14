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

    public static readonly string CreateGetJourneysProcedure = @"
    CREATE PROCEDURE [dbo].[GetJourneys]
	    @PageIndex int = 1,
	    @PageSize int = 100,
	    @Date date = NULL,
	    @DepartureStationId char(3) = NULL,
	    @ReturnStationId char(3) = NULL,
	    @Distance int = NULL,
	    @Duration int = NULL,
	    @OrderBy varchar(50) = NULL,
	    @OrderDirection char(1) = 'A',
	    @DistanceOperator char(4) = 'MORE',
	    @DurationOperator char(4) = 'MORE'
    AS
    BEGIN
    SELECT TOP (@PageSize) *
    FROM	( SELECT    ROW_NUMBER() OVER ( ORDER BY 
											CASE WHEN @OrderDirection = 'A' THEN
												CASE WHEN @OrderBy = 'Distance' THEN Distance
													 WHEN @OrderBy = 'Duration' THEN Duration
													 WHEN @OrderBy IS NULL THEN Id
													 END 
												END ASC,
											CASE WHEN @OrderDirection = 'D' THEN
												CASE WHEN @OrderBy = 'Distance' THEN Distance
													 WHEN @OrderBy = 'Duration' THEN Duration
													 WHEN @OrderBy = 'Date' THEN Id
													 END 
												END DESC,
											CASE WHEN @OrderDirection IS NULL THEN Id END) AS RowNum, *
          FROM      journeys
          WHERE     (1=(CASE when @Date is null THEN 1 else 0 end) or DepartureDate = @Date) and
					(1=(CASE when @DepartureStationId is null THEN 1 else 0 end) or @DepartureStationId = DepartureStationId) and
					(1=(CASE when @ReturnStationId is null THEN 1 else 0 end) or @ReturnStationId = ReturnStationId) and
					(1=(CASE when @Distance is null THEN 1 else 0 end) or CASE 
																			WHEN @DistanceOperator = 'MORE' THEN Distance
																			WHEN @DistanceOperator = 'LESS' THEN @Distance
																			END >= 
																		  CASE
																			WHEN @DistanceOperator = 'MORE' THEN @Distance
																			WHEN @DistanceOperator = 'LESS' THEN Distance 
																			END) and 
					(1=(CASE when @Duration is null THEN 1 else 0 end) or CASE 
																			WHEN @DurationOperator = 'MORE' THEN Duration
																			WHEN @DurationOperator = 'LESS' THEN @Duration
																			END >= 
																		  CASE
																			WHEN @DurationOperator = 'MORE' THEN @Duration
																			WHEN @DurationOperator = 'LESS' THEN Duration
																			END)
																			
																			
			
        ) AS result 
    WHERE   RowNum >= @PageSize * @PageIndex - @PageSize 
    ORDER BY RowNum
    OPTION(RECOMPILE)
    END
    ";

    public static readonly string CreateGetStationsProcedure = @"
    CREATE PROCEDURE [dbo].[GetStations] 
    AS
    BEGIN
    	SELECT *
    	FROM [dbo].stations
    END
    ";

    public static readonly string CreateGetStationNameProcedure = @"
    CREATE PROCEDURE [dbo].[GetStationName] 
    @StationId char(3) 
    AS
    BEGIN
    SELECT * 
    FROM [dbo].stations
    WHERE @StationId = StationId 
    END
    ";
}
