CREATE PROCEDURE [aaronlindberg].PropertyInstance_Create
(
	@EventId bigint,
	@PropertyId bigint
)
AS
BEGIN
    DECLARE @RET_ID bigint;
	INSERT INTO PropertyInstance( FK_Event, FK_Property)
	VALUES ( @EventId, @PropertyId);

	SELECT @RET_ID = SCOPE_IDENTITY();
END