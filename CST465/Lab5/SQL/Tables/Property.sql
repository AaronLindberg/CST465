CREATE TABLE [dbo].[Property] (
    [Id]      INT              IDENTITY (1, 1) NOT NULL,
    [Name]    VARCHAR (64)     NOT NULL,
    [Creator] UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Creator_ToPropertyTable] FOREIGN KEY ([Creator]) REFERENCES [dbo].[aspnet_Users] ([UserId])
);

