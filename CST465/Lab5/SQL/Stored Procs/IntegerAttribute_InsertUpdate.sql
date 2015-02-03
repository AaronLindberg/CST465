CREATE PROCEDURE [aaronlindberg].IntegerAttribute_InsertUpdate
(
	@AttributeId bigint,
	@EventFk bigint,
	@AttributeName varchar(64),
	@AttributeValue bigint
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