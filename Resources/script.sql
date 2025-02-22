USE [master]
GO
/****** Object:  Database [MyDataBase]    Script Date: 02.03.2016 15:37:13 ******/
CREATE DATABASE [MyDataBase]
GO
ALTER DATABASE [MyDataBase] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyDataBase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyDataBase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyDataBase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyDataBase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyDataBase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyDataBase] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyDataBase] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MyDataBase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyDataBase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyDataBase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyDataBase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyDataBase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyDataBase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyDataBase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyDataBase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyDataBase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MyDataBase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyDataBase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyDataBase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyDataBase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyDataBase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyDataBase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyDataBase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyDataBase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MyDataBase] SET  MULTI_USER 
GO
ALTER DATABASE [MyDataBase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyDataBase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyDataBase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyDataBase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MyDataBase] SET DELAYED_DURABILITY = DISABLED 
GO
USE [MyDataBase]
GO
/****** Object:  ApplicationRole [admin]    Script Date: 02.03.2016 15:37:14 ******/
/* To avoid disclosure of passwords, the password is generated in script. */
declare @idx as int
declare @randomPwd as nvarchar(64)
declare @rnd as float
select @idx = 0
select @randomPwd = N''
select @rnd = rand((@@CPU_BUSY % 100) + ((@@IDLE % 100) * 100) + 
       (DATEPART(ss, GETDATE()) * 10000) + ((cast(DATEPART(ms, GETDATE()) as int) % 100) * 1000000))
while @idx < 64
begin
   select @randomPwd = @randomPwd + char((cast((@rnd * 83) as int) + 43))
   select @idx = @idx + 1
