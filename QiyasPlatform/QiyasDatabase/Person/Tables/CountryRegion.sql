CREATE TABLE [Person].[CountryRegion] (
    [CountryRegionCode] CHAR (3)      NOT NULL,
    [Name]              NVARCHAR (50) NOT NULL,
    [ModifiedDate]      DATETIME      CONSTRAINT [DF_CountryRegion_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_CountryRegion] PRIMARY KEY CLUSTERED ([CountryRegionCode] ASC)
);

