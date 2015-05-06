CREATE TABLE [PPM].[BookRepackPackage] (
    [BookRepackPackageID] INT      IDENTITY (1, 1) NOT NULL,
    [CreatedDate]         DATETIME NULL,
    [ModifiedDate]        DATETIME NULL,
    [CreatedBy]           INT      NULL,
    [ModifiedBy]          INT      NULL,
    CONSTRAINT [PK_BookRepackPackage] PRIMARY KEY CLUSTERED ([BookRepackPackageID] ASC)
);

