CREATE TABLE [PPM].[BookPackingOperation] (
    [BookPackingOperationID] INT           NOT NULL,
    [Name]                   NVARCHAR (50) NULL,
    [CreatorID]              INT           NULL,
    [CreatedDate]            DATETIME      NULL,
    [ModifiedByID]           INT           NULL,
    [ModifiedDate]           DATETIME      NULL,
    CONSTRAINT [PK_BookPackingOperation] PRIMARY KEY CLUSTERED ([BookPackingOperationID] ASC)
);