select @rnd = rand()
end
declare @statement nvarchar(4000)
select @statement = N'CREATE APPLICATION ROLE [admin] WITH DEFAULT_SCHEMA = [db_datawriter], ' + N'PASSWORD = N' + QUOTENAME(@randomPwd,'''')
EXEC dbo.sp_executesql @statement

GO
USE [MyDataBase]
GO
/****** Object:  Sequence [dbo].[TestSeq]    Script Date: 02.03.2016 15:37:14 ******/
CREATE SEQUENCE [dbo].[TestSeq] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 9223372036854775807
 CYCLE 
 CACHE 
GO
/****** Object:  UserDefinedFunction [dbo].[timeFlight]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Batch submitted through debugger: SQLQuery5.sql|7|0|C:\Users\Lenovo\AppData\Local\Temp\~vs2B48.sql
CREATE FUNCTION [dbo].[timeFlight]
(
@timeArrival datetime,
@timeDeparture datetime
)
RETURNS char(5)
BEGIN
RETURN
convert(char(2),datediff(hh,@timeDeparture,@timeArrival))+':'+
convert(char(2),datediff(mi,@timeDeparture,@timeArrival)-datediff(hh,@timeDeparture,@timeArrival)*60)
END

GO
/****** Object:  Table [dbo].[Aircrafts]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Aircrafts](
	[AircraftId] [int] IDENTITY(1,1) NOT NULL,
	[MfdBy] [varchar](75) NOT NULL,
	[AircraftModel] [varchar](40) NOT NULL,
	[Capacity] [int] NOT NULL,
	[MfdOn] [date] NOT NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK__Aircrafts] PRIMARY KEY CLUSTERED 
(
	[AircraftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Class]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[ClassId] [int] IDENTITY(1,1) NOT NULL,
	[RtId] [int] NOT NULL,
	[FC] [int] NOT NULL,
	[BC] [int] NOT NULL,
	[EC] [int] NOT NULL,
	[AircraftId] [int] NOT NULL,
 CONSTRAINT [PK__Class__CB1927C06152016B] PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Clients]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clients](
	[ClientId] [int] IDENTITY(1,1) NOT NULL,
	[CLFName] [nvarchar](50) NOT NULL,
	[CLSName] [nvarchar](50) NOT NULL,
	[CLLName] [nvarchar](50) NOT NULL,
	[Sex] [nvarchar](10) NOT NULL,
	[Birthday] [date] NOT NULL,
	[Passport] [nvarchar](50) NOT NULL,
	[CreditCard] [varchar](50) NULL,
	[Tel] [float] NOT NULL,
	[EMail] [varchar](50) NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Code]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Code](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NOT NULL,
	[CityName] [nvarchar](255) NOT NULL,
	[CityCode] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Cities] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[CompanyId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](40) NOT NULL,
	[AirlineCode] [nchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Countries]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](255) NOT NULL,
	[CountryCode] [nvarchar](5) NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Discounts]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Discounts](
	[DiId] [int] NOT NULL,
	[Title] [varchar](40) NOT NULL,
	[Amount] [int] NOT NULL,
	[Description] [varchar](225) NULL,
PRIMARY KEY CLUSTERED 
(
	[DiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FlightPath]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlightPath](
	[PathId] [int] IDENTITY(1,1) NOT NULL,
	[Path] [nvarchar](50) NOT NULL,
	[RouteCode] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PathId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Flights]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Flights](
	[FlightId] [int] IDENTITY(1,1) NOT NULL,
	[FSN] [varchar](50) NOT NULL,
	[DepartureDate] [date] NOT NULL,
	[AircraftId] [int] NOT NULL,
	[FlightStatus] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Flights] PRIMARY KEY CLUSTERED 
(
	[FlightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OSN] [nvarchar](50) NOT NULL,
	[PlaceN] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Transac] [int] NOT NULL,
	[RtId] [int] NOT NULL,
	[Path] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
	[Class] [nvarchar](50) NOT NULL,
	[OrderStatus] [nvarchar](50) NOT NULL,
	[Remark] [varchar](10) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Places]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Places](
	[PLId] [int] IDENTITY(1,1) NOT NULL,
	[AircraftId] [int] NOT NULL,
	[PLFirstClass] [int] NOT NULL,
	[PLBusClass] [int] NOT NULL,
	[PLEcoClass] [int] NOT NULL,
 CONSTRAINT [PK_Places] PRIMARY KEY CLUSTERED 
(
	[PLId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Places] UNIQUE NONCLUSTERED 
(
	[PLId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Route]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Route](
	[RtId] [int] IDENTITY(1,1) NOT NULL,
	[RouteCode] [nvarchar](50) NOT NULL,
	[DepCityId] [int] NOT NULL,
	[ArrCityId] [int] NOT NULL,
	[DepDate] [date] NULL,
	[DepTime] [time](7) NOT NULL,
	[ArrDate] [date] NOT NULL,
	[ArrTime] [time](7) NOT NULL,
	[FlightId] [int] NOT NULL,
 CONSTRAINT [PK_Route] PRIMARY KEY CLUSTERED 
(
	[RtId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransId] [int] IDENTITY(1,1) NOT NULL,
	[BookingDate] [date] NOT NULL,
	[Charges] [int] NOT NULL,
	[PaidAmount] [int] NOT NULL,
	[Refund] [int] NOT NULL,
	[Total] [int] NOT NULL,
	[Type] [varchar](120) NOT NULL,
	[Remark] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[TransId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserLogin] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Tel] [float] NOT NULL,
	[Sex] [nvarchar](10) NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
	[FName] [nvarchar](20) NOT NULL,
	[LName] [nvarchar](20) NOT NULL,
	[SName] [nvarchar](20) NOT NULL,
	[MaritalStatus] [nvarchar](10) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[BCT]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BCT]
AS
SELECT DISTINCT dbo.Route.RtId, dbo.Class.BC, dbo.Route.DepCityId, DepCities.CityCode, dbo.Route.ArrCityId, ArrCities.CityCode AS ACode
FROM            dbo.Route INNER JOIN
                         dbo.Code AS DepCities ON DepCities.CityId = dbo.Route.DepCityId INNER JOIN
                         dbo.Code AS ArrCities ON ArrCities.CityId = dbo.Route.ArrCityId INNER JOIN
                         dbo.Class ON dbo.Route.RtId = dbo.Class.RtId

GO
/****** Object:  View [dbo].[ECT]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ECT]
AS
SELECT DISTINCT dbo.Route.RtId, dbo.Class.EC, dbo.Route.DepCityId, DepCities.CityCode, dbo.Route.ArrCityId, ArrCities.CityCode AS ACode
FROM            dbo.Route INNER JOIN
                         dbo.Code AS DepCities ON DepCities.CityId = dbo.Route.DepCityId INNER JOIN
                         dbo.Code AS ArrCities ON ArrCities.CityId = dbo.Route.ArrCityId INNER JOIN
                         dbo.Class ON dbo.Route.RtId = dbo.Class.RtId

GO
/****** Object:  View [dbo].[FCT]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[FCT]
AS
SELECT DISTINCT dbo.Route.RtId, dbo.Class.FC, dbo.Route.DepCityId, DepCities.CityCode AS DCode, dbo.Route.ArrCityId, ArrCities.CityCode AS ACode
FROM            dbo.Route INNER JOIN
                         dbo.Code AS DepCities ON DepCities.CityId = dbo.Route.DepCityId INNER JOIN
                         dbo.Code AS ArrCities ON ArrCities.CityId = dbo.Route.ArrCityId INNER JOIN
                         dbo.Class ON dbo.Route.RtId = dbo.Class.RtId

GO
/****** Object:  View [dbo].[Invoices]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Invoices]
AS
SELECT        dbo.Transactions.TransId, dbo.Orders.OSN, dbo.Users.UserLogin,
                             (SELECT        dbo.Clients.CLFName + ' ' + dbo.Clients.CLSName + ' ' + dbo.Clients.CLLName AS Exp1) AS [Client Name], dbo.FlightPath.Path, dbo.Transactions.BookingDate, dbo.Transactions.Charges, 
                         dbo.Transactions.PaidAmount, dbo.Transactions.Total, dbo.Transactions.Type
FROM            dbo.Orders INNER JOIN
                         dbo.Route ON dbo.Orders.RtId = dbo.Route.RtId INNER JOIN
                         dbo.Transactions ON dbo.Orders.Transac = dbo.Transactions.TransId INNER JOIN
                         dbo.Class ON dbo.Route.RtId = dbo.Class.RtId INNER JOIN
                         dbo.Flights ON dbo.Route.FlightId = dbo.Flights.FlightId INNER JOIN
                         dbo.Users ON dbo.Orders.UserId = dbo.Users.UserId INNER JOIN
                         dbo.Clients ON dbo.Orders.ClientId = dbo.Clients.ClientId INNER JOIN
                         dbo.FlightPath ON dbo.FlightPath.PathId = dbo.Orders.Path

GO
/****** Object:  View [dbo].[TrancReport]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TrancReport]
AS
SELECT        dbo.Transactions.TransId, dbo.Orders.OSN, dbo.Flights.FSN, dbo.Users.UserLogin, dbo.Users.Address, dbo.Users.Tel, dbo.Users.Email,
                             (SELECT        dbo.Clients.CLFName + ' ' + dbo.Clients.CLSName + ' ' + dbo.Clients.CLLName AS Exp1) AS [Client Name], dbo.FlightPath.Path, dbo.Orders.Class, dbo.Transactions.BookingDate, 
                         dbo.Route.DepDate, dbo.Route.DepTime, dbo.Route.ArrDate, dbo.Route.ArrTime, dbo.Transactions.Charges, dbo.Transactions.PaidAmount, dbo.Transactions.Total, dbo.Transactions.Type, 
                         dbo.Transactions.Remark
FROM            dbo.Transactions INNER JOIN
                         dbo.Orders ON dbo.Transactions.TransId = dbo.Orders.Transac INNER JOIN
                         dbo.Users ON dbo.Orders.UserId = dbo.Users.UserId INNER JOIN
                         dbo.Clients ON dbo.Orders.ClientId = dbo.Clients.ClientId INNER JOIN
                         dbo.FlightPath ON dbo.Orders.Path = dbo.FlightPath.PathId INNER JOIN
                         dbo.Route ON dbo.Orders.RtId = dbo.Route.RtId INNER JOIN
                         dbo.Flights ON dbo.Route.FlightId = dbo.Flights.FlightId

GO
/****** Object:  Index [IX_Fligts_AircraftId]    Script Date: 02.03.2016 15:37:14 ******/
CREATE NONCLUSTERED INDEX [IX_Fligts_AircraftId] ON [dbo].[Flights]
(
	[AircraftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_ClientId]    Script Date: 02.03.2016 15:37:14 ******/
CREATE NONCLUSTERED INDEX [IX_Orders_ClientId] ON [dbo].[Orders]
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_FlightId]    Script Date: 02.03.2016 15:37:14 ******/
CREATE NONCLUSTERED INDEX [IX_Orders_FlightId] ON [dbo].[Orders]
(
	[RtId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_UserId]    Script Date: 02.03.2016 15:37:14 ******/
CREATE NONCLUSTERED INDEX [IX_Orders_UserId] ON [dbo].[Orders]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Aircrafts]  WITH CHECK ADD  CONSTRAINT [FK_Aircrafts_Companies] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([CompanyId])
GO
ALTER TABLE [dbo].[Aircrafts] CHECK CONSTRAINT [FK_Aircrafts_Companies]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Aircrafts] FOREIGN KEY([AircraftId])
REFERENCES [dbo].[Aircrafts] ([AircraftId])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Aircrafts]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Route] FOREIGN KEY([RtId])
REFERENCES [dbo].[Route] ([RtId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Route]
GO
ALTER TABLE [dbo].[Code]  WITH CHECK ADD  CONSTRAINT [PK_Cities_1] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([CountryId])
GO
ALTER TABLE [dbo].[Code] CHECK CONSTRAINT [PK_Cities_1]
GO
ALTER TABLE [dbo].[Flights]  WITH CHECK ADD  CONSTRAINT [FK_Flights_Aircrafts] FOREIGN KEY([AircraftId])
REFERENCES [dbo].[Aircrafts] ([AircraftId])
GO
ALTER TABLE [dbo].[Flights] CHECK CONSTRAINT [FK_Flights_Aircrafts]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Clients] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([ClientId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Clients]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_FlightPath] FOREIGN KEY([Path])
REFERENCES [dbo].[FlightPath] ([PathId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_FlightPath]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Route] FOREIGN KEY([RtId])
REFERENCES [dbo].[Route] ([RtId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Route]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Transactions] FOREIGN KEY([Transac])
REFERENCES [dbo].[Transactions] ([TransId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Transactions]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
ALTER TABLE [dbo].[Places]  WITH CHECK ADD  CONSTRAINT [FK_Places_Aircrafts] FOREIGN KEY([AircraftId])
REFERENCES [dbo].[Aircrafts] ([AircraftId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Places] CHECK CONSTRAINT [FK_Places_Aircrafts]
GO
ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK_Route_Flights] FOREIGN KEY([FlightId])
REFERENCES [dbo].[Flights] ([FlightId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_Route_Flights]
GO
/****** Object:  StoredProcedure [dbo].[NewFSN]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[NewFSN] 
AS
BEGIN
	DECLARE @MaxNo INT, @No VARCHAR (50);
	    SELECT @MaxNo = ISNULL( MAX (CAST(RIGHT(FSN, 6) AS INT)), 0) + 1 FROM Flights;
		SELECT 'FSN' + RIGHT( CAST(YEAR(CURRENT_TIMESTAMP) AS VARCHAR) + RIGHT('00000' + CAST(@MaxNo AS VARCHAR), 6), 10);
END

GO
/****** Object:  StoredProcedure [dbo].[NewOSN]    Script Date: 02.03.2016 15:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[NewOSN] 
AS
BEGIN
	DECLARE @MaxNo INT, @No VARCHAR (50);
	    SELECT @MaxNo = ISNULL( MAX (CAST(RIGHT(OSN, 6) AS INT)), 0) + 1 FROM Orders;
		SELECT 'OSN' + RIGHT( CAST(YEAR(CURRENT_TIMESTAMP) AS VARCHAR) + RIGHT('00000' + CAST(@MaxNo AS VARCHAR), 6), 10);
END


GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[60] 2[15] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 2
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -134
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Class"
            Begin Extent = 
               Top = 0
               Left = 507
               Bottom = 166
               Right = 681
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Route"
            Begin Extent = 
               Top = 8
               Left = 29
               Bottom = 211
               Right = 203
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DepCities"
            Begin Extent = 
               Top = 169
               Left = 506
               Bottom = 299
               Right = 680
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ArrCities"
            Begin Extent = 
               Top = 308
               Left = 505
               Bottom = 438
               Right = 679
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'BCT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'BCT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'BCT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Route"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DepCities"
            Begin Extent = 
               Top = 39
               Left = 270
               Bottom = 169
               Right = 444
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ArrCities"
            Begin Extent = 
               Top = 59
               Left = 467
               Bottom = 189
               Right = 641
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Class"
            Begin Extent = 
               Top = 0
               Left = 748
               Bottom = 130
               Right = 922
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ECT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ECT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ECT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Route"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DepCities"
            Begin Extent = 
               Top = 35
               Left = 273
               Bottom = 165
               Right = 447
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ArrCities"
            Begin Extent = 
               Top = 52
               Left = 484
               Bottom = 182
               Right = 658
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Class"
            Begin Extent = 
               Top = 0
               Left = 675
               Bottom = 130
               Right = 849
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'FCT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'FCT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'FCT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[55] 2[19] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 2
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = -341
      End
      Begin Tables = 
         Begin Table = "Orders"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "Route"
            Begin Extent = 
               Top = 6
               Left = 250
               Bottom = 136
               Right = 424
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "Transactions"
            Begin Extent = 
               Top = 6
               Left = 462
               Bottom = 136
               Right = 636
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "Class"
            Begin Extent = 
               Top = 6
               Left = 674
               Bottom = 136
               Right = 848
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Flights"
            Begin Extent = 
               Top = 6
               Left = 886
               Bottom = 136
               Right = 1060
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "Clients"
            Begin Extent = 
               Top = 138
               Left = 250
               Bottom = 268
               Right = 424
            End
            DisplayFlags = 280
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Invoices'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'         TopColumn = 0
         End
         Begin Table = "FlightPath"
            Begin Extent = 
               Top = 138
               Left = 460
               Bottom = 251
               Right = 634
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 20
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Invoices'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Invoices'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[61] 2[14] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 2
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Transactions"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "Orders"
            Begin Extent = 
               Top = 6
               Left = 250
               Bottom = 136
               Right = 424
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "Flights"
            Begin Extent = 
               Top = 6
               Left = 462
               Bottom = 136
               Right = 636
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 674
               Bottom = 136
               Right = 848
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "Clients"
            Begin Extent = 
               Top = 6
               Left = 886
               Bottom = 136
               Right = 1060
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "FlightPath"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 251
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Route"
            Begin Extent = 
               Top = 138
               Left = 250
               Bottom = 268
               Right = 424
            End
            DisplayFlags = 280
 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TrancReport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'           TopColumn = 4
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TrancReport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TrancReport'
GO
USE [master]
GO
ALTER DATABASE [MyDataBase] SET  READ_WRITE 
GO
