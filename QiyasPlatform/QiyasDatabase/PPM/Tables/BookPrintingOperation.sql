CREATE TABLE [PPM].[BookPrintingOperation] (
    [BookPrintingOperationID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]                    NVARCHAR (50) NULL,
    [ExamID]                  INT           NULL,
    [PrintsForOneModel]       INT           NULL,
    [ExamsNeededForA3]        INT           NULL,
    [ExamsNeededForA4]        INT           NULL,
    [ExamsNeededForCD]        INT           NULL,
    [OperationStatusID]       INT           NULL,
    [CreatorID]               INT           NULL,
    [CreatedDate]             DATETIME      NULL,
    [ModifiedByID]            INT           NULL,
    [ModifiedDate]            DATETIME      NULL,
    CONSTRAINT [PK_BookPrintingOperation] PRIMARY KEY CLUSTERED ([BookPrintingOperationID] ASC),
    CONSTRAINT [FK_BookPrintingOperation_Exam] FOREIGN KEY ([ExamID]) REFERENCES [PPM].[Exam] ([ExamID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BookPrintingOperation_OperationStatus] FOREIGN KEY ([OperationStatusID]) REFERENCES [PPM].[OperationStatus] ([OperationStatusID])
);

