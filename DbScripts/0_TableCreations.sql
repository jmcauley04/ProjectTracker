USE [db_a8fb43_projecttracker]



GO
IF OBJECT_ID('dbo.[PunchlistEntries]') IS NOT NULL DROP TABLE dbo.PunchlistEntries;
IF OBJECT_ID('dbo.[TaskEntries]') IS NOT NULL DROP TABLE dbo.TaskEntries;

IF OBJECT_ID('dbo.[TaskPriorities]') IS NOT NULL DROP TABLE dbo.TaskPriorities;
IF OBJECT_ID('dbo.[TaskStatuses]') IS NOT NULL DROP TABLE dbo.TaskStatuses;
IF OBJECT_ID('dbo.[PunchlistStatuses]') IS NOT NULL DROP TABLE dbo.PunchlistStatuses;
IF OBJECT_ID('dbo.[PunchlistPriorities]') IS NOT NULL DROP TABLE dbo.PunchlistPriorities;
IF OBJECT_ID('dbo.[PunchlistOwners]') IS NOT NULL DROP TABLE dbo.PunchlistOwners;
IF OBJECT_ID('dbo.[PunchlistFlags]') IS NOT NULL DROP TABLE dbo.PunchlistFlags;

IF OBJECT_ID('dbo.[Comments]') IS NOT NULL DROP TABLE dbo.Comments;
IF OBJECT_ID('dbo.[HistoryLogs]') IS NOT NULL DROP TABLE dbo.HistoryLogs;

IF OBJECT_ID('dbo.[RelationTypes]') IS NOT NULL DROP TABLE dbo.RelationTypes;
GO

CREATE TABLE [dbo].[TaskStatuses] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](512) NOT NULL,
	[Color] nvarchar(20) NOT NULL,
)

INSERT INTO dbo.TaskStatuses ([Name], [Description], [Color])
VALUES
	('New', 'Task has not yet been  assigned', 'hsl(150 90% 40%)'),
	('Working on it', 'Task has been assigned', 'hsl(35 100% 60%)'),
	('Stuck', 'Task is blocked', 'hsl(0 100% 60%)'),
	('Done', 'Task is completed', 'hsl(150 90% 40%)');

GO

CREATE TABLE [dbo].[TaskPriorities] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](512) NOT NULL,
	[Color] nvarchar(20) NOT NULL,
)

INSERT INTO dbo.TaskPriorities ([Name], [Description], [Color])
VALUES
	('Low', 'Must be prioritized after mid items', 'hsl(205 100% 80%)'),
	('Mid', 'Must be prioritized after high items', 'hsl(205 100% 50%)'),
	('High', 'Must be prioritized after go-live', 'hsl(205 70% 30%)'),
	('Critical', 'Must be completed before go-live', 'hsl(240 80% 20%)');

GO

CREATE TABLE [dbo].[TaskEntries] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[StatusId] [int] NOT NULL,
	[PriorityId] [int] NOT NULL,
	[Created] [datetime2] NOT NULL DEFAULT GETDATE(),
	[Due] [datetime2] NULL,
	CONSTRAINT FK_TaskEntries_TaskPriorities FOREIGN KEY (PriorityId) REFERENCES dbo.TaskPriorities (Id),
	CONSTRAINT FK_TaskEntries_TaskStatuses FOREIGN KEY (StatusId) REFERENCES dbo.TaskStatuses (Id),
)

GO

CREATE TABLE [dbo].[PunchlistStatuses] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](512) NOT NULL,
	[Color] nvarchar(20) NOT NULL,
)

INSERT INTO dbo.PunchlistStatuses ([Name], [Description], [Color])
VALUES
	('New', 'Item has not yet been  assigned', 'hsl(0 80% 40%)'),
	('Assigned', 'Item has been assigned', 'hsl(30 100% 50%)'),
	('Contained', 'Item is temporarily handled for the purposes of go-live', 'hsl(50 100% 50%)'),
	('Complete', 'Item is completed', 'hsl(80 40% 60%)');

GO

CREATE TABLE [dbo].[PunchlistPriorities] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](512) NOT NULL,
	[Color] nvarchar(20) NOT NULL,
)

