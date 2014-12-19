CREATE PROCEDURE [aaronlindberg].EventMemory_InsertUpdate
(
	@EventId int,
	@UserId uniqueidentifier,
	@EventName varchar(50),
	@EventDescription varchar(2048),
	@DateScheduled date
)
AS
DECLARE @RecordExists int;
SELECT @RecordExists = COUNT(EventMemoryId) 
FROM EventMemory
WHERE EventMemoryId=@EventId;

IF @RecordExists = 0
BEGIN
INSERT INTO EventMemory(UserFk, EventName, EventDescription, Scheduled)
VALUES ( @UserId, @EventName, @EventDescription, @DateScheduled);
END
ELSE
BEGIN
UPDATE EventMemory
SET UserFk=@UserId,
EventName=@EventName,
EventDescription = @EventDescription,
Scheduled = @DateScheduled
WHERE EventMemoryId=@EventId;
END