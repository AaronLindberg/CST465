CREATE TABLE [dbo].[Property]
(
	[Id]		INT					NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name]		VARCHAR(64)			NOT NULL,
	[Creator]	UNIQUEIDENTIFIER    NULL,
	CONSTRAINT [FK_Creator_ToPropertyTable] FOREIGN KEY ([Creator]) REFERENCES [aspnet_Users]([UserId])				
)
