CREATE TABLE [PPM].[BookPackingOperation] (
    [BookPackingOperationID]   INT           IDENTITY (1, 1) NOT NULL,
    [Name]                     NVARCHAR (50) NULL,
    [CreatorID]                INT           NULL,
    [CreatedDate]              DATETIME      NULL,
    [ModifiedByID]             INT           NULL,
    [ModifiedDate]             DATETIME      NULL,
    [BookPrintingOperationID]  INT           NULL,
    [PackagingTypeID]          INT           NULL,
    [PackingCalculationTypeID] INT           NULL,
    [AllocatedFrom]            NVARCHAR (50) NULL,
    [PackingValue]             INT           NULL,
    [PackageTotal]             INT           NULL,
    [PackingParentID]          INT           NULL,
    CONSTRAINT [PK_BookPackingOperation] PRIMARY KEY CLUSTERED ([BookPackingOperationID] ASC),
    CONSTRAINT [FK_BookPackingOperation_BookPackingOperation] FOREIGN KEY ([PackingParentID]) REFERENCES [PPM].[BookPackingOperation] ([BookPackingOperationID]),
    CONSTRAINT [FK_BookPackingOperation_BookPrintingOperation] FOREIGN KEY ([BookPrintingOperationID]) REFERENCES [PPM].[BookPrintingOperation] ([BookPrintingOperationID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BookPackingOperation_PackagingType] FOREIGN KEY ([PackagingTypeID]) REFERENCES [PPM].[PackagingType] ([PackagingTypeID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BookPackingOperation_PackingCalculationType] FOREIGN KEY ([PackingCalculationTypeID]) REFERENCES [PPM].[PackingCalculationType] ([PackingCalculationTypeID])
);



