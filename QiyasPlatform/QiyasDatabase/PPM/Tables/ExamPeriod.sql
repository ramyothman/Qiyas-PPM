CREATE TABLE [PPM].[ExamPeriod] (
    [ExamPeriodID]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (50) NULL,
    [ExamTypeID]      INT           NULL,
    [ExamYear]        INT           NULL,
    [StudentGenderID] INT           NULL,
    [StartDate]       DATETIME      NULL,
    [IsActive]        BIT           NULL,
    [CreatorID]       INT           NULL,
    [CreatedDate]     DATETIME      NULL,
    [ModifiedByID]    INT           NULL,
    [ModifiedDate]    DATETIME      NULL,
    [EndDate]         DATETIME      NULL,
    CONSTRAINT [PK_ExamPeriod] PRIMARY KEY CLUSTERED ([ExamPeriodID] ASC),
    CONSTRAINT [FK_ExamPeriod_ExamType] FOREIGN KEY ([ExamTypeID]) REFERENCES [PPM].[ExamType] ([ExaminationTypeID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ExamPeriod_StudentGender] FOREIGN KEY ([StudentGenderID]) REFERENCES [PPM].[StudentGender] ([StudentGenderID])
);

