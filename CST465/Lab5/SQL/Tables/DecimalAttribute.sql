CREATE TABLE [dbo].[DecimalAttribute] (
    [DecimalId]            BIGINT       IDENTITY (1, 1) NOT NULL,
    [EventMemoryFk]        BIGINT       NOT NULL,
    [DecimalAttributeName] VARCHAR (50) NOT NULL,
    [DecimalValue]         FLOAT (53)   NOT NULL,
    PRIMARY KEY CLUSTERED ([DecimalId] ASC),
    FOREIGN KEY ([EventMemoryFk]) REFERENCES [dbo].[EventMemory] ([EventMemoryId])
);

