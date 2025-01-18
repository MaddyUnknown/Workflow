CREATE TABLE [dbo].[tbl_User]
(
	[iUserId] BIGINT IDENTITY(1,1), 
    [vcName] VARCHAR(100) NOT NULL, 
    [vcUsername] VARCHAR(50) NOT NULL, 
    [vcEmail] VARCHAR(100) NULL, 
    [vcHashedPassword] VARCHAR(100) NOT NULL, 
    CONSTRAINT [tbl_User_PK_iUserId] PRIMARY KEY ([iUserId]),
    CONSTRAINT [tbl_User_UC_vcUsername] UNIQUE ([vcUsername]), 
    CONSTRAINT [tbl_User_UC_vcEmail] UNIQUE ([vcEmail])
)
