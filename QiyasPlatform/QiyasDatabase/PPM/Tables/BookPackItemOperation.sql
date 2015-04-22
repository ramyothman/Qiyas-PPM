CREATE TABLE [PPM].[BookPackItemOperation] (
    [BookPackItemOperationID]  INT           IDENTITY (1, 1) NOT NULL,
    [Name]                     NVARCHAR (50) NULL,
    [CreatorID]                INT           NULL,
    [CreatedDate]              DATETIME      NULL,
    [ModifiedByID]             INT           NULL,
    [ModifiedDate]             DATETIME      NULL,
    [BookPackItemID]           INT           NULL,
    [PackagingTypeID]          INT           NULL,
    [PackingCalculationTypeID] INT           NULL,
    [AllocatedFrom]            NVARCHAR (50) NULL,
    [PackingValue]             INT           NULL,
    [PackageTotal]             INT           NULL,
    [PackingParentID]          INT           NULL,
    CONSTRAINT [PK_BookPackItemOperation] PRIMARY KEY CLUSTERED ([BookPackItemOperationID] ASC),
    CONSTRAINT [FK_BookPackItemOperation_BookPackItem] FOREIGN KEY ([BookPackItemID]) REFERENCES [PPM].[BookPackItem] ([BookPackItemID]) ON DELETE CASCADE ON UPDATE CASCADE
);

