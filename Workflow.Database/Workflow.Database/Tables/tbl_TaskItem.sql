CREATE TABLE [dbo].[tbl_TaskItem]
(
	[iTaskId] BIGINT IDENTITY(1,1),
    [iProjectId] BIGINT NOT NULL,
    [vcTitle] VARCHAR(100) NOT NULL,
    [vcDescription] VARCHAR(500) NULL,
    [dtDeadline] DATE NULL,
    [iStatus] INT NOT NULL DEFAULT 0,
    [iCreatorUserId] BIGINT NOT NULL,
    [iAssignedUserId] BIGINT NULL,
    CONSTRAINT [tbl_TaskItem_PK_iTaskId] PRIMARY KEY ([iTaskId]),
    CONSTRAINT [tbl_TaskItem_FK_iProjectId] FOREIGN KEY ([iProjectId]) REFERENCES [tbl_Project]([iProjectId]), 
    CONSTRAINT [tbl_TaskItem_FK_iCreatorUserId] FOREIGN KEY ([iCreatorUserId]) REFERENCES [tbl_User]([iUserId]), 
    CONSTRAINT [tbl_TaskItem_FK_iAssignedUserId] FOREIGN KEY ([iAssignedUserId]) REFERENCES [tbl_User]([iUserId])
)
