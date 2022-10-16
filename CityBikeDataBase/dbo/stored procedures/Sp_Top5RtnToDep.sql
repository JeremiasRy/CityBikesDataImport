-- =============================================
-- Author:		Jeremias Ryttäri
-- Create date: 16.10.2022
-- Description:	Stations top 5 return stations from parameter station
-- =============================================
CREATE PROCEDURE [dbo].[Sp_Top5RtnToDep]
	@StationId char(3)
AS
BEGIN
	SELECT TOP (5) ReturnStationId as StationId, COUNT(*) as Amount
	FROM journeys
	WHERE DepartureStationId = @StationId
	GROUP BY ReturnStationId, DepartureStationId
	ORDER BY Amount DESC
END
GO

