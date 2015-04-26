CREATE TABLE [PPM].[BookPackItem] (
    [BookPackItemID]         INT             IDENTITY (1, 1) NOT NULL,
    [BookPackingOperationID] INT             NULL,
    [PackCode]               NVARCHAR (20)   NULL,
    [PackSerial]             INT             NULL,
    [Weight]                 DECIMAL (18, 2) NULL,
    [OperationStatusID]      INT             NULL,
    [ParentID]               INT             NULL,
    [StartBookSerial]        INT             NULL,
    [LastBookSerial]         INT             NULL,
    CONSTRAINT [PK_BookPackItem] PRIMARY KEY CLUSTERED ([BookPackItemID] ASC),
    CONSTRAINT [FK_BookPackItem_BookPackingOperation] FOREIGN KEY ([BookPackingOperationID]) REFERENCES [PPM].[BookPackingOperation] ([BookPackingOperationID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BookPackItem_BookPackItem] FOREIGN KEY ([ParentID]) REFERENCES [PPM].[BookPackItem] ([BookPackItemID])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_BookPackItem_Code]
    ON [PPM].[BookPackItem]([PackCode] ASC);

