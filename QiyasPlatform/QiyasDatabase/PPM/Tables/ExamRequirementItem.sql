CREATE TABLE [PPM].[ExamRequirementItem] (
    [ExamRequirementItemID]      INT IDENTITY (1, 1) NOT NULL,
    [ExamCenterRequiredExamsID]  INT NULL,
    [ExamID]                     INT NULL,
    [PrintsForOneModel]          INT NULL,
    [ExamsNeededForA3]           INT NULL,
    [ExamsNeededForA4]           INT NULL,
    [ExamsNeededForCD]           INT NULL,
    [RequestPreparationStatusID] INT NULL,
    CONSTRAINT [PK_ExamRequirementItem] PRIMARY KEY CLUSTERED ([ExamRequirementItemID] ASC),
    CONSTRAINT [FK_ExamRequirementItem_ExamCenterRequiredExams] FOREIGN KEY ([ExamCenterRequiredExamsID]) REFERENCES [PPM].[ExamCenterRequiredExams] ([ExamCenterRequiredExamsID]) ON DELETE CASCADE ON UPDATE CASCADE
);



