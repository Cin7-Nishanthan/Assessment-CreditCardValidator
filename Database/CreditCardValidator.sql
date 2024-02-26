

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
END
GO
INSERT INTO Cards(Id, Name)
VALUES (1, 'Visa'), (2, 'Mastercard'), (3, 'AmEx'), (4, 'Discover')
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
END
GO

ALTER TABLE CardValidations
		ADD CONSTRAINT FK_CardId_CardValidations_Cards FOREIGN KEY (CardId) REFERENCES Cards(Id)
GO

INSERT
INTO	CardValidations(Id, StartingNumber, Length, CardId)
VALUES	(1, '4', 16, 1)
		, (2, '22', 16, 2), (3, '51', 16, 2), (4, '52', 16, 2), (5, '53', 16, 2), (6, '54', 16, 2), (7, '55', 16, 2)
		, (8, '34', 15, 3), (9, '37', 15, 3)
		, (10, '6011', 16, 4)
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
USE CreditCardValidator
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