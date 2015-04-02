CREATE TABLE [PPM].[PackageWeight] (
    [PackageWeightID] INT             IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (50)   NULL,
    [Weight]          DECIMAL (18, 2) NULL,
    [PackageCode]     INT             NULL,
    [CreatorID]       INT             NULL,
    [CreatedDate]     DATETIME        NULL,
    [ModifiedByID]    INT             NULL,
    [ModifiedDate]    DATETIME        NULL,
    CONSTRAINT [PK_PackageWeight] PRIMARY KEY CLUSTERED ([PackageWeightID] ASC)
);

