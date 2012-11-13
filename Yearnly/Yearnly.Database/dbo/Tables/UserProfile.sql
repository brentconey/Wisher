CREATE TABLE [dbo].[UserProfile] (
    [UserId]   INT           IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (56) NOT NULL,
    [Email] NVARCHAR(250) NULL, 
    [FirstName] NVARCHAR(150) NULL, 
    [LastName] NVARCHAR(250) NULL, 
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    UNIQUE NONCLUSTERED ([UserName] ASC)
);

