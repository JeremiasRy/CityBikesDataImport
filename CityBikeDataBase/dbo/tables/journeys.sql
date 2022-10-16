CREATE TABLE [dbo].[journeys](
	[DepartureDate] [date] NULL,
	[DepartureStationId] [char](3) NULL,
	[ReturnStationId] [char](3) NULL,
	[Distance] [int] NULL,
	[Duration] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,

	CONSTRAINT PK_journeys PRIMARY KEY CLUSTERED(Id), 
	CONSTRAINT FK_DepStationId FOREIGN KEY (DepartureStationId) REFERENCES [dbo].[stations] ([StationId]),
	CONSTRAINT FK_RetStationId FOREIGN KEY (ReturnStationId) REFERENCES [dbo].[stations] ([StationId]))

	



