CREATE PROCEDURE [aaronlindberg].Property_Exsits
(
	@Name varchar(64),
	@Creator UNIQUEIDENTIFIER
)
AS
DECLARE @RecordExists int;

SELECT @RecordExists = COUNT(Id) 
FROM Property
WHERE Name = @Name AND Creator = @Creator;

RETURN @RecordExists;