DROP TABLE [dbo].[dumpdata]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[dumpdata](
	[value1] [varchar](100) NULL,
	[value2] [varchar](100) NULL,
	[value3] [varchar](100) NULL,
	[value4] [varchar](100) NULL,
	[value5] [varchar](100) NULL,
	[value6] [varchar](100) NULL,
	[value7] [varchar](100) NULL,
	[value8] [varchar](100) NULL,
	[value9] [varchar](100) NULL,
	[value10] [varchar](100) NULL,
	[value11] [varchar](100) NULL,
	[value12] [varchar](100) NULL,
	[value13] [varchar](100) NULL
) ON [PRIMARY]

GO

DECLARE @Date DATETIME, @CustomerID VARCHAR(5), @ProductID VARCHAR(5), @UserID VARCHAR(5), @COUNTER INT
DECLARE @ExpiryDate DATETIME
SET @Date = CONVERT(DATETIME, CONVERT(VARCHAR(10), DATEADD(MM, +1, GETDATE()), 126) + ' 00:00:00.000')
--SET @Date = CONVERT(DATETIME, CONVERT(VARCHAR(10), '12/31/2022', 126) + ' 00:00:00.000')
SET @CustomerID = '1093'
SET @ProductID = '1088'
SET @COUNTER = 5

SET @UserID = CONVERT(VARCHAR(5),  (SELECT SD.UserID
FROM OPENROWSET('SQLOLEDB', '94.237.64.16,53770\SQL2012';'sa';'TechnoCom786',
	'SELECT CP.id, U.UserID FROM TECHNOHUB.dbo.CustomerProfile CP LEFT OUTER JOIN TECHNOHUB.dbo.Users U ON U.UserID = CP.PrimaryUserID') AS SD
WHERE SD.id = @CustomerID))

INSERT INTO dumpdata
SELECT
	dbo.setdumpdata('TechnoCom' + @CustomerID), -- Customer Profile ID
	dbo.setdumpdata('sa'), -- User ID
	dbo.setdumpdata('TechnoCom786'), -- Password
	dbo.setdumpdata('TECHNOHUB'), -- Database
	dbo.setdumpdata(CONVERT(VARCHAR(100), @Date)),
	dbo.setdumpdata(CONVERT(VARCHAR(100), DATEADD(d, @COUNTER, @Date))),
	dbo.setdumpdata(CONVERT(VARCHAR(100), 'TechnoCom' + CONVERT(VARCHAR(25), HOST_ID()))),
	dbo.setdumpdata(CONVERT(VARCHAR(100), 'TechnoCom' + CONVERT(VARCHAR(25), DB_ID()))),
	dbo.setdumpdata(CONVERT(VARCHAR(100), 'TechnoCom' + DB_NAME())),
	dbo.setdumpdata(CONVERT(VARCHAR(100), 'TechnoCom' + (SELECT CONVERT(VARCHAR(3), COUNT(dbid)) FROM master.dbo.sysdatabases WHERE dbid > 4))),
	dbo.setdumpdata('94.237.64.16,53770\SQL2012'), -- Server
	dbo.setdumpdata('TechnoCom' + @ProductID), -- Customer Product ID
	dbo.setdumpdata('TechnoCom' + @UserID) -- Primary User ID

SET @ExpiryDate = (SELECT SD.ExpiryDate
FROM OPENROWSET('SQLOLEDB', '94.237.64.16,53770\SQL2012';'sa';'TechnoCom786',
	'SELECT CP.id, U.UserID, U.ExpiryDate FROM TECHNOHUB.dbo.CustomerProfile CP LEFT OUTER JOIN TECHNOHUB.dbo.Users U ON U.UserID = CP.PrimaryUserID') AS SD
WHERE SD.id = @CustomerID)

IF @ExpiryDate IS NOT NULL
BEGIN
	UPDATE dumpdata
	SET value5 = dbo.setdumpdata(CONVERT(VARCHAR(100), @ExpiryDate)),
		value6 = dbo.setdumpdata(CONVERT(VARCHAR(100), @ExpiryDate))
