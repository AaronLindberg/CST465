CREATE TABLE [dbo].[StringAttribute] (
    [StringId]            INT            IDENTITY(1, 1) PRIMARY KEY,
    [EventMemoryFk]       INT            NOT NULL,
    [StringAttributeName] VARCHAR (50)   NOT NULL,
    [StringValue]         VARCHAR (2048) NOT NULL,
    FOREIGN KEY ([EventMemoryFk]) REFERENCES [dbo].[EventMemory] ([EventMemoryId])
);

