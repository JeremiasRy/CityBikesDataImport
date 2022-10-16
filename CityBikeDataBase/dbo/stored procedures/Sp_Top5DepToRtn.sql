-- =============================================
-- Author:		Jeremias Ryttäri
-- Create date: 16.10.2022
-- Description:	Stations top 5 departures that returned.
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetStationTop5DeparturesThatReturned]
	@StationId char(3)
AS
BEGIN
	SELECT TOP (5) DepartureStationId as StationId, COUNT(*) as ReturnsCount
	FROM journeys
	WHERE ReturnStationId = @StationId
	GROUP BY ReturnStationId, DepartureStationId
	ORDER BY ReturnsCount DESC
END
GO


