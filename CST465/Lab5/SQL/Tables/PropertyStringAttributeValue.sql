CREATE TABLE [dbo].[PropertyStringAttributeValue] (
    [Id]                        INT            IDENTITY (1, 1) NOT NULL,
    [PropertyStringAttributeId] INT            NOT NULL,
    [Event]                     INT            NULL,
    [Value]                     VARCHAR (2048) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PropertyStringAttributeValue_ToPropertyStringAttributeTable] FOREIGN KEY ([PropertyStringAttributeId]) REFERENCES [dbo].[PropertyStringAttribute] ([Id]),
    CONSTRAINT [FK_Event_ToEventMemoryTable] FOREIGN KEY ([Event]) REFERENCES [dbo].[EventMemory] ([EventMemoryId])
);

