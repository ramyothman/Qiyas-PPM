CREATE TABLE [PPM].[ShippingBagItem] (
    [ShippingBagItemID] INT      IDENTITY (1, 1) NOT NULL,
    [ShippingBagID]     INT      NULL,
    [BookPackItemID]    INT      NULL,
    [CreatedDate]       DATETIME NULL,
    [ModifiedDate]      DATETIME NULL,
    [CreatedBy]         INT      NULL,
    [ModifiedBy]        INT      NULL,
    CONSTRAINT [PK_ShippingBagItem] PRIMARY KEY CLUSTERED ([ShippingBagItemID] ASC),
    CONSTRAINT [FK_ShippingBagItem_BookPackItem] FOREIGN KEY ([BookPackItemID]) REFERENCES [PPM].[BookPackItem] ([BookPackItemID]),
    CONSTRAINT [FK_ShippingBagItem_ShippingBag] FOREIGN KEY ([ShippingBagID]) REFERENCES [PPM].[ShippingBag] ([ShippingBagID]) ON DELETE CASCADE ON UPDATE CASCADE
);

