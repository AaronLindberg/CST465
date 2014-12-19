CREATE TABLE [dbo].[EventComment] (
    [CommentId] INT              IDENTITY (1, 1) PRIMARY KEY,
    [UserFK]    UNIQUEIDENTIFIER NULL,
    [EventFK]   INT              NULL,
    [Comment]   VARCHAR (2048)   NULL,
    [TimeStamp] DATETIME         NULL,
    FOREIGN KEY ([EventFK]) REFERENCES [dbo].[EventMemory] ([EventMemoryId])
);

