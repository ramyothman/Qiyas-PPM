CREATE TABLE [PPM].[RequestWithdrawDetail] (
    [RequestWithdrawDetailID]    INT IDENTITY (1, 1) NOT NULL,
    [RequestWithdrawID]          INT NULL,
    [ExamRequirementItemID]      INT NULL,
    [PrintsForOneModel]          INT NULL,
    [ExamsNeededForA3]           INT NULL,
    [ExamsNeededForA4]           INT NULL,
    [ExamsNeededForCD]           INT NULL,
    [ShippedPrintsForOneModel]   INT NULL,
    [ShippedExamsNeededForA3]    INT NULL,
    [ShippedExamsNeededForA4]    INT NULL,
    [ShippedExamsNeededForCD]    INT NULL,
    [RequestPreparationStatusID] INT NULL,
    CONSTRAINT [PK_RequestWithdrawDetail] PRIMARY KEY CLUSTERED ([RequestWithdrawDetailID] ASC),
    CONSTRAINT [FK_RequestWithdrawDetail_ExamRequirementItem] FOREIGN KEY ([ExamRequirementItemID]) REFERENCES [PPM].[ExamRequirementItem] ([ExamRequirementItemID]),
    CONSTRAINT [FK_RequestWithdrawDetail_RequestPreparationStatus] FOREIGN KEY ([RequestPreparationStatusID]) REFERENCES [PPM].[RequestPreparationStatus] ([RequestPreparationStatusID]),
    CONSTRAINT [FK_RequestWithdrawDetail_RequestWithdraw] FOREIGN KEY ([RequestWithdrawID]) REFERENCES [PPM].[RequestWithdraw] ([RequestWithdrawID]) ON DELETE CASCADE ON UPDATE CASCADE
);

