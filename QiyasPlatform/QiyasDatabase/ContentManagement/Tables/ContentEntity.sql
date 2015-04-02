CREATE TABLE [ContentManagement].[ContentEntity] (
    [ContentEntityId]   INT              IDENTITY (1, 1) NOT NULL,
    [ContentEntityType] CHAR (2)         NULL,
    [RowGuid]           UNIQUEIDENTIFIER CONSTRAINT [DF_ContentEntity_RowGuid] DEFAULT (newid()) ROWGUIDCOL NULL,
    [ModifiedDate]      DATETIME         NULL,
    CONSTRAINT [PK_ContentEntity] PRIMARY KEY CLUSTERED ([ContentEntityId] ASC)
);

