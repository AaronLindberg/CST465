CREATE TABLE [dbo].[PropertyStringAttribute]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[PropertyId] INT NOT NULL, 
	[AttributeName] VARCHAR(64) NOT NULL,
    CONSTRAINT [FK_PropertyStringAttribute_ToPropertyTable] FOREIGN KEY ([PropertyId]) REFERENCES [Property]([Id])
)
