CREATE PROCEDURE [aaronlindberg].StringAttribute_InsertUpdate
(
	@AttributeId bigint,
	@EventFk bigint,
	@AttributeName varchar(64),
	@AttributeValue varchar(4096)
)
AS
DECLARE @RecordExists int;
SELECT @RecordExists = COUNT(StringId) 
FROM StringAttribute
WHERE StringId=@AttributeId;

IF @RecordExists = 0
BEGIN
INSERT INTO StringAttribute(StringId, EventMemoryFk, StringAttributeName, StringValue)
VALUES (@AttributeId, @EventFk, @AttributeName, @AttributeValue);
END
ELSE
BEGIN
UPDATE StringAttribute
SET EventMemoryFk=@EventFk,
StringAttributeName=@AttributeName,
StringValue = @AttributeValue
WHERE StringId=@AttributeId;
END