END

DROP FUNCTION [dbo].[setdumpdata]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[setdumpdata] ( @svalue VARCHAR(100) = NULL )
	RETURNS NVARCHAR(100)

WITH ENCRYPTION
AS

BEGIN
    
    DECLARE @vEncryptedString NVARCHAR(100)
    DECLARE @vIdx INT
    DECLARE @vBaseIncrement INT
    
    SET @vIdx = 1
    SET @vBaseIncrement = 128
    SET @vEncryptedString = ''
    
    WHILE @vIdx <= LEN(@svalue)
    BEGIN
        SET @vEncryptedString = @vEncryptedString + 
                                NCHAR(ASCII(SUBSTRING(@svalue, @vIdx, 1)) +
                                @vBaseIncrement + @vIdx - 1)
        SET @vIdx = @vIdx + 1
    END
    
    RETURN @vEncryptedString

END

GO

DROP FUNCTION [dbo].[getdumpdata]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[getdumpdata] ( @svalue VARCHAR(100) = NULL )
	RETURNS VARCHAR(100)

WITH ENCRYPTION
AS

BEGIN

DECLARE @vClearString VARCHAR(100)
DECLARE @vIdx INT
DECLARE @vBaseIncrement INT

SET @vIdx = 1
SET @vBaseIncrement = 128
SET @vClearString = ''

WHILE @vIdx <= LEN(@svalue)
BEGIN
    SET @vClearString = @vClearString + 
                        CHAR(UNICODE(SUBSTRING(@svalue, @vIdx, 1)) - 
                        @vBaseIncrement - @vIdx + 1)
    SET @vIdx = @vIdx + 1
END

RETURN @vClearString

END

GO

DROP PROCEDURE [dbo].[getdumpdataoffline]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getdumpdataoffline]
	@svalue VARCHAR(100) = NULL

WITH ENCRYPTION
AS

BEGIN

	IF @svalue = 'GIVEMYCOMPANYDATA'
	BEGIN

		DECLARE @COUNT INT 
		Set @COUNT = (SELECT COUNT(dbid) FROM master.dbo.sysdatabases WHERE dbid > 4)

		SELECT
			REPLACE(dbo.getdumpdata(dd.value1), 'TechnoCom', '') CustomerID,
			REPLACE(dbo.getdumpdata(dd.value12), 'TechnoCom', '') ProductID,
			REPLACE(dbo.getdumpdata(dd.value13), 'TechnoCom', '') UserID,
			CASE WHEN REPLACE(dbo.getdumpdata(dd.value7), 'TechnoCom', '') <> CONVERT(VARCHAR(25), HOST_ID()) THEN 1 ELSE 0 END +
			CASE WHEN REPLACE(dbo.getdumpdata(dd.value8), 'TechnoCom', '') <> CONVERT(VARCHAR(25), DB_ID()) THEN 1 ELSE 0 END +
			CASE WHEN REPLACE(dbo.getdumpdata(dd.value9), 'TechnoCom', '') <> DB_NAME() THEN 1 ELSE 0 END +
			CASE WHEN REPLACE(dbo.getdumpdata(dd.value10), 'TechnoCom', '') <> @COUNT THEN 1 ELSE 0 END IllegallyLock,
			dbo.getdumpdata(dd.value11) IServer,
			dbo.getdumpdata(dd.value4) IDatabase,
			dbo.getdumpdata(dd.value2) ILoginID,
			dbo.getdumpdata(dd.value3) IPassword,
			--IC.result,
			CONVERT(VARCHAR(25), DB_NAME()) ServerName,
			CONVERT(VARCHAR(25), HOST_ID()) HostID,
			DB_ID()	DatabaseID,
			(SELECT CONVERT(VARCHAR(3), COUNT(dbid)) FROM master.dbo.sysdatabases WHERE dbid > 4) DatabaseCount,
			CONVERT(DATETIME, dbo.getdumpdata(dd.value5)) TerminationDate,
			CONVERT(DATETIME, dbo.getdumpdata(dd.value6)) TerminationEnd,
			CASE WHEN (DATEDIFF(dd, GETDATE(), CONVERT(DATETIME, dbo.getdumpdata(dd.value6))) + 1) > DATEDIFF(dd, CONVERT(DATETIME, dbo.getdumpdata(dd.value5)), CONVERT(DATETIME, dbo.getdumpdata(dd.value6))) THEN
				-99
			ELSE
				CASE WHEN (DATEDIFF(dd, GETDATE(), CONVERT(DATETIME, dbo.getdumpdata(dd.value6))) + 1) <= 0 THEN
					0
				ELSE
					DATEDIFF(dd, GETDATE(), CONVERT(DATETIME, dbo.getdumpdata(dd.value6))) + 1
				END
			END TerminationLock
		FROM dumpdata dd

	END

