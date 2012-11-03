CREATE TABLE [dbo].[UserItem] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [UserId]      INT            NOT NULL,
    [Title]       NVARCHAR (250) NOT NULL,
    [Link]        NVARCHAR (250) NULL,
    [Description] NVARCHAR (250) NULL,
    [DateCreated] DATETIME       CONSTRAINT [DF_UserItem_DateCreated] DEFAULT (getdate()) NULL,
    [DateUpdated] DATETIME       NULL,
    CONSTRAINT [PK_UserItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ItemToUser] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile] ([UserId])
);





