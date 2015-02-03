
CREATE PROCEDURE [aaronlindberg].PropertyStringAttribute_CreateUpdate
(
	@Id int,
	@PropertyId int,
	@AttributeName varchar(64), 
	@DefaultValue varchar (2048)
)
AS
DECLARE @RecordExists int;
DECLARE @EventId int;
SET @EventId=NULL;
SET @RecordExists = 0;
IF(@Id = -1)
BEGIN
SET @RecordExists = 0;
END
ELSE
BEGIN
	SELECT @RecordExists = COUNT(Id) 
	FROM PropertyStringAttribute
	WHERE Id=@Id;
END

IF @RecordExists = 0
BEGIN
	INSERT INTO PropertyStringAttribute(PropertyId, AttributeName)
	VALUES (@PropertyId, @AttributeName);
	SELECT @Id = SCOPE_IDENTITY();
END
ELSE
BEGIN
	UPDATE PropertyStringAttribute
	SET PropertyId=@PropertyId,
	AttributeName = @AttributeName
	WHERE Id=@Id;
END

--CreateUpdate Default Values
SELECT @RecordExists = COUNT(Id) 
FROM PropertyStringAttributeValue
WHERE PropertyStringAttributeId = @Id AND [Event] = @EventId;

IF @RecordExists = 0
BEGIN
	INSERT INTO PropertyStringAttributeValue( PropertyStringAttributeId, [Event], Value)
	VALUES ( @Id, @EventId, @DefaultValue);
END
ELSE
BEGIN
	UPDATE PropertyStringAttributeValue
	SET Value = @DefaultValue
	WHERE PropertyStringAttributeId = @Id AND [Event] = @EventId;
END