CREATE TABLE [dbo].[tbl_Token]
(
	[iTokenId] BIGINT IDENTITY(1,1),
    [iUserId] BIGINT NOT NULL,
    [iTokenType] INT NOT NULL, 
    [vcTokenString] VARCHAR(250) NOT NULL, 
    [dtCreationDateTime] DATETIME NOT NULL, 
    [dtExpiryDateTime] DATETIME NOT NULL, 
    CONSTRAINT [tbl_Token_PK_iTokenId] PRIMARY KEY ([iTokenId]),
    CONSTRAINT [tbl_Token_FK_iUserId] FOREIGN KEY ([iUserId]) REFERENCES [tbl_User]([iUserId]), 
    CONSTRAINT [tbl_Token_UC_iUserId_iTokenType] UNIQUE ([iUserId], [iTokenType])
)
