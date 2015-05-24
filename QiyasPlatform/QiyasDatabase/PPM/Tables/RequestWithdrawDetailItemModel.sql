CREATE TABLE [PPM].[RequestWithdrawDetailItemModel] (
    [RequestWithdrawDetailItemModelID] INT IDENTITY (1, 1) NOT NULL,
    [RequestWithdrawDetailItemID]      INT NULL,
    [ExamModelID]                      INT NULL,
    CONSTRAINT [PK_RequestWithdrawDetailItemModel] PRIMARY KEY CLUSTERED ([RequestWithdrawDetailItemModelID] ASC),
    CONSTRAINT [FK_RequestWithdrawDetailItemModel_ExamModel] FOREIGN KEY ([ExamModelID]) REFERENCES [PPM].[ExamModel] ([ExamModelID]),
    CONSTRAINT [FK_RequestWithdrawDetailItemModel_RequestWithdrawDetailItem] FOREIGN KEY ([RequestWithdrawDetailItemID]) REFERENCES [PPM].[RequestWithdrawDetailItem] ([RequestWithdrawDetailItemID]) ON DELETE CASCADE ON UPDATE CASCADE
);

