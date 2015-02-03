CREATE PROCEDURE [aaronlindberg].PropertyStringAttribute_InsertUpdate
(
	@Id bigint,
	@PropertyId bigint,
	@AttributeName varchar(64), 
	@DefaultValue varchar (2048),
	@EventId bigint
)
AS
DECLARE @RecordExists int;

SELECT @RecordExists = COUNT(Id) 
FROM PropertyStringAttribute
WHERE Id=@Id;

IF @RecordExists = 0
BEGIN
	INSERT INTO PropertyStringAttribute(PropertyId, AttributeName)
	VALUES (@PropertyId, @AttributeName);
	SELECT @Id = SCOPE_IDENTITY();
END
ELSE
BEGIN
	UPDATE PropertyStringAttribute
	SET PropertyId = @PropertyId,
	AttributeName = @DefaultValue
	WHERE Id = @Id;
END

--InsertUpdate Default Values
SELECT @RecordExists = COUNT(Id) 
FROM PropertyStringAttributeValue
WHERE PropertyStringAttributeId = @Id AND [Event] = @EventId;

IF @RecordExists = 0
BEGIN
	INSERT INTO PropertyStringAttributeValue( PropertyStringAttributeId, [Event], Value)
	VALUES ( @PropertyId, @EventId, @DefaultValue);
END
ELSE
BEGIN
	UPDATE PropertyStringAttributeValue
	SET Value = @DefaultValue
	WHERE PropertyStringAttributeId = @Id AND [Event] = @EventId;
END