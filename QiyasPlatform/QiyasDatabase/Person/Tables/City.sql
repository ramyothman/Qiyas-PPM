CREATE TABLE [Person].[City] (
    [CityID]        INT           IDENTITY (1, 1) NOT NULL,
    [StateRegionID] INT           NULL,
    [Name]          NVARCHAR (50) NULL,
    [IsActive]      BIT           NULL,
    CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([CityID] ASC),
    CONSTRAINT [FK_City_StateProvince] FOREIGN KEY ([StateRegionID]) REFERENCES [Person].[StateProvince] ([StateProvinceId]) ON DELETE CASCADE ON UPDATE CASCADE
);

