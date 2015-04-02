CREATE TABLE [RoleSecurity].[SystemFunction] (
    [SystemFunctionId]  INT              IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50)    NULL,
    [IsActive]          BIT              NULL,
    [IsBackendFunction] BIT              NULL,
    [RowGuid]           UNIQUEIDENTIFIER CONSTRAINT [DF_SystemFunction_RowGuid] DEFAULT (newid()) ROWGUIDCOL NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_SystemFunction_ModifiedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_SystemFunction] PRIMARY KEY CLUSTERED ([SystemFunctionId] ASC)
);

