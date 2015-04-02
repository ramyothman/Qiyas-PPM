CREATE TABLE [Person].[AddressType] (
    [AddressTypeId] INT              IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50)    NOT NULL,
    [RowGuid]       UNIQUEIDENTIFIER CONSTRAINT [DF_AddressType_RowGuid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]  DATETIME         CONSTRAINT [DF_AddressType_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AddressType] PRIMARY KEY CLUSTERED ([AddressTypeId] ASC)
);

