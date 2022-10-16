CREATE TABLE [dbo].[stations](
	[StationId] [char](3) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Operator] [nvarchar](50) NULL,
	[Capacity] [int] NULL,
	[Latitude] [float] NULL,
	[Altitude] [float] NULL,

	CONSTRAINT PK_StationId PRIMARY KEY CLUSTERED ([StationId]))
