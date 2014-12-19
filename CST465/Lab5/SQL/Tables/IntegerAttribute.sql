CREATE TABLE [dbo].[IntegerAttribute] (
    [IntegerId]            INT          IDENTITY (1, 1) PRIMARY KEY,
    [EventMemoryFk]        INT          NOT NULL,
    [IntegerAttributeName] VARCHAR (50) NOT NULL,
    [IntegerValue]         INT          NOT NULL,
    FOREIGN KEY ([EventMemoryFk]) REFERENCES [dbo].[EventMemory] ([EventMemoryId])
);

