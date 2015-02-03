CREATE TABLE [dbo].[StringAttribute] (
    [StringId]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [EventMemoryFk]       BIGINT         NOT NULL,
    [StringAttributeName] VARCHAR (50)   NOT NULL,
    [StringValue]         VARCHAR (2048) NOT NULL,
    PRIMARY KEY CLUSTERED ([StringId] ASC),
    FOREIGN KEY ([EventMemoryFk]) REFERENCES [dbo].[EventMemory] ([EventMemoryId])
);

