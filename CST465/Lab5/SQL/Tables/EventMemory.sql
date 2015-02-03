CREATE TABLE [dbo].[EventMemory] (
    [EventMemoryId]    BIGINT           IDENTITY (1, 1) NOT NULL,
    [UserFk]           UNIQUEIDENTIFIER NOT NULL,
    [EventName]        VARCHAR (50)     NOT NULL,
    [EventDescription] VARCHAR (2048)   NOT NULL,
    [Scheduled]        DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([EventMemoryId] ASC),
    FOREIGN KEY ([UserFk]) REFERENCES [dbo].[UserProfile] ([UserId])
);

