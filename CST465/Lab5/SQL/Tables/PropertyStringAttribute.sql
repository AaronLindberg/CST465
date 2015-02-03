CREATE TABLE [dbo].[PropertyStringAttribute] (
    [Id]            BIGINT       IDENTITY (1, 1) NOT NULL,
    [PropertyId]    BIGINT       NOT NULL,
    [AttributeName] VARCHAR (64) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PropertyStringAttribute_ToPropertyTable] FOREIGN KEY ([PropertyId]) REFERENCES [dbo].[Property] ([Id])
);

