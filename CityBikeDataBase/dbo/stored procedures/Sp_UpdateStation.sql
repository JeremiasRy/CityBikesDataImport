CREATE PROCEDURE [dbo].[Sp_UpdateStation]
	@StationId char(3),
	@Name nvarchar(50) = null,
	@Address nvarchar(50) = null,
	@City nvarchar(50) = null,
	@Operator nvarchar(50) = null,
	@Capacity int = null,
	@Latitude float = null,
	@Altitude float = null
AS
	UPDATE stations
	SET Name = (CASE when @Name is null THEN Name else @Name end),
		Address = (CASE when @Address is null then Address else @Address end),
		City = (CASE when @City is null then City else @City end),
		Operator = (CASE when @Operator is null then Operator else @Operator end),
		Capacity = (CASE when @Capacity is null then Capacity else @Capacity end),
		Latitude = (CASE when @Latitude is null then Latitude else @Latitude end),
		Altitude = (CASE when @Altitude is null then Altitude else @Altitude end)
	WHERE StationId = @StationId
RETURN 0
