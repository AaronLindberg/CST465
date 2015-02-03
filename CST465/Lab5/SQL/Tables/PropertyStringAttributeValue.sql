CREATE TABLE [dbo].[PropertyStringAttributeValue] (
    [Id]                        BIGINT         IDENTITY (1, 1) NOT NULL,
    [PropertyStringAttributeId] BIGINT         NOT NULL,
    [Event]                     BIGINT         NULL,
    [Value]                     VARCHAR (2048) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Event_ToEventMemoryTable] FOREIGN KEY ([Event]) REFERENCES [dbo].[EventMemory] ([EventMemoryId]),
    CONSTRAINT [FK_PropertyStringAttributeValue_ToPropertyStringAttributeTable] FOREIGN KEY ([PropertyStringAttributeId]) REFERENCES [dbo].[PropertyStringAttribute] ([Id])
);

