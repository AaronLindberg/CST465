CREATE PROCEDURE [aaronlindberg].IntegerAttribute_InsertUpdate
(
	@AttributeId int,
	@EventFk int,
	@AttributeName varchar(64),
	@AttributeValue int
)
AS
DECLARE @RecordExists int;
SELECT @RecordExists = COUNT(IntegerId) 
FROM IntegerAttribute
WHERE IntegerId=@AttributeId;

IF @RecordExists = 0
BEGIN
INSERT INTO IntegerAttribute(EventMemoryFk, IntegerAttributeName, IntegerValue)
VALUES ( @EventFk, @AttributeName, @AttributeValue);
END
ELSE
BEGIN
UPDATE IntegerAttribute
SET EventMemoryFk=@EventFk,
IntegerAttributeName=@AttributeName,
IntegerValue = @AttributeValue
WHERE IntegerId=@AttributeId;
END