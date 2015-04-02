CREATE TABLE [RoleSecurity].[Role] (
    [RoleId]       INT              IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50)    NULL,
    [IsActive]     BIT              NULL,
    [RowGuid]      UNIQUEIDENTIFIER CONSTRAINT [DF_Role_RowGuid] DEFAULT (newid()) ROWGUIDCOL NULL,
    [ModifiedDate] DATETIME         CONSTRAINT [DF_Role_ModifiedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

