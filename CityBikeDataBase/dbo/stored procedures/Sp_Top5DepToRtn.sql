-- =============================================
-- Author:		Jeremias Ryttäri
-- Create date: 16.10.2022
-- Description:	Stations top 5 departure stations that returned to parameter station.
-- =============================================
CREATE PROCEDURE [dbo].[Sp_Top5DepToRtn]
	@StationId char(3)
AS
BEGIN
	SELECT TOP (5) DepartureStationId as StationId, COUNT(*) as Amount
	FROM journeys
	WHERE ReturnStationId = @StationId
	GROUP BY ReturnStationId, DepartureStationId
	ORDER BY Amount DESC
END
GO


