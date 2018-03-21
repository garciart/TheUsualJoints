USE [theusualjoints]
GO

/****** Object:  StoredProcedure [dbo].[AddAdvertiser]    Script Date: 01/11/2015 23:27:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddAdvertiser]
(@CityID int, @BlockID int, @ImageUrl nvarchar(255), @NavigateUrl nvarchar(255), @AlternateText nvarchar(100), @Keyword nvarchar(50), @Impressions int, @AdvertiserID int OUTPUT)
AS
-- Create the new advertiser entry
INSERT INTO Advertiser (BlockID, ImageUrl, NavigateUrl, AlternateText, Keyword, Impressions)
VALUES (@BlockID, @ImageUrl, @NavigateUrl, @AlternateText, @Keyword, @Impressions)
-- Save the generated ID to a variable
SELECT @AdvertiserID = @@Identity
-- Associate with a city
INSERT INTO AdvertiserCity (AdvertiserID, CityID)
VALUES (@AdvertiserID, @CityID)

GO

/****** Object:  StoredProcedure [dbo].[AddCity]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddCity]
(@StateID int, @CityName nvarchar(50), @ZipCode char(6), @TimeZoneID int, @Active bit, @CityID int OUTPUT)
AS
INSERT INTO City (StateID, CityName, ZipCode, TimeZoneID, Active)
VALUES (@StateID, @CityName, @ZipCode, @TimeZoneID, @Active)
-- Save the generated ID to a variable
SELECT @CityID = @@Identity

GO

/****** Object:  StoredProcedure [dbo].[AddCountry]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddCountry]
(@CountryName nvarchar(50), @CountryAbbreviation char(2), @Active bit, @CountryID int OUTPUT)
AS
INSERT INTO Country (CountryName, CountryAbbreviation, Active)
VALUES (@CountryName, @CountryAbbreviation, @Active)
-- Save the generated ID to a variable
SELECT @CountryID = @@Identity

GO

/****** Object:  StoredProcedure [dbo].[AddEstablishment]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddEstablishment]
(@CityID int, @EstablishmentName nvarchar(50), @Priority int, @Tier int, @Logo nvarchar(50), @Motto nvarchar(50), @Cuisine nvarchar(50), @StreetAddress nvarchar(50), @CityName nvarchar(50), @StateAbbreviation char(2), @ZipCode char(6), @Latitude float, @Longitude float, @TelephoneNo char(14), @FaxNo char(14), @Email nvarchar(100), @WebSite nvarchar(100), @Facebook nvarchar(100), @AboutUs nvarchar(MAX), @Photo01 nvarchar(50), @Photo02 nvarchar(50), @Photo03 nvarchar(50), @Photo04 nvarchar(50), @Photo05 nvarchar(50), @EstablishmentID int OUTPUT)
AS
-- Create the new establishmnet entry
INSERT INTO Establishment (EstablishmentName, Priority, Tier, Logo, Motto, Cuisine, StreetAddress, CityName, StateAbbreviation, ZipCode, Latitude, Longitude, TelephoneNo, FaxNo, Email, WebSite, Facebook, AboutUs, Photo01, Photo02, Photo03, Photo04, Photo05)
VALUES (@EstablishmentName, @Priority, @Tier, @Logo, @Motto, @Cuisine, @StreetAddress, @CityName, @StateAbbreviation, @ZipCode, @Latitude, @Longitude, @TelephoneNo, @FaxNo, @Email, @WebSite, @Facebook, @AboutUs, @Photo01, @Photo02, @Photo03, @Photo04, @Photo05)
-- Save the generated ID to a variable
SELECT @EstablishmentID = @@Identity
-- Associate with a city
INSERT INTO EstablishmentCity (EstablishmentID, CityID)
VALUES (@EstablishmentID, @CityID)

GO

/****** Object:  StoredProcedure [dbo].[AddEvent]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddEvent]
(@CityID int,
@EventName nvarchar(50),
@Date smalldatetime,
@EndDate smalldatetime,
@Description nvarchar(300),
@ImageUrl nvarchar(50),
@NavigateUrl nvarchar(100),
@EventID int OUTPUT)
AS
-- Create the new event entry
INSERT INTO Event (EventName, Date, EndDate, Description, ImageUrl, NavigateUrl)
VALUES (@EventName, @Date, @EndDate, @Description, @ImageUrl, @NavigateUrl)
-- Save the generated ID to a variable
SELECT @EventID = @@Identity
-- Associate with a city
INSERT INTO EventCity (EventID, CityID)
VALUES (@EventID, @CityID)

GO

/****** Object:  StoredProcedure [dbo].[AddLink]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddLink]
(@CityID int,
@LinkName nvarchar(50),
@LinkUrl nvarchar(100),
@LinkTitle nvarchar(100),
@LinkCategoryName nvarchar(50),
@LinkCategoryRank int,
@LinkID int OUTPUT)
AS
-- Create the new link entry
INSERT INTO Link (LinkName, LinkUrl, LinkTitle, LinkCategoryName, LinkCategoryRank)
VALUES (@LinkName, @LinkUrl, @LinkTitle, @LinkCategoryName, @LinkCategoryRank)
-- Save the generated ID to a variable
SELECT @LinkID = @@Identity
-- Associate with a city
INSERT INTO LinkCity (LinkID, CityID)
VALUES (@LinkID, @CityID)

GO

/****** Object:  StoredProcedure [dbo].[AddRoutineEstablishmentSchedule]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddRoutineEstablishmentSchedule]
(@EstablishmentID int,
@EstablishmentName nvarchar(50),
@Weekday int,
@HoursOfOperation nvarchar(50),
@HappyHourTimes nvarchar(50),
@HappyHourSpecials nvarchar(300),
@FoodAndDrinkSpecials nvarchar(300),
@SpecialEvents nvarchar(300),
@EstablishmentRoutineEventID int OUTPUT)
AS
-- Create the new Routine Establishment Schedule
INSERT INTO RoutineEstablishmentEvents (EstablishmentID, EstablishmentName, Weekday, HoursOfOperation, HappyHourTimes, HappyHourSpecials, FoodAndDrinkSpecials, SpecialEvents)
VALUES (@EstablishmentID, @EstablishmentName, @Weekday, @HoursOfOperation, @HappyHourTimes, @HappyHourSpecials, @FoodAndDrinkSpecials, @SpecialEvents)
-- Save the generated ID to a variable
SELECT @EstablishmentRoutineEventID = @@Identity

GO

/****** Object:  StoredProcedure [dbo].[AddSpecialEstablishmentSchedule]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddSpecialEstablishmentSchedule]
(@EstablishmentId int,
@EstablishmentName nvarchar(50),
@Date smallDateTime,
@Description nvarchar(50),
@TodaysHoursOfOperation nvarchar(50),
@TodaysHappyHourTimes nvarchar(50),
@TodaysHappyHourSpecials nvarchar(300),
@THSAppendOrReplaceFlag int,
@TodaysFoodAndDrinkSpecials nvarchar(300),
@TFSAppendOrReplaceFlag int,
@TodaysSpecialEvents nvarchar(300),
@TSEAppendOrReplaceFlag int,
@EstablishmentSpecialEventID int OUTPUT)
AS
-- Create the new Special Establishment Schedule
-- Prevent duplicate dates
IF NOT EXISTS
(
	SELECT * FROM SpecialEstablishmentEvents
	WHERE SpecialEstablishmentEvents.EstablishmentId = @EstablishmentId AND SpecialEstablishmentEvents.Date = @Date
)
INSERT INTO SpecialEstablishmentEvents (EstablishmentId, EstablishmentName, Date, Description, TodaysHoursOfOperation, TodaysHappyHourTimes, TodaysHappyHourSpecials, THSAppendOrReplaceFlag, TodaysFoodAndDrinkSpecials, TFSAppendOrReplaceFlag, TodaysSpecialEvents, TSEAppendOrReplaceFlag)
VALUES (@EstablishmentId, @EstablishmentName, @Date, @Description, @TodaysHoursOfOperation, @TodaysHappyHourTimes, @TodaysHappyHourSpecials, @THSAppendOrReplaceFlag, @TodaysFoodAndDrinkSpecials, @TFSAppendOrReplaceFlag, @TodaysSpecialEvents, @TSEAppendOrReplaceFlag)
-- Save the generated ID to a variable
SELECT @EstablishmentSpecialEventID = @@Identity

GO

/****** Object:  StoredProcedure [dbo].[AddState]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddState]
(@CountryID int, @StateName nvarchar(50), @StateAbbreviation char(2), @Active bit, @StateID int OUTPUT)
AS
INSERT INTO State (CountryID, StateName, StateAbbreviation, Active)
VALUES (@CountryID, @StateName, @StateAbbreviation, @Active)
-- Save the generated ID to a variable
SELECT @StateID = @@Identity

GO

/****** Object:  StoredProcedure [dbo].[AssignAdvertiserToCity]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AssignAdvertiserToCity]
(@AdvertiserID int, @CityID int)
AS
INSERT INTO AdvertiserCity (AdvertiserID, CityID)
VALUES (@AdvertiserID, @CityID)

GO

/****** Object:  StoredProcedure [dbo].[AssignEstablishmentToCity]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AssignEstablishmentToCity]
(@EstablishmentID int, @CityID int)
AS
INSERT INTO EstablishmentCity (EstablishmentID, CityID)
VALUES (@EstablishmentID, @CityID)

GO

/****** Object:  StoredProcedure [dbo].[AssignEventToCity]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AssignEventToCity]
(@EventID int, @CityID int)
AS
INSERT INTO EventCity (EventID, CityID)
VALUES (@EventID, @CityID)

GO

/****** Object:  StoredProcedure [dbo].[AssignLinkToCity]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AssignLinkToCity]
(@LinkID int, @CityID int)
AS
INSERT INTO LinkCity (LinkID, CityID)
VALUES (@LinkID, @CityID)

GO

/****** Object:  StoredProcedure [dbo].[DeleteAdvertiser]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteAdvertiser]
(@AdvertiserID int)
AS
DELETE FROM AdvertiserCity
WHERE AdvertiserID = @AdvertiserID
DELETE FROM Advertiser
WHERE AdvertiserID = @AdvertiserID

GO

/****** Object:  StoredProcedure [dbo].[DeleteCity]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteCity]
(@CityID int)
AS
DELETE FROM City
WHERE CityID = @CityID

GO

/****** Object:  StoredProcedure [dbo].[DeleteCountry]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteCountry]
(@CountryID int)
AS
DELETE FROM Country
WHERE CountryID = @CountryID

GO

/****** Object:  StoredProcedure [dbo].[DeleteEstablishment]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteEstablishment]
(@EstablishmentID int)
AS
DELETE FROM EstablishmentCity
WHERE EstablishmentID = @EstablishmentID
DELETE FROM Establishment
WHERE EstablishmentID = @EstablishmentID

GO

/****** Object:  StoredProcedure [dbo].[DeleteEvent]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteEvent]
(@EventID int)
AS
DELETE FROM EventCity
WHERE EventID = @EventID
DELETE FROM Event
WHERE EventID = @EventID

GO

/****** Object:  StoredProcedure [dbo].[DeleteLink]    Script Date: 01/11/2015 23:27:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteLink]
(@LinkID int)
AS
DELETE FROM LinkCity
WHERE LinkID = @LinkID
DELETE FROM Link
WHERE LinkID = @LinkID

GO

/****** Object:  StoredProcedure [dbo].[DeleteRoutineEstablishmentSchedule]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteRoutineEstablishmentSchedule]
(@EstablishmentRoutineEventID int)
AS
DELETE FROM RoutineEstablishmentEvents
WHERE EstablishmentRoutineEventID = @EstablishmentRoutineEventID
GO

/****** Object:  StoredProcedure [dbo].[DeleteSpecialEstablishmentSchedule]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteSpecialEstablishmentSchedule]
(@EstablishmentSpecialEventID int)
AS
DELETE FROM SpecialEstablishmentEvents
WHERE EstablishmentSpecialEventID = @EstablishmentSpecialEventID

GO

/****** Object:  StoredProcedure [dbo].[DeleteState]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteState]
(@StateID int)
AS
DELETE FROM State
WHERE StateID = @StateID

GO

/****** Object:  StoredProcedure [dbo].[Get30DaySpecialEstablishmentSchedule]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Get30DaySpecialEstablishmentSchedule]
(@EstablishmentID int,
@Date smallDateTime)
AS
SELECT EstablishmentSpecialEventID, Date, Description, TodaysHoursOfOperation, TodaysHappyHourTimes,
TodaysHappyHourSpecials, THSAppendOrReplaceFlag,
TodaysFoodAndDrinkSpecials, TFSAppendOrReplaceFlag,
TodaysSpecialEvents, TSEAppendOrReplaceFlag
FROM SpecialEstablishmentEvents
WHERE EstablishmentID = @EstablishmentID
  AND Date >= @Date
  AND Date <= DATEADD (day, 30, @Date)
ORDER BY Date
GO

/****** Object:  StoredProcedure [dbo].[GetAdvertiserDetails]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAdvertiserDetails]
(@AdvertiserID int)
AS
SELECT BlockID, ImageUrl, NavigateUrl, AlternateText, Keyword, Impressions
FROM Advertiser
WHERE AdvertiserID = @AdvertiserID
GO

/****** Object:  StoredProcedure [dbo].[GetAdvertisers]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAdvertisers]
AS
SELECT Advertiser.AdvertiserID, Advertiser.BlockID, Advertiser.ImageUrl, Advertiser.NavigateUrl, Advertiser.AlternateText, Advertiser.Keyword, Advertiser.Impressions
FROM Advertiser
ORDER BY Advertiser.AlternateText ASC

GO

/****** Object:  StoredProcedure [dbo].[GetAdvertisersInCity]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAdvertisersInCity]
(@CityID int)
AS
BEGIN
	SELECT Advertiser.AdvertiserID, Advertiser.BlockID, Advertiser.ImageUrl, Advertiser.NavigateUrl, Advertiser.AlternateText, Advertiser.Keyword, Advertiser.Impressions
	FROM Advertiser INNER JOIN AdvertiserCity
	ON Advertiser.AdvertiserID = AdvertiserCity.AdvertiserID
	WHERE AdvertiserCity.CityID = @CityID
	ORDER BY Advertiser.BlockID
END
GO

/****** Object:  StoredProcedure [dbo].[GetAdvertiserUserName]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAdvertiserUserName]
(@AdvertiserID int)
AS
SELECT UserName
FROM Advertiser
WHERE AdvertiserID = @AdvertiserID

GO

/****** Object:  StoredProcedure [dbo].[GetCitiesInState]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCitiesInState]
(@StateID int, @Active bit)
AS
IF @Active = 'TRUE'
BEGIN
	SELECT CityID, CityName, ZipCode, TimeZoneID, Active
	FROM City
	WHERE StateID = @StateID AND Active = 'TRUE'
	ORDER BY CityName ASC
END
ELSE
BEGIN
	SELECT CityID, CityName, ZipCode, TimeZoneID, Active
	FROM City
	WHERE StateID = @StateID
	ORDER BY CityName ASC
END

GO

/****** Object:  StoredProcedure [dbo].[GetCitiesWithAdvertiser]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCitiesWithAdvertiser]
(@AdvertiserID int)
AS
SELECT City.CityID, City.CityName
FROM City INNER JOIN AdvertiserCity
ON City.CityID = AdvertiserCity.CityID
WHERE AdvertiserCity.AdvertiserID = @AdvertiserID

GO

/****** Object:  StoredProcedure [dbo].[GetCitiesWithEstablishment]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCitiesWithEstablishment]
(@EstablishmentID int)
AS
SELECT City.CityID, City.CityName
FROM City INNER JOIN EstablishmentCity
ON City.CityID = EstablishmentCity.CityID
WHERE EstablishmentCity.EstablishmentID = @EstablishmentID

GO

/****** Object:  StoredProcedure [dbo].[GetCitiesWithEvent]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCitiesWithEvent]
(@EventID int)
AS
SELECT City.CityID, City.CityName
FROM City INNER JOIN EventCity
ON City.CityID = EventCity.CityID
WHERE EventCity.EventID = @EventID

GO

/****** Object:  StoredProcedure [dbo].[GetCitiesWithLink]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCitiesWithLink]
(@LinkID int)
AS
SELECT City.CityID, City.CityName
FROM City INNER JOIN LinkCity
ON City.CityID = LinkCity.CityID
WHERE LinkCity.LinkID = @LinkID

GO

/****** Object:  StoredProcedure [dbo].[GetCitiesWithoutAdvertiser]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCitiesWithoutAdvertiser]
(@AdvertiserID int)
AS
SELECT City.CityID, City.CityName
FROM City
WHERE CityID NOT IN
	(SELECT City.CityID
	FROM City INNER JOIN AdvertiserCity
	ON City.CityID = AdvertiserCity.CityID
	WHERE AdvertiserCity.AdvertiserID = @AdvertiserID)

GO

/****** Object:  StoredProcedure [dbo].[GetCitiesWithoutEstablishment]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCitiesWithoutEstablishment]
(@EstablishmentID int)
AS
SELECT City.CityID, City.CityName
FROM City
WHERE CityID NOT IN
	(SELECT City.CityID
	FROM City INNER JOIN EstablishmentCity
	ON City.CityID = EstablishmentCity.CityID
	WHERE EstablishmentCity.EstablishmentID = @EstablishmentID)

GO

/****** Object:  StoredProcedure [dbo].[GetCitiesWithoutEvent]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCitiesWithoutEvent]
(@EventID int)
AS
SELECT City.CityID, City.CityName
FROM City
WHERE CityID NOT IN
	(SELECT City.CityID
	FROM City INNER JOIN EventCity
	ON City.CityID = EventCity.CityID
	WHERE EventCity.EventID = @EventID)

GO

/****** Object:  StoredProcedure [dbo].[GetCitiesWithoutLink]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCitiesWithoutLink]
(@LinkID int)
AS
SELECT City.CityID, City.CityName
FROM City
WHERE CityID NOT IN
	(SELECT City.CityID
	FROM City INNER JOIN LinkCity
	ON City.CityID = LinkCity.CityID
	WHERE LinkCity.LinkID = @LinkID)

GO

/****** Object:  StoredProcedure [dbo].[GetCityDetails]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCityDetails]
(@CityID int)
AS
SELECT StateID, CityName, Active, ZipCode, TimeZoneID
FROM City
WHERE CityID = @CityID

GO

/****** Object:  StoredProcedure [dbo].[GetCountries]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCountries]
(@Active bit)
AS
IF @Active = 'TRUE'
BEGIN
	SELECT CountryID, CountryName, CountryAbbreviation, Active
	FROM Country
	WHERE Active = 'TRUE'
	ORDER BY CountryName ASC
END
ELSE
BEGIN
	SELECT CountryID, CountryName, CountryAbbreviation, Active
	FROM Country
	ORDER BY CountryName ASC
END

GO

/****** Object:  StoredProcedure [dbo].[GetCountryDetails]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCountryDetails]
(@CountryID int)
AS
SELECT CountryName, CountryAbbreviation, Active
FROM Country
WHERE CountryID = @CountryID
ORDER BY CountryName ASC

GO

/****** Object:  StoredProcedure [dbo].[GetEstablishmentDetails]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEstablishmentDetails]
(@EstablishmentID int)
AS
SELECT EstablishmentID, EstablishmentName, Priority, Tier, Logo, Motto, Cuisine, StreetAddress, CityName, StateAbbreviation, ZipCode, Latitude, Longitude, TelephoneNo, FaxNo, Email, WebSite, Facebook, AboutUs, Photo01, Photo02, Photo03, Photo04, Photo05
FROM Establishment
WHERE EstablishmentID = @EstablishmentID

GO

/****** Object:  StoredProcedure [dbo].[GetEstablishments]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEstablishments]
AS
SELECT Establishment.EstablishmentID, Establishment.EstablishmentName, Establishment.Priority, Establishment.Tier, Establishment.Logo, Establishment.Motto, Establishment.Cuisine, Establishment.StreetAddress, Establishment.CityName, Establishment.StateAbbreviation, Establishment.ZipCode, Establishment.Latitude, Establishment.Longitude, Establishment.TelephoneNo, Establishment.FaxNo, Establishment.Email, Establishment.WebSite, Establishment.Facebook, Establishment.AboutUs, Establishment.Photo01, Establishment.Photo02, Establishment.Photo03, Establishment.Photo04, Establishment.Photo05
FROM Establishment
ORDER BY (REPLACE(REPLACE(REPLACE(Establishment.EstablishmentName, 'the ', ''), 'an ', ''), 'a ', ''))

GO

/****** Object:  StoredProcedure [dbo].[GetEstablishmentsInCity]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEstablishmentsInCity]
(@CityID int)
AS
BEGIN
	SELECT Establishment.EstablishmentID, Establishment.EstablishmentName, Establishment.Priority, Establishment.Tier, Establishment.Logo, Establishment.Motto, Establishment.Cuisine, Establishment.StreetAddress, Establishment.CityName, Establishment.StateAbbreviation, Establishment.ZipCode, Establishment.Latitude, Establishment.Longitude, Establishment.TelephoneNo, Establishment.FaxNo, Establishment.Email, Establishment.WebSite, Establishment.Facebook, Establishment.AboutUs, Establishment.Photo01, Establishment.Photo02, Establishment.Photo03, Establishment.Photo04, Establishment.Photo05, Establishment.Active
	FROM Establishment INNER JOIN EstablishmentCity
	ON Establishment.EstablishmentID = EstablishmentCity.EstablishmentID
	WHERE EstablishmentCity.CityID = @CityID
	ORDER BY Establishment.Priority
END
GO

/****** Object:  StoredProcedure [dbo].[GetEstablishmentUserName]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEstablishmentUserName]
(@EstablishmentID int)
AS
SELECT UserName
FROM Establishment
WHERE EstablishmentID = @EstablishmentID

GO

/****** Object:  StoredProcedure [dbo].[GetEventDetails]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEventDetails]
(@EventID int)
AS
SELECT EventName, Date, EndDate, Description, ImageUrl, NavigateUrl
FROM Event
WHERE EventID = @EventID

GO

/****** Object:  StoredProcedure [dbo].[GetEvents]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEvents]
AS
SELECT Event.EventID, Event.EventName, Event.Date, Event.EndDate,
	Event.Description, Event.ImageUrl, Event.NavigateUrl
FROM Event
ORDER BY Event.EventName ASC

GO

/****** Object:  StoredProcedure [dbo].[GetEventsInCity]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEventsInCity]
(@CityID int, @All bit)
AS
IF @All = 'TRUE'
BEGIN
	SELECT Event.EventID, Event.EventName, Event.Date, Event.EndDate, Event.Description, Event.ImageUrl, Event.NavigateUrl
	FROM Event INNER JOIN EventCity
	ON Event.EventID = EventCity.EventID
	WHERE EventCity.CityID = @CityID
	ORDER BY Event.Date
END
ELSE
BEGIN
	SELECT Event.EventID, Event.EventName, Event.Date, Event.EndDate, Event.Description, Event.ImageUrl, Event.NavigateUrl
	FROM Event INNER JOIN EventCity
	ON Event.EventID = EventCity.EventID
	WHERE EventCity.CityID = @CityID
	  AND Date >= GetDate()
	  AND Date <= DATEADD (day, 30, GetDate())
	ORDER BY Event.Date
END

GO

/****** Object:  StoredProcedure [dbo].[GetLinkDetails]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetLinkDetails]
(@LinkID int)
AS
SELECT LinkName, LinkUrl, LinkTitle, LinkCategoryName, LinkCategoryRank
FROM Link
WHERE LinkID = @LinkID

GO

/****** Object:  StoredProcedure [dbo].[GetLinks]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetLinks]
AS
SELECT Link.LinkID, Link.LinkName, Link.LinkUrl,
	Link.LinkTitle, Link.LinkCategoryName, Link.LinkCategoryRank
FROM Link 
ORDER BY Link.LinkName ASC

GO

/****** Object:  StoredProcedure [dbo].[GetLinksInCity]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetLinksInCity]
(@CityID int)
AS
SELECT Link.LinkID, Link.LinkName, Link.LinkUrl, Link.LinkTitle, Link.LinkCategoryName, Link.LinkCategoryRank
FROM Link INNER JOIN LinkCity
  ON Link.LinkID = LinkCity.LinkID
WHERE LinkCity.CityID = @CityID
ORDER BY Link.LinkCategoryRank

GO

/****** Object:  StoredProcedure [dbo].[GetRoutineEstablishmentSchedule]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRoutineEstablishmentSchedule]
(@EstablishmentID int)
AS
SELECT EstablishmentRoutineEventID, EstablishmentName, Weekday, HoursOfOperation, HappyHourTimes,
HappyHourSpecials, FoodAndDrinkSpecials, SpecialEvents
FROM RoutineEstablishmentEvents
WHERE EstablishmentID = @EstablishmentID
ORDER BY Weekday

GO

/****** Object:  StoredProcedure [dbo].[GetSpecialEstablishmentSchedule]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSpecialEstablishmentSchedule]
(@EstablishmentID int)
AS
SELECT EstablishmentSpecialEventID, Date, Description, TodaysHoursOfOperation, TodaysHappyHourTimes,
TodaysHappyHourSpecials, THSAppendOrReplaceFlag,
TodaysFoodAndDrinkSpecials, TFSAppendOrReplaceFlag,
TodaysSpecialEvents, TSEAppendOrReplaceFlag
FROM SpecialEstablishmentEvents
WHERE EstablishmentID = @EstablishmentID
ORDER BY Date

GO

/****** Object:  StoredProcedure [dbo].[GetStateDetails]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetStateDetails]
(@StateID int)
AS
SELECT CountryID, StateName, StateAbbreviation, Active
FROM State
WHERE StateID = @StateID
ORDER BY StateName ASC

GO

/****** Object:  StoredProcedure [dbo].[GetStatesInCountry]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetStatesInCountry]
(@CountryID int, @Active bit)
AS
IF @Active = 'TRUE'
BEGIN
	SELECT StateID, StateName, StateAbbreviation, Active
	FROM State
	WHERE CountryID = @CountryID AND Active = 'TRUE'
	ORDER BY StateName ASC
END
ELSE
BEGIN
	SELECT StateID, StateName, StateAbbreviation, Active
	FROM State
	WHERE CountryID = @CountryID
	ORDER BY StateName ASC
END

GO

/****** Object:  StoredProcedure [dbo].[GetTodaysScheduleForCity]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTodaysScheduleForCity]
(@CityID int,
@PageNumber int,
@EstablishmentsPerPage int,
@HowManyEstablishments int OUTPUT)
AS

-- declare a new TABLE variable
DECLARE @Establishments TABLE
(RowNumber int, EstablishmentID int, EstablishmentName nvarchar(50),
Priority int, Logo nvarchar(50), Motto nvarchar(50), Weekday int,
HoursOfOperation nvarchar(50), HappyHourTimes nvarchar(50), HappyHourSpecials nvarchar(300),
FoodAndDrinkSpecials nvarchar(300), SpecialEvents nvarchar(300))

-- populate the table variable with the complete list of products
INSERT INTO @Establishments
SELECT ROW_NUMBER() OVER
(ORDER BY Establishment.Priority, (REPLACE(REPLACE(REPLACE(Establishment.EstablishmentName, 'the ', ''), 'an ', ''), 'a ', ''))),
RoutineEstablishmentEvents.EstablishmentID, Establishment.EstablishmentName, Establishment.Priority,
Establishment.Logo, Establishment.Motto, RoutineEstablishmentEvents.Weekday, RoutineEstablishmentEvents.HoursOfOperation,
RoutineEstablishmentEvents.HappyHourTimes, RoutineEstablishmentEvents.HappyHourSpecials,
RoutineEstablishmentEvents.FoodAndDrinkSpecials, RoutineEstablishmentEvents.SpecialEvents
FROM RoutineEstablishmentEvents
INNER JOIN EstablishmentCity ON RoutineEstablishmentEvents.EstablishmentID = EstablishmentCity.EstablishmentID
INNER JOIN Establishment ON RoutineEstablishmentEvents.EstablishmentID = Establishment.EstablishmentID
WHERE EstablishmentCity.CityID = @CityID
	AND RoutineEstablishmentEvents.Weekday = DATEPART(Weekday, GetDate())
-- return the total number of products using an OUTPUT variable
SELECT @HowManyEstablishments = COUNT(EstablishmentID) FROM @Establishments
-- extract the requested page of Establishments
SELECT EstablishmentID, EstablishmentName, Priority, Logo, Motto,
Weekday, HoursOfOperation, HappyHourTimes, HappyHourSpecials,
FoodAndDrinkSpecials, SpecialEvents
FROM @Establishments
WHERE RowNumber > (@PageNumber - 1) * @EstablishmentsPerPage
	AND RowNumber <= @PageNumber * @EstablishmentsPerPage

GO

/****** Object:  StoredProcedure [dbo].[GetTodaysSpecialEvents]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTodaysSpecialEvents]
AS
SELECT EstablishmentSpecialEventID, EstablishmentID, EstablishmentName,
Date, TodaysHoursOfOperation,
TodaysHappyHourTimes, TodaysHappyHourSpecials, THSAppendOrReplaceFlag,
TodaysFoodAndDrinkSpecials, TFSAppendOrReplaceFlag,
TodaysSpecialEvents, TSEAppendOrReplaceFlag
FROM SpecialEstablishmentEvents
WHERE CONVERT(nvarchar(30), SpecialEstablishmentEvents.Date, 101) = CONVERT(nvarchar(30), GetDate(), 101)
ORDER BY EstablishmentID

GO

/****** Object:  StoredProcedure [dbo].[MoveAdvertiserToCity]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MoveAdvertiserToCity]
(@AdvertiserID int, @OldCityID int, @NewCityID int)
AS
UPDATE AdvertiserCity
SET CityID = @NewCityID
WHERE CityID = @OldCityID
AND AdvertiserID = @AdvertiserID

GO

/****** Object:  StoredProcedure [dbo].[MoveEstablishmentToCity]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MoveEstablishmentToCity]
(@EstablishmentID int, @OldCityID int, @NewCityID int)
AS
UPDATE EstablishmentCity
SET CityID = @NewCityID
WHERE CityID = @OldCityID
AND EstablishmentID = @EstablishmentID

GO

/****** Object:  StoredProcedure [dbo].[MoveEventToCity]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MoveEventToCity]
(@EventID int, @OldCityID int, @NewCityID int)
AS
UPDATE EventCity
SET CityID = @NewCityID
WHERE CityID = @OldCityID
AND EventID = @EventID

GO

/****** Object:  StoredProcedure [dbo].[MoveLinkToCity]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MoveLinkToCity]
(@LinkID int, @OldCityID int, @NewCityID int)
AS
UPDATE LinkCity
SET CityID = @NewCityID
WHERE CityID = @OldCityID
AND LinkID = @LinkID

GO

/****** Object:  StoredProcedure [dbo].[RemoveAdvertiserFromCity]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RemoveAdvertiserFromCity]
(@AdvertiserID int, @CityID int)
AS
DELETE FROM AdvertiserCity
WHERE CityID = @CityID AND AdvertiserID = @AdvertiserID

GO

/****** Object:  StoredProcedure [dbo].[RemoveEstablishmentFromCity]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RemoveEstablishmentFromCity]
(@EstablishmentID int, @CityID int)
AS
DELETE FROM EstablishmentCity
WHERE CityID = @CityID AND EstablishmentID = @EstablishmentID

GO

/****** Object:  StoredProcedure [dbo].[RemoveEventFromCity]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RemoveEventFromCity]
(@EventID int, @CityID int)
AS
DELETE FROM EventCity
WHERE CityID = @CityID AND EventID = @EventID

GO

/****** Object:  StoredProcedure [dbo].[RemoveLinkFromCity]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RemoveLinkFromCity]
(@LinkID int, @CityID int)
AS
DELETE FROM LinkCity
WHERE CityID = @CityID AND LinkID = @LinkID

GO

/****** Object:  StoredProcedure [dbo].[UpdateAdvertiser]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateAdvertiser]
(@AdvertiserID int, @BlockID int, @ImageUrl nvarchar(255), @NavigateUrl nvarchar(255), @AlternateText nvarchar(100), @Keyword nvarchar(50), @Impressions int)
AS
UPDATE Advertiser
SET BlockID = @BlockID, ImageUrl = @ImageUrl, NavigateUrl = @NavigateUrl, AlternateText = @AlternateText, Keyword = @Keyword, Impressions = @Impressions
WHERE AdvertiserID = @AdvertiserID

GO

/****** Object:  StoredProcedure [dbo].[UpdateAdvertiserUserName]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateAdvertiserUserName]
(@AdvertiserID int, @UserName nvarchar(40))
AS
UPDATE Advertiser
SET
UserName = @UserName
WHERE AdvertiserID = @AdvertiserID

GO

/****** Object:  StoredProcedure [dbo].[UpdateCity]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateCity]
(@CityID int, @CityName nvarchar(50), @ZipCode char(6), @TimeZoneID int, @Active bit)
AS
UPDATE City
SET CityName = @CityName, ZipCode = @ZipCode, TimeZoneID = @TimeZoneID, Active = @Active
WHERE CityID = @CityID

GO

/****** Object:  StoredProcedure [dbo].[UpdateCountry]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateCountry]
(@CountryID int, @CountryName nvarchar(50), @CountryAbbreviation char(2), @Active bit)
AS
UPDATE Country
SET CountryName = @CountryName, CountryAbbreviation = @CountryAbbreviation, Active = @Active
WHERE CountryID = @CountryID

GO

/****** Object:  StoredProcedure [dbo].[UpdateEstablishment]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateEstablishment]
(@EstablishmentID int, @EstablishmentName nvarchar(50), @Priority int, @Tier int, @Logo nvarchar(50), @Motto nvarchar(50), @Cuisine nvarchar(50), @StreetAddress nvarchar(50), @CityName nvarchar(50), @StateAbbreviation char(2), @ZipCode char(6), @Latitude float, @Longitude float, @TelephoneNo char(14), @FaxNo char(14), @Email nvarchar(100), @WebSite nvarchar(100), @Facebook nvarchar(100), @AboutUs nvarchar(MAX), @Photo01 nvarchar(50), @Photo02 nvarchar(50), @Photo03 nvarchar(50), @Photo04 nvarchar(50), @Photo05 nvarchar(50))
AS
UPDATE Establishment
SET 
EstablishmentName = @EstablishmentName, Priority = @Priority, Tier = @Tier, Logo = @Logo, Motto = @Motto, Cuisine = @Cuisine, StreetAddress = @StreetAddress, CityName = @CityName, StateAbbreviation = @StateAbbreviation, ZipCode = @ZipCode, Latitude = @Latitude, Longitude = @Longitude, TelephoneNo = @TelephoneNo, FaxNo = @FaxNo, Email = @Email, WebSite = @WebSite, Facebook = @Facebook, AboutUs = @AboutUs, Photo01 = @Photo01, Photo02 = @Photo02, Photo03 = @Photo03, Photo04 = @Photo04, Photo05 = @Photo05
WHERE EstablishmentID = @EstablishmentID

GO

/****** Object:  StoredProcedure [dbo].[UpdateEstablishmentUserName]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateEstablishmentUserName]
(@EstablishmentID int, @UserName nvarchar(40))
AS
UPDATE Establishment
SET
UserName = @UserName
WHERE EstablishmentID = @EstablishmentID

GO

/****** Object:  StoredProcedure [dbo].[UpdateEvent]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateEvent]
(@EventID int,
@EventName nvarchar(50),
@Date smalldatetime,
@EndDate smalldatetime,
@Description nvarchar(300),
@ImageUrl nvarchar(50),
@NavigateUrl nvarchar(100))
AS
UPDATE Event
SET EventName = @EventName, Date = @Date, EndDate = @EndDate, Description = @Description,
ImageUrl = @ImageUrl, NavigateUrl = @NavigateUrl
WHERE EventID = @EventID

GO

/****** Object:  StoredProcedure [dbo].[UpdateLink]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateLink]
(@LinkID int,
@LinkName nvarchar(50),
@LinkUrl nvarchar(100),
@LinkTitle nvarchar(100),
@LinkCategoryName nvarchar(50),
@LinkCategoryRank int)
AS
UPDATE Link
SET LinkName = @LinkName, LinkUrl = @LinkUrl, LinkTitle = @LinkTitle,
LinkCategoryName = @LinkCategoryName, LinkCategoryRank = @LinkCategoryRank
WHERE LinkID = @LinkID

GO

/****** Object:  StoredProcedure [dbo].[UpdateRoutineEstablishmentSchedule]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateRoutineEstablishmentSchedule]
(@EstablishmentRoutineEventID int,
@Weekday int,
@HoursOfOperation nvarchar(50),
@HappyHourTimes nvarchar(50),
@HappyHourSpecials nvarchar(300),
@FoodAndDrinkSpecials nvarchar(300),
@SpecialEvents nvarchar(300))
AS
UPDATE RoutineEstablishmentEvents
SET Weekday = @Weekday, HoursOfOperation = @HoursOfOperation, HappyHourTimes = @HappyHourTimes,
HappyHourSpecials = @HappyHourSpecials, FoodAndDrinkSpecials = @FoodAndDrinkSpecials, SpecialEvents = @SpecialEvents
WHERE EstablishmentRoutineEventID = @EstablishmentRoutineEventID

GO

/****** Object:  StoredProcedure [dbo].[UpdateSpecialEstablishmentSchedule]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateSpecialEstablishmentSchedule]
(@EstablishmentSpecialEventID int,
@Description nvarchar(50),
@TodaysHoursOfOperation nvarchar(50),
@TodaysHappyHourTimes nvarchar(50),
@TodaysHappyHourSpecials nvarchar(300),
@THSAppendOrReplaceFlag int,
@TodaysFoodAndDrinkSpecials nvarchar(300),
@TFSAppendOrReplaceFlag int,
@TodaysSpecialEvents nvarchar(300),
@TSEAppendOrReplaceFlag int)
AS
UPDATE SpecialEstablishmentEvents
SET Description = @Description, TodaysHoursOfOperation = @TodaysHoursOfOperation, TodaysHappyHourTimes = @TodaysHappyHourTimes, TodaysHappyHourSpecials = @TodaysHappyHourSpecials, THSAppendOrReplaceFlag = @THSAppendOrReplaceFlag, TodaysFoodAndDrinkSpecials = @TodaysFoodAndDrinkSpecials, TFSAppendOrReplaceFlag = @TFSAppendOrReplaceFlag, TodaysSpecialEvents = @TodaysSpecialEvents, TSEAppendOrReplaceFlag = @TSEAppendOrReplaceFlag
WHERE EstablishmentSpecialEventID = @EstablishmentSpecialEventID

GO

/****** Object:  StoredProcedure [dbo].[UpdateState]    Script Date: 01/11/2015 23:27:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateState]
(@StateID int, @StateName nvarchar(50), @StateAbbreviation char(2), @Active bit)
AS
UPDATE State
SET StateName = @StateName, StateAbbreviation = @StateAbbreviation, Active = @Active
WHERE StateID = @StateID

GO