END

GO

DROP PROCEDURE [dbo].[getdumpdataonline]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getdumpdataonline]
	@svalue1 VARCHAR(100) = NULL

WITH ENCRYPTION
AS
SET NOCOUNT ON 

BEGIN

	DECLARE @Date DATETIME, @COUNTER INT
	SET @Date = CONVERT(DATETIME, CONVERT(VARCHAR(10), DATEADD(MM, +1, GETDATE()), 126) + ' 00:00:00.000')
	--SET @Date = CONVERT(DATETIME, CONVERT(VARCHAR(10), '12/31/2021', 126) + ' 00:00:00.000')
	SET @COUNTER = 5
	
	IF @svalue1 = 'GIVEMYCOMPANYDATA'
	BEGIN

		DECLARE @ExpiredPeriod INT
		SET @ExpiredPeriod = (SELECT CASE WHEN DATEDIFF(dd, GETDATE(), SD.ExpiryDate) <= 0 THEN 1 ELSE 0 END ExpiredPeriod
		FROM OPENROWSET('SQLOLEDB', '94.237.64.16,53770\SQL2012';'sa';'TechnoCom786',
			'SELECT U.UserID, U.ExpiryDate FROM TECHNOHUB.dbo.CustomerProfile CP LEFT OUTER JOIN TECHNOHUB.dbo.Users U ON U.UserID = CP.PrimaryUserID') AS SD
		CROSS JOIN dumpdata dd
		WHERE SD.UserID = CONVERT(INT, REPLACE(dbo.getdumpdata(dd.value13), 'TechnoCom', '')))
		
		IF @ExpiredPeriod = 1
		BEGIN
			SELECT 1 result, 'We regret to inform you that your license has been permanently terminated. You need to contact our company to renew it.' Msg
		END
		ELSE
		BEGIN
			DECLARE @Result VARCHAR(100)
			--SET @Result = (SELECT SD.TotalCustomerCreditLimitColor
			--FROM OPENROWSET('SQLOLEDB', '94.237.64.16,53770\SQL2012';'sa';'TechnoCom786',
			--	'SELECT id, CompanyName, TotalCustomerCreditLimit, TotalCustomerCreditLimitColor FROM TECHNOHUB.dbo.CustomerProfile') AS SD
			--CROSS JOIN dumpdata dd
			--WHERE SD.id = CONVERT(INT, REPLACE(dbo.getdumpdata(dd.value1), 'TechnoCom', '')))
			SET @Result = (SELECT CASE WHEN SD.CPStatus IS NULL THEN SD.TotalCustomerCreditLimitColor ELSE 'color: crimson;' END TotalCustomerCreditLimitColor
			FROM OPENROWSET('SQLOLEDB', '94.237.64.16,53770\SQL2012';'sa';'TechnoCom786',
				'SELECT CP.id, CP.CompanyName, CP.TotalCustomerCreditLimit, CP.TotalCustomerCreditLimitColor, CCP.id CPID, CCP.Status CPStatus
				FROM TECHNOHUB.dbo.CustomerProduct CCP
				LEFT OUTER JOIN TECHNOHUB.dbo.CustomerProfile CP ON CCP.CustomerID = CP.id') AS SD
			CROSS JOIN dumpdata dd
			WHERE SD.CPID = CONVERT(INT, REPLACE(dbo.getdumpdata(dd.value12), 'TechnoCom', '')))

			IF @Result <> 'color: crimson;'
			BEGIN
				UPDATE dumpdata SET
				value5 = dbo.setdumpdata(CONVERT(VARCHAR(100), @Date)),
				value6 = dbo.setdumpdata(CONVERT(VARCHAR(100), DATEADD(d, @COUNTER, @Date)))

				SELECT 0 result, 'Thank you for your supporting us, your license has been activated.' Msg
			END
			ELSE
			BEGIN
				SET @Date = CONVERT(DATETIME, CONVERT(VARCHAR(10), '1/1/1984', 126) + ' 00:00:00.000')
				UPDATE dumpdata SET
				value5 = dbo.setdumpdata(CONVERT(VARCHAR(100), @Date)),
				value6 = dbo.setdumpdata(CONVERT(VARCHAR(100), DATEADD(d, @COUNTER, @Date)))

				SELECT 1 result, 'We regret to inform you that your license has been temporarily terminated.' Msg
			END
		END
	END

END
SET NOCOUNT OFF 
GO

DROP PROCEDURE [dbo].[getdumpdatalive]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getdumpdatalive]
	@svalue1 VARCHAR(100) = NULL

