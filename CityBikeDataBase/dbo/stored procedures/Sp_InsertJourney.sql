CREATE PROCEDURE [dbo].[Sp_InsertJourney]
	@DepartureStationId char(3),
	@ReturnStationId char(3),
	@Duration int,
	@Distance int,
	@Date date = null
AS
	INSERT INTO journeys
	VALUES (@Date, @DepartureStationId, @ReturnStationId, @Distance, @Duration)
RETURN 0
