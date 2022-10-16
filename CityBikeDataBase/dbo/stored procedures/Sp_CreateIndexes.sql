
CREATE PROCEDURE [dbo].[Sp_CreateIndexes]
AS
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
    INCLUDE([Duration],[DepartureStationId],[DepartureDate],[Distance])
RETURN 0
