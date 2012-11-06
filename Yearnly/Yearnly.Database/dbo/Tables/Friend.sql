CREATE TABLE [dbo].[Friend]
(
	[UserId] INT NOT NULL,
	[FriendId] INT NOT NULL ,  
    [DateAdded] DATETIME NOT NULL, 
    CONSTRAINT [PK_Friend] PRIMARY KEY ([UserId])
)
