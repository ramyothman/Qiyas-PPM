CREATE TABLE [PPM].[ContainerRequest] (
    [ContainerRequestID]        INT      IDENTITY (1, 1) NOT NULL,
    [ExamCenterRequiredExamsID] INT      NULL,
    [CreatedDate]               DATETIME NULL,
    [ModifiedDate]              DATETIME NULL,
    [CreatedBy]                 INT      NULL,
    [ModifiedBy]                INT      NULL,
    CONSTRAINT [PK_ContainerRequest] PRIMARY KEY CLUSTERED ([ContainerRequestID] ASC),
    CONSTRAINT [FK_ContainerRequest_ExamCenterRequiredExams] FOREIGN KEY ([ExamCenterRequiredExamsID]) REFERENCES [PPM].[ExamCenterRequiredExams] ([ExamCenterRequiredExamsID]) ON DELETE CASCADE ON UPDATE CASCADE
);

