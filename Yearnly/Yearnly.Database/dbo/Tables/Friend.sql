CREATE TABLE [dbo].[Friend]
(
	[UserId] INT NOT NULL,
	[FriendId] INT NOT NULL ,  
    [DateAdded] DATETIME NOT NULL, 
    CONSTRAINT [PK_Friend] PRIMARY KEY ([UserId], [FriendId]), 
    CONSTRAINT [FK_Friend_ToUserProfile] FOREIGN KEY ([FriendId]) REFERENCES [UserProfile]([UserId]),
	CONSTRAINT [FK_User_ToUserProfile] FOREIGN KEY ([UserId]) REFERENCES [UserProfile]([UserId])
)
