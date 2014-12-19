CREATE PROCEDURE [aaronlindberg].DecimalAttribute_InsertUpdate
(
	@AttributeId INT,
	@EventFk INT,
	@AttributeName VARCHAR(50),
	@AttributeValue FLOAT (53)
)
AS
DECLARE @RecordExists INT;
SELECT @RecordExists = COUNT(DecimalId) 
FROM DecimalAttribute
WHERE DecimalId=@AttributeId;

IF @RecordExists = 0
BEGIN
INSERT INTO DecimalAttribute( EventMemoryFk, DecimalAttributeName, DecimalValue)
VALUES ( @EventFk, @AttributeName, convert(float,@AttributeValue) );
END
ELSE
BEGIN
UPDATE DecimalAttribute
SET EventMemoryFk=@EventFk,
DecimalAttributeName=@AttributeName,
DecimalValue = convert(float,@AttributeValue)
WHERE DecimalId=@AttributeId;
END