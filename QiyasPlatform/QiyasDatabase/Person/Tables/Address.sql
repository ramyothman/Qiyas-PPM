CREATE TABLE [Person].[Address] (
    [AddressId]         INT              IDENTITY (1, 1) NOT NULL,
    [AddressLine1]      NVARCHAR (60)    NULL,
    [AddressLine2]      NVARCHAR (60)    NULL,
    [AddressLine3]      NVARCHAR (60)    NULL,
    [CountryRegionCode] CHAR (3)         NOT NULL,
    [City]              NVARCHAR (30)    NULL,
    [StateProvinceId]   INT              NULL,
    [PostalCode]        NVARCHAR (15)    NULL,
    [ZipCode]           NVARCHAR (15)    NULL,
    [SpatialLocation]   NVARCHAR (60)    NULL,
    [RowGuid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Address_RowGuid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Address_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([AddressId] ASC),
    CONSTRAINT [FK_Address_CountryRegion] FOREIGN KEY ([CountryRegionCode]) REFERENCES [Person].[CountryRegion] ([CountryRegionCode]),
    CONSTRAINT [FK_Address_StateProvince] FOREIGN KEY ([StateProvinceId]) REFERENCES [Person].[StateProvince] ([StateProvinceId])
);


GO
ALTER TABLE [Person].[Address] NOCHECK CONSTRAINT [FK_Address_StateProvince];

