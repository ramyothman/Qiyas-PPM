CREATE TABLE [RoleSecurity].[UserClaim] (
    [UserClaimId] INT            IDENTITY (1, 1) NOT NULL,
    [UserId]      INT            NULL,
    [ClaimValue]  NVARCHAR (150) NULL,
    [ClaimType]   NCHAR (150)    NULL,
    [CreatedDate] DATETIME       CONSTRAINT [DF_UserClaim_CreatedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED ([UserClaimId] ASC),
    CONSTRAINT [FK_UserClaim_Person] FOREIGN KEY ([UserId]) REFERENCES [Person].[Person] ([BusinessEntityId]) ON DELETE CASCADE ON UPDATE CASCADE
);

