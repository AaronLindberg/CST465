CREATE TABLE [dbo].[DateTimeAttribute] (
    [DateTimeId]            BIGINT       IDENTITY (1, 1) NOT NULL,
    [EventMemoryFk]         BIGINT       NOT NULL,
    [DateTimeAttributeName] VARCHAR (64) NOT NULL,
    [DateTimeValue]         DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([DateTimeId] ASC),
    FOREIGN KEY ([EventMemoryFk]) REFERENCES [dbo].[EventMemory] ([EventMemoryId])
);

