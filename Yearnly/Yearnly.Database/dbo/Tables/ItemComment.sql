CREATE TABLE [dbo].[ItemComment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ItemId] INT NOT NULL,
	[UserId] INT NOT NULL,
	[Comment] NVARCHAR(500) NOT NULL,
	[DateAdded] DATETIME NOT NULL DEFAULT GETDATE(), 
    CONSTRAINT [FK_ItemComment_ToItem] FOREIGN KEY ([ItemId]) REFERENCES [UserItem]([Id]),
	CONSTRAINT [FK_ItemComment_ToUser] FOREIGN KEY ([UserId]) REFERENCES [UserProfile]([UserId])
)
