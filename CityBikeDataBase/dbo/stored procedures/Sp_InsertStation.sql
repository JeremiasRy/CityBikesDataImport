CREATE PROCEDURE [dbo].[Sp_InsertStation]
	@StationId char(3),
	@Name nvarchar(50),
	@Address nvarchar(50) = null,
	@City nvarchar(50) = null,
	@Operator nvarchar(50) = null,
	@Capacity int = null,
	@Latitude float = null,
	@Altitude float = null
AS
	INSERT INTO stations
	VALUES (@StationId, @Name, @Address, @City, @Operator, @Capacity, @Latitude, @Altitude)
RETURN 0