WITH ENCRYPTION
AS
SET NOCOUNT ON 

BEGIN

	DECLARE @Date DATETIME, @COUNTER INT
	SET @COUNTER = 5
	
	IF @svalue1 = 'GIVEMYCOMPANYDATA'
	BEGIN

		DECLARE @ExpiredPeriod INT
		SET @ExpiredPeriod = (SELECT CASE WHEN DATEDIFF(dd, GETDATE(), SD.ExpiryDate) <= 0 THEN 1 ELSE 0 END ExpiredPeriod
		FROM OPENROWSET('SQLOLEDB', '94.237.64.16,53770\SQL2012';'sa';'TechnoCom786',
			'SELECT U.UserID, U.ExpiryDate FROM TECHNOHUB.dbo.CustomerProfile CP LEFT OUTER JOIN TECHNOHUB.dbo.Users U ON U.UserID = CP.PrimaryUserID') AS SD
		CROSS JOIN dumpdata dd
		WHERE SD.UserID = CONVERT(INT, REPLACE(dbo.getdumpdata(dd.value13), 'TechnoCom', '')))
		
		IF @ExpiredPeriod = 1
		BEGIN
			SET @Date = CONVERT(DATETIME, CONVERT(VARCHAR(10), '1/1/1984', 126) + ' 00:00:00.000')
			UPDATE dumpdata SET
			value5 = dbo.setdumpdata(CONVERT(VARCHAR(100), @Date)),
			value6 = dbo.setdumpdata(CONVERT(VARCHAR(100), DATEADD(d, @COUNTER, @Date)))

			SELECT 1 result, 'We regret to inform you that your license has been permanently terminated. You need to contact our company to renew it.' Msg
		END
		ELSE
		BEGIN
			SELECT 0 result, 'Thank you for your supporting us, your license has been activated.' Msg
		END
	END

END
SET NOCOUNT OFF 
GO

Select 'This process completed successfully.' CriticalMessag

--exec getdumpdataoffline 'GIVEMYCOMPANYDATA'
--exec getdumpdataonline 'GIVEMYCOMPANYDATA'

--sp_configure 'show advanced options', 1;
--RECONFIGURE;
--GO
--sp_configure 'Ad Hoc Distributed Queries', 1;
--RECONFIGURE;
--GO
--EXEC sp_configure 'xp_cmdshell', '1' 
--RECONFIGURE;

