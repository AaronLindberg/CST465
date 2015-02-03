CREATE PROCEDURE [aaronlindberg].Property_InsertUpdate
(
	@Id int,
	@Name varchar(64),
	@Creator UNIQUEIDENTIFIER
)
AS
DECLARE @RecordExists int;
DECLARE @RET_ID int;
SELECT @RecordExists = COUNT(Id) 
FROM Property
WHERE Id = @Id;

IF @RecordExists = 0
BEGIN
INSERT INTO Property( Name, Creator)
VALUES ( @Name, @Creator);
SELECT TOP 1 @RET_ID = Id FROM [Property] WHERE Name = @Name AND Creator = @Creator;
END
ELSE
BEGIN
UPDATE Property
SET Name = @Name, @RET_ID = @Id
WHERE Id = @Id;
END
SELECT @RET_ID;