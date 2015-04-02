CREATE TABLE [RoleSecurity].[UserLoginProvider] (
    [UserLoginProviderId] INT            IDENTITY (1, 1) NOT NULL,
    [UserId]              INT            NULL,
    [LoginProvider]       NVARCHAR (500) NULL,
    [ProviderKey]         NVARCHAR (150) NULL,
    [CreatedDate]         DATETIME       CONSTRAINT [DF_UserLoginProvider_CreatedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_UserLoginProvider] PRIMARY KEY CLUSTERED ([UserLoginProviderId] ASC),
    CONSTRAINT [FK_UserLoginProvider_Person] FOREIGN KEY ([UserId]) REFERENCES [Person].[Person] ([BusinessEntityId]) ON DELETE CASCADE ON UPDATE CASCADE
);

