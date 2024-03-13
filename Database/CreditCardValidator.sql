

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'CreditCardValidator')
BEGIN
	CREATE DATABASE CreditCardValidator
END
GO

USE CreditCardValidator
GO


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Cards' and xtype='U')
BEGIN
	CREATE TABLE Cards
	(
		Id INT NOT NULL,
		Name NVARCHAR(20) NOT NULL,
		Status TINYINT NOT NULL DEFAULT 1,
		IsDeleted BIT NOT NULL DEFAULT 0,
		CONSTRAINT PK_Cards PRIMARY KEY (Id)
	)

	INSERT INTO Cards(Id, Name)
	VALUES (1, 'Visa'), (2, 'Mastercard'), (3, 'AmEx'), (4, 'Discover')

END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CardValidations' and xtype='U')
BEGIN
	CREATE TABLE CardValidations
	(
		Id INT NOT NULL,
		StartingNumber NVARCHAR(5) NOT NULL,
		Length INT NOT NULL,
		CardId INT NOT NULL,
		Status TINYINT NOT NULL DEFAULT 1,
		IsDeleted BIT NOT NULL DEFAULT 0,
		CONSTRAINT PK_CardValidations PRIMARY KEY (Id)
	)
	
	
	ALTER TABLE CardValidations
		ADD CONSTRAINT FK_CardId_CardValidations_Cards FOREIGN KEY (CardId) REFERENCES Cards(Id)

	INSERT
	INTO	CardValidations(Id, StartingNumber, Length, CardId)
	VALUES	(1, '4', 16, 1)
			, (2, '22', 16, 2), (3, '51', 16, 2), (4, '52', 16, 2), (5, '53', 16, 2), (6, '54', 16, 2), (7, '55', 16, 2)
			, (8, '34', 15, 3), (9, '37', 15, 3)
			, (10, '6011', 16, 4)
END
GO


DROP PROCEDURE GetCardValidation
GO

CREATE PROCEDURE GetCardValidation
	@CardId INT = NULL
AS
BEGIN
	IF(NULLIF(@CardId, 0) IS NULL)
	BEGIN
		SELECT	Id, StartingNumber, Length, CardId
		FROM	CardValidations
		WHERE	Status = 1 AND IsDeleted = 0
	END
	ELSE
	BEGIN
		SELECT	Id, StartingNumber, Length, CardId
		FROM	CardValidations
		WHERE	CardId = @CardId AND Status = 1 AND IsDeleted = 0
	END
END

--EXEC	GetCardValidation 
--EXEC	GetCardValidation @CardId = 1
--EXEC	GetCardValidation @CardId = 2
--EXEC	GetCardValidation @CardId = 3
--EXEC	GetCardValidation @CardId = 4

--New Script to create new table for Logs
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ApplicationLogs' and xtype='U')
BEGIN
	CREATE TABLE ApplicationLogs
	(
		Id INT IDENTITY(1,1) PRIMARY KEY,
		TimeStamp DATETIME NOT NULL,
		Level NVARCHAR(50) NOT NULL,
		Message NVARCHAR(MAX),
		Exception NVARCHAR(MAX)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='LogSteps' and xtype='U')
BEGIN
	CREATE TABLE LogSteps
	(
		Id INT IDENTITY(1, 1),
		RequestId INT NOT NULL,
		Step INT NOT NULL,
		Data NVARCHAR(MAX),
		DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
		CONSTRAINT PK_LogSteps PRIMARY KEY (Id)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='LogRequests' and xtype='U')
BEGIN
	CREATE TABLE LogRequests
	(
		Id INT IDENTITY(1, 1),
		RequestId INT NOT NULL,
		Data NVARCHAR(MAX),
		DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
		CONSTRAINT PK_LogRequests PRIMARY KEY (Id)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='LogRequests' and xtype='U')
