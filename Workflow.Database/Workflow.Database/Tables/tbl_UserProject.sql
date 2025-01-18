CREATE TABLE [dbo].[tbl_UserProject]
(
	[iUserProjectId] BIGINT IDENTITY(1,1), 
    [iProjectId] BIGINT NOT NULL, 
    [iUserId] BIGINT NOT NULL, 
    [bIsOwner] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [tbl_UserProject_PK_iUserProjectId] PRIMARY KEY ([iUserProjectId]), 
    CONSTRAINT [tbl_UserProject_UC_iProjectId_iUserId] UNIQUE ([iProjectId], [iUserId]), 
    CONSTRAINT [tbl_UserProject_FK_iProjectId] FOREIGN KEY ([iProjectId]) REFERENCES [tbl_Project]([iProjectId]), 
    CONSTRAINT [tbl_UserProject_FK_iUserId] FOREIGN KEY ([iUserId]) REFERENCES [tbl_User]([iUserId]) 
)
