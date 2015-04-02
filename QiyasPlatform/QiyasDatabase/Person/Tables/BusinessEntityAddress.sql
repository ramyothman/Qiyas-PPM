CREATE TABLE [Person].[BusinessEntityAddress] (
    [BusinessEntityAddressId] INT              IDENTITY (1, 1) NOT NULL,
    [BusinessEntityId]        INT              NOT NULL,
    [AddressId]               INT              NOT NULL,
    [AddressTypeId]           INT              NOT NULL,
    [RowGuid]                 UNIQUEIDENTIFIER CONSTRAINT [DF_BusinessEntityAddress_RowGuid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]            DATETIME         CONSTRAINT [DF_BusinessEntityAddress_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_BusinessEntityAddress] PRIMARY KEY CLUSTERED ([BusinessEntityAddressId] ASC),
    CONSTRAINT [FK_BusinessEntityAddress_Address] FOREIGN KEY ([AddressId]) REFERENCES [Person].[Address] ([AddressId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BusinessEntityAddress_AddressType] FOREIGN KEY ([AddressTypeId]) REFERENCES [Person].[AddressType] ([AddressTypeId]),
    CONSTRAINT [FK_BusinessEntityAddress_BusinessEntity] FOREIGN KEY ([BusinessEntityId]) REFERENCES [Person].[BusinessEntity] ([BusinessEntityId]) ON DELETE CASCADE ON UPDATE CASCADE
);

