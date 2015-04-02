CREATE TABLE [PPM].[ExamSpeciality] (
    [ExamSpecialityID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50) NULL,
    [ExamTypeID]       INT           NULL,
    [IsActive]         BIT           NULL,
    [CreatorID]        INT           NULL,
    [CreatedDate]      DATETIME      NULL,
    [ModifiedByID]     INT           NULL,
    [ModifiedDate]     DATETIME      NULL,
    CONSTRAINT [PK_ExamSpeciality] PRIMARY KEY CLUSTERED ([ExamSpecialityID] ASC),
    CONSTRAINT [FK_ExamSpeciality_ExamType] FOREIGN KEY ([ExamTypeID]) REFERENCES [PPM].[ExamType] ([ExaminationTypeID]) ON DELETE CASCADE ON UPDATE CASCADE
);

