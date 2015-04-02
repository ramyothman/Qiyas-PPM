CREATE TABLE [PPM].[Exam] (
    [ExamID]           INT            IDENTITY (1, 1) NOT NULL,
    [ExamCode]         NVARCHAR (12)  NULL,
    [Name]             NVARCHAR (250) NULL,
    [ExamSpecialityID] INT            NULL,
    [StudentGenderID]  INT            NULL,
    [NumberofSections] INT            NULL,
    [NumberofPages]    INT            NULL,
    [TimeForSection]   INT            NULL,
    [Notes]            NTEXT          NULL,
    [IsActive]         BIT            NULL,
    [CreatorID]        INT            NULL,
    [CreatedDate]      DATETIME       NULL,
    [ModifiedByID]     INT            NULL,
    [ModifiedDate]     DATETIME       NULL,
    CONSTRAINT [PK_Exam] PRIMARY KEY CLUSTERED ([ExamID] ASC),
    CONSTRAINT [FK_Exam_ExamSpeciality] FOREIGN KEY ([ExamSpecialityID]) REFERENCES [PPM].[ExamSpeciality] ([ExamSpecialityID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Exam_StudentGender] FOREIGN KEY ([StudentGenderID]) REFERENCES [PPM].[StudentGender] ([StudentGenderID])
);

