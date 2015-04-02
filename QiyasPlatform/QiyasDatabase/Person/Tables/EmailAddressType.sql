CREATE TABLE [Person].[EmailAddressType] (
    [EmailAddressTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (50) NOT NULL,
    [ModifiedDate]       DATETIME      CONSTRAINT [DF_EmailAddressType_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_EmailAddressType] PRIMARY KEY CLUSTERED ([EmailAddressTypeId] ASC)
);

