CREATE TABLE [RoleSecurity].[UserMonitor] (
    [UserMonitorId] INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]      INT           NULL,
    [IsSuccess]     BIT           NULL,
    [UserIP]        NVARCHAR (50) NULL,
    [UserName]      NVARCHAR (50) NULL,
    [DateCreated]   DATETIME      NULL,
    CONSTRAINT [PK_UserMonitor] PRIMARY KEY CLUSTERED ([UserMonitorId] ASC)
);

