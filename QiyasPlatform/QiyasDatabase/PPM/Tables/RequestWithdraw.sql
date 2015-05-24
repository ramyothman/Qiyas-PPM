CREATE TABLE [PPM].[RequestWithdraw] (
    [RequestWithdrawID]         INT      IDENTITY (1, 1) NOT NULL,
    [ExamCenterRequiredExamsID] INT      NULL,
    [RequestOrder]              INT      NULL,
    [ModifiedDate]              DATETIME NULL,
    [CreatedDate]               DATETIME NULL,
    [CreatedBy]                 INT      NULL,
    [ModifiedBy]                INT      NULL,
    CONSTRAINT [PK_RequestWithdraw] PRIMARY KEY CLUSTERED ([RequestWithdrawID] ASC),
    CONSTRAINT [FK_RequestWithdraw_ExamCenterRequiredExams] FOREIGN KEY ([ExamCenterRequiredExamsID]) REFERENCES [PPM].[ExamCenterRequiredExams] ([ExamCenterRequiredExamsID]) ON DELETE CASCADE ON UPDATE CASCADE
);

