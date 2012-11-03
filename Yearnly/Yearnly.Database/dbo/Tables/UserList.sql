CREATE TABLE [dbo].[UserList] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [UserId]      INT            NOT NULL,
    [Name]        NVARCHAR (150) NOT NULL,
    [DateCreated] DATETIME       CONSTRAINT [DF_List_DateCreated] DEFAULT (getdate()) NULL,
    [DateUpdated] DATETIME       NULL,
    CONSTRAINT [PK_List] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ListToUser] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile] ([UserId])
);