BEGIN
	CREATE TABLE LogRequests
	(
		Id INT IDENTITY(1, 1),
		RequestId INT NOT NULL,
		Data NVARCHAR(MAX),
		DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
		CONSTRAINT PK_LogRequests PRIMARY KEY (Id)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='LogExceptions' and xtype='U')
BEGIN
	CREATE TABLE LogExceptions
	(
		Id INT IDENTITY(1, 1),
		RequestId INT NOT NULL,
		Data NVARCHAR(MAX),
		DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
		CONSTRAINT PK_LogExceptions PRIMARY KEY (Id)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='LogResponses' and xtype='U')
BEGIN
	CREATE TABLE LogResponses
	(
		Id INT IDENTITY(1, 1),
		RequestId INT NOT NULL,
		Data NVARCHAR(MAX),
		DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
		CONSTRAINT PK_LogResponses PRIMARY KEY (Id)
	)
END
GO


IF NOT EXISTS (SELECT * FROM sys.sequences WHERE name = 'LogRequestIdSequence')
BEGIN
	CREATE SEQUENCE LogRequestIdSequence  
	AS INT
	START WITH 1 
	INCREMENT BY 1 
END
GO

/*
DECLARE	@requestId INT
SELECT @requestId = NEXT VALUE FOR LogRequestIdSequence  
SELECT @requestId
*/

IF EXISTS (SELECT * FROM sysobjects WHERE name='LogSave' and xtype='P')
BEGIN
	DROP PROCEDURE LogSave
END
GO

CREATE PROCEDURE LogSave
	@RequestId INT,
	@Step INT,
	@Data NVARCHAR(MAX),
	@RequestData NVARCHAR(MAX) = NULL,
	@ExceptionData NVARCHAR(MAX) = NULL,
	@ResponseData NVARCHAR(MAX) = NULL
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN transactionLogSave
			
			IF @RequestId = 0
				SELECT @requestId = NEXT VALUE FOR LogRequestIdSequence  

			INSERT
			INTO	LogSteps(RequestId, Step, Data)
			VALUES	(@RequestId, @Step, @Data)

			IF COALESCE(@RequestData, '') != ''
			BEGIN
				INSERT
				INTO	LogRequests(RequestId, Data)
				VALUES	(@RequestId, @RequestData)
			END

			IF COALESCE(@ExceptionData, '') != ''
			BEGIN
				INSERT
				INTO	LogExceptions(RequestId, Data)
				VALUES	(@RequestId, @ExceptionData)
			END

			IF COALESCE(@ResponseData, '') != ''
			BEGIN
				INSERT
				INTO	LogResponses(RequestId, Data)
				VALUES	(@RequestId, @ResponseData)
			END
		COMMIT TRAN transactionLogSave
		SELECT @RequestId
	END	TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN transactionLogSave
	END CATCH
END

/*

DELETE	FROM LogSteps
DELETE	FROM LogRequests
DELETE	FROM LogExceptions
DELETE	FROM LogResponses

DECLARE @requestId INT = 0
DECLARE @logResponse Table (RequestId INT)
INSERT	@logResponse (RequestId)
EXEC	LogSave @requestId = @requestId, @Step = 1,	@Data = 'Request Starts', @RequestData = 'Json', @ExceptionData = NULL, @ResponseData = NULL
SELECT	@RequestId = RequestId FROM @LogResponse

INSERT	@logResponse (RequestId)
EXEC	LogSave @requestId = @requestId, @Step = 2,	@Data = 'Request Proess', @RequestData = '', @ExceptionData = NULL, @ResponseData = NULL
SELECT	@RequestId = RequestId FROM @LogResponse

INSERT	@logResponse (RequestId)
EXEC	LogSave @requestId = @requestId, @Step = 3,	@Data = 'Request Proess', @RequestData = '', @ExceptionData = 'Exception', @ResponseData = NULL
SELECT	@RequestId = RequestId FROM @LogResponse

SELECT	@RequestId


SELECT	* FROM LogSteps
SELECT	* FROM LogRequests 
SELECT	* FROM LogExceptions
SELECT	* FROM LogResponses

*/





