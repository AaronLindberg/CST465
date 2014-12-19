CREATE TABLE [dbo].[EventMemory] (
    [EventMemoryId]    INT              IDENTITY (1, 1) PRIMARY KEY,
    [UserFk]           UNIQUEIDENTIFIER NOT NULL,
    [EventName]        VARCHAR (50)     NOT NULL,
    [EventDescription] VARCHAR (2048)   NOT NULL,
    [Scheduled]        DATETIME         NOT NULL,
    FOREIGN KEY ([UserFk]) REFERENCES [dbo].[UserProfile] ([UserId])
);

