CREATE PROCEDURE [aaronlindberg].Property_SelectByCreator
(
	@Creator UNIQUEIDENTIFIER
)
AS

SELECT Id, Name 
FROM [Property] 
WHERE Creator = @Creator OR Creator = NULL;
