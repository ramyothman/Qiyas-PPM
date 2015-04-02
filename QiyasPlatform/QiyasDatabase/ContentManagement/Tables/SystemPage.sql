CREATE TABLE [ContentManagement].[SystemPage] (
    [SystemPageId]         INT              NOT NULL,
    [Name]                 NVARCHAR (50)    NULL,
    [Path]                 NVARCHAR (150)   NULL,
    [SecurityAccessTypeId] INT              NULL,
    [IsActive]             BIT              NULL,
    [RowGuid]              UNIQUEIDENTIFIER CONSTRAINT [DF_SystemPage_RowGuid] DEFAULT (newid()) ROWGUIDCOL NULL,
    [ModifiedDate]         DATETIME         CONSTRAINT [DF_SystemPage_ModifiedDate] DEFAULT (getdate()) NULL,
    [SystemFolderId]       INT              NULL,
    CONSTRAINT [PK_SystemPage] PRIMARY KEY CLUSTERED ([SystemPageId] ASC),
    CONSTRAINT [FK_SystemPage_ContentEntity] FOREIGN KEY ([SystemPageId]) REFERENCES [ContentManagement].[ContentEntity] ([ContentEntityId]),
    CONSTRAINT [FK_SystemPage_SecurityAccessType] FOREIGN KEY ([SecurityAccessTypeId]) REFERENCES [RoleSecurity].[SecurityAccessType] ([SecurityAccessTypeId]),
    CONSTRAINT [FK_SystemPage_SystemFolder] FOREIGN KEY ([SystemFolderId]) REFERENCES [ContentManagement].[SystemFolder] ([SystemFolderId]) ON DELETE CASCADE ON UPDATE CASCADE
);

