CREATE PROCEDURE [dbo].[Sp_DeleteJourney]
	@Id int
AS
 	DELETE from journeys Where Id = @Id
RETURN 0
