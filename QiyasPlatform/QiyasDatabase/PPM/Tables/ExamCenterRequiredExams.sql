CREATE TABLE [PPM].[ExamCenterRequiredExams] (
    [ExamCenterRequiredExamsID]  INT            IDENTITY (1, 1) NOT NULL,
    [ExamPeriodID]               INT            NULL,
    [ExamCenterID]               INT            NULL,
    [RequestPreparationStatusID] INT            NULL,
    [FileNeedsPath]              NVARCHAR (500) NULL,
    [CreatedDate]                DATETIME       NULL,
    [ModifiedDate]               DATETIME       NULL,
    [CreatedBy]                  INT            NULL,
    [ModifiedBy]                 INT            NULL,
    CONSTRAINT [PK_ExamCenterRequiredExams] PRIMARY KEY CLUSTERED ([ExamCenterRequiredExamsID] ASC),
    CONSTRAINT [FK_ExamCenterRequiredExams_ExamCenter] FOREIGN KEY ([ExamCenterID]) REFERENCES [PPM].[ExamCenter] ([ExaminationCenterID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ExamCenterRequiredExams_ExamPeriod] FOREIGN KEY ([ExamPeriodID]) REFERENCES [PPM].[ExamPeriod] ([ExamPeriodID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ExamCenterRequiredExams_RequestPreparationStatus] FOREIGN KEY ([RequestPreparationStatusID]) REFERENCES [PPM].[RequestPreparationStatus] ([RequestPreparationStatusID])
);



