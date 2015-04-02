CREATE TABLE [ContentManagement].[SystemFolder] (
    [SystemFolderId] INT            NOT NULL,
    [Name]           NVARCHAR (150) NULL,
    [Path]           NVARCHAR (500) NULL,
    CONSTRAINT [PK_SystemFolder] PRIMARY KEY CLUSTERED ([SystemFolderId] ASC),
    CONSTRAINT [FK_SystemFolder_ContentEntity] FOREIGN KEY ([SystemFolderId]) REFERENCES [ContentManagement].[ContentEntity] ([ContentEntityId])
);

