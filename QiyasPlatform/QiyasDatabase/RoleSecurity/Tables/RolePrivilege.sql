CREATE TABLE [RoleSecurity].[RolePrivilege] (
    [RolePrivilegeId]  INT      IDENTITY (1, 1) NOT NULL,
    [RoleId]           INT      NULL,
    [ContentEntityId]  INT      NULL,
    [SystemFunctionId] INT      NULL,
    [HasAccess]        BIT      NULL,
    [ModifiedDate]     DATETIME CONSTRAINT [DF_RolePrivilege_ModifiedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_RolePrivilege] PRIMARY KEY CLUSTERED ([RolePrivilegeId] ASC),
    CONSTRAINT [FK_RolePrivilege_ContentEntity] FOREIGN KEY ([ContentEntityId]) REFERENCES [ContentManagement].[ContentEntity] ([ContentEntityId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_RolePrivilege_Role] FOREIGN KEY ([RoleId]) REFERENCES [RoleSecurity].[Role] ([RoleId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_RolePrivilege_SystemFunction] FOREIGN KEY ([SystemFunctionId]) REFERENCES [RoleSecurity].[SystemFunction] ([SystemFunctionId])
);

