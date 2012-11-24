CREATE TABLE [dbo].[webpages_OAuthMembership] (
    [Provider]       NVARCHAR (30)  NOT NULL,
    [ProviderUserId] NVARCHAR (100) NOT NULL,
    [UserId]         INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId]), 
    CONSTRAINT [FK_webpages_OAuthMembership_UsersProfile] FOREIGN KEY ([UserId]) REFERENCES [UserProfile]([UserId])
);

