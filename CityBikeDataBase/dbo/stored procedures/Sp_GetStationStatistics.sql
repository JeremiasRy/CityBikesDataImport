

-- =============================================
-- Author:		Jeremias Ryttäri
-- Create date: 16.10.2022
-- Description:	Get statistics from journeys
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetStationStatistics] 
	@StationId char(3),
	@Month int = null
AS
BEGIN
		SELECT Count(Id) as Amount, 'Departures' as Type
		FROM journeys
		WHERE @StationId = DepartureStationId and (1=(CASE when @Month is null then 1 else 0 end) or MONTH(DepartureDate) = @Month)
	UNION
		SELECT Count(Id) as Amount, 'Returns' as Type
		FROM journeys
		WHERE @StationId = ReturnStationId and (1=(CASE when @Month is null then 1 else 0 end) or MONTH(DepartureDate) = @Month)
	UNION
		SELECT AVG(Duration) as Amount, 'Duration average departures (s)' as Type
		FROM journeys
		WHERE @StationId = DepartureStationId and (1=(CASE when @Month is null then 1 else 0 end) or MONTH(DepartureDate) = @Month)
	UNION
		SELECT AVG(Duration) as Amount, 'Duration average returns (s)' as Type
		FROM journeys
		WHERE @StationId = ReturnStationId and (1=(CASE when @Month is null then 1 else 0 end) or MONTH(DepartureDate) = @Month)
	UNION
		SELECT AVG(Distance) as Amount, 'Distance average departures (m)' as Type
		FROM journeys
		WHERE @StationId = DepartureStationId and (1=(CASE when @Month is null then 1 else 0 end) or MONTH(DepartureDate) = @Month)
			UNION
		SELECT AVG(Distance) as Amount, 'Distance average returns (m)' as Type
		FROM journeys
		WHERE @StationId = ReturnStationId and (1=(CASE when @Month is null then 1 else 0 end) or MONTH(DepartureDate) = @Month)
END
GO

