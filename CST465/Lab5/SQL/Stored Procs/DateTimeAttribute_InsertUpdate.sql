CREATE PROCEDURE [aaronlindberg].DateTimeAttribute_InsertUpdate
(
	@AttributeId bigint,
	@EventFk bigint,
	@AttributeName varchar(64),
	@AttributeValue datetime
)
AS
DECLARE @RecordExists int;
SELECT @RecordExists = COUNT(DateTimeId) 
FROM DateTimeAttribute
WHERE DateTimeId=@AttributeId;

IF @RecordExists = 0
BEGIN
INSERT INTO DateTimeAttribute( EventMemoryFk, DateTimeAttributeName, DateTimeValue)
VALUES ( @EventFk, @AttributeName, @AttributeValue);
END
ELSE
BEGIN
UPDATE DateTimeAttribute
SET EventMemoryFk=@EventFk,
DateTimeAttributeName=@AttributeName,
DateTimeValue = @AttributeValue
WHERE DateTimeId=@AttributeId;
END