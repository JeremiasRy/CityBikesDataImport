CREATE PROCEDURE [dbo].[Sp_DeleteStation]
	@stationId char(3)
AS	
	ALTER TABLE journeys
	NOCHECK CONSTRAINT FK_DepStationId, FK_RetStationId
	DELETE from stations where StationId = @stationId
	ALTER TABLE journeys
	WITH CHECK 
	CHECK CONSTRAINT FK_DepSTationId, FK_RetSTationId
RETURN 0
