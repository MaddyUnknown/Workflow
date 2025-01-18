CREATE TABLE [dbo].[tbl_Project]
(
	[iProjectId] BIGINT IDENTITY(1,1), 
    [vcTitle] VARCHAR(100) NOT NULL, 
    CONSTRAINT [tbl_Project_PK_iProjectId] PRIMARY KEY ([iProjectId]) 
)
