

-- =============================================
-- Author:		Jeremias Ryttäri
-- Create date: 16.10.2022
-- Description:	Get statistics from journeys
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetStationStatistics] 
	@StationId char(3)
AS
BEGIN
		SELECT Count(Id) as Amount, 'Departures' as Type
		FROM journeys
		WHERE @StationId = DepartureStationId 
	UNION
		SELECT Count(Id) as Amount, 'Returns' as Type
		FROM journeys
		WHERE @StationId = ReturnStationId
	UNION
		SELECT AVG(Duration) as Amount, 'Duration average departures (s)' as Type
		FROM journeys
		WHERE @StationId = DepartureStationId
	UNION
		SELECT AVG(Duration) as Amount, 'Duration average returns (s)' as Type
		FROM journeys
		WHERE @StationId = ReturnStationId
	UNION
		SELECT AVG(Distance) as Amount, 'Distance average departures (m)' as Type
		FROM journeys
		WHERE @StationId = DepartureStationId
			UNION
		SELECT AVG(Distance) as Amount, 'Distance average returns (m)' as Type
		FROM journeys
		WHERE @StationId = ReturnStationId
END
GO

