CREATE TABLE [PPM].[BookPackItemModel] (
    [BookPackItemModelID] INT IDENTITY (1, 1) NOT NULL,
    [BookPackItemID]      INT NULL,
    [ExamModelID]         INT NULL,
    CONSTRAINT [PK_BookPackItemModel] PRIMARY KEY CLUSTERED ([BookPackItemModelID] ASC),
    CONSTRAINT [FK_BookPackItemModel_BookPackItem] FOREIGN KEY ([BookPackItemID]) REFERENCES [PPM].[BookPackItem] ([BookPackItemID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BookPackItemModel_ExamModel] FOREIGN KEY ([ExamModelID]) REFERENCES [PPM].[ExamModel] ([ExamModelID]) ON DELETE CASCADE ON UPDATE CASCADE
);

