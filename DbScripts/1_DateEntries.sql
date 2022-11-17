USE [db_a8fb43_projecttracker]
GO

IF OBJECT_ID('dbo.[ProjectEvents]') IS NOT NULL DROP TABLE dbo.ProjectEvents;

CREATE TABLE [dbo].[ProjectEvents] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
    [TargetDate] [datetime2] NOT NULL DEFAULT GETDATE(),
    [OriginalDate] [datetime2] NOT NULL DEFAULT GETDATE()
);

