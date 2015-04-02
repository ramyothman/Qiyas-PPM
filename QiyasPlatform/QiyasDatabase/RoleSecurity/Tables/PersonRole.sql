CREATE TABLE [RoleSecurity].[PersonRole] (
    [PersonRoleId] INT      IDENTITY (1, 1) NOT NULL,
    [RoleId]       INT      NULL,
    [PersonId]     INT      NULL,
    [ModifiedDate] DATETIME CONSTRAINT [DF_PersonRole_ModifiedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_PersonRole] PRIMARY KEY CLUSTERED ([PersonRoleId] ASC),
    CONSTRAINT [FK_PersonRole_Credential] FOREIGN KEY ([PersonId]) REFERENCES [Person].[Credential] ([BusinessEntityId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PersonRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [RoleSecurity].[Role] ([RoleId]) ON DELETE CASCADE ON UPDATE CASCADE
);

