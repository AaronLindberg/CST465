CREATE TABLE [dbo].[IntegerAttribute] (
    [IntegerId]            BIGINT       IDENTITY (1, 1) NOT NULL,
    [EventMemoryFk]        BIGINT       NOT NULL,
    [IntegerAttributeName] VARCHAR (50) NOT NULL,
    [IntegerValue]         BIGINT       NOT NULL,
    PRIMARY KEY CLUSTERED ([IntegerId] ASC),
    FOREIGN KEY ([EventMemoryFk]) REFERENCES [dbo].[EventMemory] ([EventMemoryId])
);

