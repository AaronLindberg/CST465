CREATE PROCEDURE [aaronlindberg].Property_StringAttribute_Select
(
	@PropertyId bigint,
	@EventId bigint
)
AS
BEGIN
	SELECT PropertyStringAttribute.Id, AttributeName, PropertyStringAttributeValue.Value  FROM PropertyStringAttribute 
	JOIN PropertyStringAttributeValue
	ON PropertyStringAttributeValue.PropertyStringAttributeId = PropertyStringAttribute.id
	WHERE @PropertyId = PropertyId AND PropertyStringAttributeValue.[Event] = @EventId
    UNION
    SELECT PropertyStringAttribute.Id, AttributeName, PropertyStringAttributeValue.Value FROM PropertyStringAttribute 
	JOIN PropertyStringAttributeValue
	ON PropertyStringAttributeValue.PropertyStringAttributeId = PropertyStringAttribute.id
	WHERE @PropertyId = PropertyId AND PropertyStringAttributeValue.[Event] = @EventId 
		AND PropertyStringAttribute.Id 
			NOT IN (SELECT PropertyStringAttribute.Id FROM PropertyStringAttribute 
				WHERE @PropertyId = PropertyId AND PropertyStringAttributeValue.[Event] = NULL );
END