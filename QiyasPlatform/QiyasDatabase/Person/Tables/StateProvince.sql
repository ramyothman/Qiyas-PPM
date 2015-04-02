CREATE TABLE [Person].[StateProvince] (
    [StateProvinceId]     INT              IDENTITY (1, 1) NOT NULL,
    [StateProvinceCode]   NCHAR (3)        NULL,
    [CountryRegionCode]   CHAR (3)         NOT NULL,
    [IsOnlyStateProvince] BIT              NULL,
    [Name]                NVARCHAR (50)    NOT NULL,
    [RowGuid]             UNIQUEIDENTIFIER CONSTRAINT [DF_StateProvince_RowGuid] DEFAULT (newid()) NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_StateProvince_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_StateProvince] PRIMARY KEY CLUSTERED ([StateProvinceId] ASC),
    CONSTRAINT [FK_StateProvince_CountryRegion] FOREIGN KEY ([CountryRegionCode]) REFERENCES [Person].[CountryRegion] ([CountryRegionCode]) ON DELETE CASCADE ON UPDATE CASCADE
);

