CREATE TABLE [RoleSecurity].[SecurityAccessType] (
    [SecurityAccessTypeId] INT              IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (50)    NULL,
    [RowGuid]              UNIQUEIDENTIFIER CONSTRAINT [DF_SecurityAccessType_RowGuid] DEFAULT (newid()) ROWGUIDCOL NULL,
    [ModifiedDate]         DATETIME         CONSTRAINT [DF_SecurityAccessType_ModifiedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_SecurityAccessType] PRIMARY KEY CLUSTERED ([SecurityAccessTypeId] ASC)
);