INSERT INTO dbo.PunchlistPriorities ([Name], [Description], [Color])
VALUES
	('Low', 'Must be prioritized after mid items', 'hsl(80 40% 60%)'),
	('Mid', 'Must be prioritized after high items', 'hsl(50 100% 50%)'),
	('High', 'Must be prioritized after go-live', 'hsl(30 100% 50%)'),
	('Critical', 'Must be completed before go-live', 'hsl(0 80% 40%)');

GO

CREATE TABLE [dbo].[PunchlistOwners] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](512) NOT NULL,
	[Color] nvarchar(20) NOT NULL,
)

INSERT INTO dbo.PunchlistOwners ([Name], [Description], [Color])
VALUES
	('Walmart', 'Walmart', 'hsl(209 100% 43%)'),
	('Symbotic', 'Symbotic', 'hsl(94 55% 48%)'),
	('Cadell', 'Caddell', 'hsl(41 90% 55%)');

GO

CREATE TABLE [dbo].[PunchlistFlags] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](512) NOT NULL,
	[Color] nvarchar(20) NOT NULL,
)

INSERT INTO dbo.PunchlistFlags ([Name], [Description], [Color])
VALUES
	('Maintenance', 'This is related to maintenance', 'hsl(50 100% 50%)'),
	('Safety', 'This is related to safety', 'hsl(80 40% 60%)'),
	('Other', 'This is related to something not yet categorized', 'hsl(0 20% 60%)');

GO

CREATE TABLE [dbo].[PunchlistEntries] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Resolution] [nvarchar](max) NULL,
	[BeforeImage] [nvarchar](max) NULL,
	[AfterImage] [nvarchar](max) NULL,
	[StatusId] [int] NOT NULL,
	[PriorityId] [int] NOT NULL,
	[OwnerId] [int] NOT NULL,
	[FlagId] [int] NOT NULL,
	[Created] [datetime2] NOT NULL DEFAULT GETDATE(),
	[Due] [datetime2] NULL,
	[ApprovedBy] [nvarchar](200) NULL,
	[X] float NULL,
	[Y] float NULL,
	CONSTRAINT FK_PunchlistEntries_PunchlistPriorities FOREIGN KEY (PriorityId) REFERENCES dbo.PunchlistPriorities (Id),
	CONSTRAINT FK_PunchlistEntries_PunchlistStatuses FOREIGN KEY (StatusId) REFERENCES dbo.PunchlistStatuses (Id),
	CONSTRAINT FK_PunchlistEntries_PunchlistFlags FOREIGN KEY (FlagId) REFERENCES dbo.PunchlistFlags (Id),
	CONSTRAINT FK_PunchlistEntries_PunchlistOwners FOREIGN KEY (OwnerId) REFERENCES dbo.PunchlistOwners (Id),
)

GO

CREATE TABLE [dbo].[RelationTypes] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
)

INSERT INTO dbo.RelationTypes ([Name])
VALUES
	('Punchlist'),
	('Task');

GO

CREATE TABLE [dbo].[Comments] (	
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[RelationTypeId] [int] NOT NULL,
	[RelationId] [int] NOT NULL,
	[User] [nvarchar](200) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[Created] [datetime2] NOT NULL DEFAULT GETDATE(),
	CONSTRAINT FK_Comments_RelationTypes FOREIGN KEY (RelationTypeId) REFERENCES dbo.RelationTypes (Id),
)

CREATE TABLE [dbo].[HistoryLogs] (	
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[RelationTypeId] [int] NOT NULL,
	[RelationId] [int] NOT NULL,
	[User] [nvarchar](200) NOT NULL,
	[Timestamp] [datetime2] NOT NULL DEFAULT GETDATE(),
	[Description] [nvarchar](1000) NOT NULL,
	[Old] [nvarchar](1000) NOT NULL,
	[New] [nvarchar](1000) NOT NULL,
	CONSTRAINT FK_HistoryLogs_RelationTypes FOREIGN KEY (RelationTypeId) REFERENCES dbo.RelationTypes (Id),
)

GO
