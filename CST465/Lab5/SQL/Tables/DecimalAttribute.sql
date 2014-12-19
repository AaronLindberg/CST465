CREATE TABLE [dbo].[DecimalAttribute] (
    [DecimalId]            INT          IDENTITY (1, 1) PRIMARY KEY,
    [EventMemoryFk]        INT          NOT NULL,
    [DecimalAttributeName] VARCHAR (50) NOT NULL,
    [DecimalValue]         FLOAT (53)   NOT NULL,
    FOREIGN KEY ([EventMemoryFk]) REFERENCES [dbo].[EventMemory] ([EventMemoryId])
);

