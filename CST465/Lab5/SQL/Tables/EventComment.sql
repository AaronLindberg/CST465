CREATE TABLE [dbo].[EventComment] (
    [CommentId] BIGINT           IDENTITY (1, 1) NOT NULL,
    [UserFK]    UNIQUEIDENTIFIER NULL,
    [EventFK]   BIGINT           NULL,
    [Comment]   VARCHAR (2048)   NULL,
    [TimeStamp] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([CommentId] ASC),
    FOREIGN KEY ([EventFK]) REFERENCES [dbo].[EventMemory] ([EventMemoryId])
);

