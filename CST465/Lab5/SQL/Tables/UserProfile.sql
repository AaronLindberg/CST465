CREATE TABLE [dbo].[UserProfile] (
    [UserId]    UNIQUEIDENTIFIER NOT NULL,
    [FirstName] VARCHAR (50)     NOT NULL,
    [LastName]  VARCHAR (50)     NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

