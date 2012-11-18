CREATE TABLE [dbo].[FriendRequest]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1, 1), 
    [FromUserId] INT NOT NULL, 
    [ToUserId] INT NOT NULL, 
    [DateAdded] DATETIME NOT NULL, 
    CONSTRAINT [FK_FromFriendRequest_User] FOREIGN KEY ([FromUserId]) REFERENCES [UserProfile]([UserId]),
	CONSTRAINT [FK_ToFriendRequest_User] FOREIGN KEY ([ToUserId]) REFERENCES [UserProfile]([UserId])
)
