CREATE TABLE [Person].[EmailAddress] (
    [EmailAddressId]        INT              IDENTITY (1, 1) NOT NULL,
    [BusinessEntityId]      INT              NOT NULL,
    [EmailAddressTypeId]    INT              NOT NULL,
    [Email]                 NVARCHAR (60)    NOT NULL,
    [EmailVerified]         BIT              NULL,
    [EmailVerificationHash] NVARCHAR (128)   NULL,
    [RowGuid]               UNIQUEIDENTIFIER CONSTRAINT [DF_EmailAddress_RowGuid] DEFAULT (newid()) ROWGUIDCOL NULL,
    [ModifiedDate]          DATETIME         CONSTRAINT [DF_EmailAddress_ModifiedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_EmailAddressId] PRIMARY KEY CLUSTERED ([EmailAddressId] ASC),
    CONSTRAINT [FK_EmailAddress_EmailAddressType] FOREIGN KEY ([EmailAddressTypeId]) REFERENCES [Person].[EmailAddressType] ([EmailAddressTypeId]),
    CONSTRAINT [FK_EmailAddress_Person] FOREIGN KEY ([BusinessEntityId]) REFERENCES [Person].[Person] ([BusinessEntityId]) ON DELETE CASCADE ON UPDATE CASCADE
);

