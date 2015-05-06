CREATE TABLE [PPM].[BookRepackPackageItem] (
    [BookRepackPackageItemID] INT IDENTITY (1, 1) NOT NULL,
    [BookRepackPackageID]     INT NULL,
    [BookPackItemID]          INT NULL,
    CONSTRAINT [PK_BookRepackPackageItem] PRIMARY KEY CLUSTERED ([BookRepackPackageItemID] ASC),
    CONSTRAINT [FK_BookRepackPackageItem_BookPackItem] FOREIGN KEY ([BookPackItemID]) REFERENCES [PPM].[BookPackItem] ([BookPackItemID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BookRepackPackageItem_BookRepackPackage] FOREIGN KEY ([BookRepackPackageID]) REFERENCES [PPM].[BookRepackPackage] ([BookRepackPackageID]) ON DELETE CASCADE ON UPDATE CASCADE
);

