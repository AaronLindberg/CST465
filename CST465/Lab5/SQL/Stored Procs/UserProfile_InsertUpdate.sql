CREATE PROCEDURE [aaronlindberg].UserProfile_InsertUpdate
(
	@UserId [uniqueidentifier],
	@FirstName varchar(50),
	@LastName varchar(50)
)
AS
DECLARE @RecordExists int;
SELECT @RecordExists = COUNT(UserId) 
FROM UserProfile
WHERE UserId=@UserId;

IF @RecordExists = 0
BEGIN
INSERT INTO UserProfile(UserId, FirstName, LastName)
VALUES (@UserId, @FirstName, @LastName);
END
ELSE
BEGIN
UPDATE UserProfile
SET FirstName=@FirstName,
LastName=@LastName
WHERE UserId=@UserId;
END