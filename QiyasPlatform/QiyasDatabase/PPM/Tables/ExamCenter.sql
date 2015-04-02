CREATE TABLE [PPM].[ExamCenter] (
    [ExaminationCenterID] INT            IDENTITY (1, 1) NOT NULL,
    [CenterCode]          NVARCHAR (12)  NULL,
    [Name]                NVARCHAR (150) NULL,
    [CityID]              INT            NULL,
    [StudentGenderID]     INT            NULL,
    [IsActive]            BIT            NULL,
    [CreatorID]           INT            NULL,
    [CreatedDate]         DATETIME       NULL,
    [ModifiedByID]        INT            NULL,
    [ModifiedDate]        DATETIME       NULL,
    CONSTRAINT [PK_ExaminationCenter] PRIMARY KEY CLUSTERED ([ExaminationCenterID] ASC),
    CONSTRAINT [FK_ExamCenter_City] FOREIGN KEY ([CityID]) REFERENCES [Person].[City] ([CityID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ExamCenter_StudentGender] FOREIGN KEY ([StudentGenderID]) REFERENCES [PPM].[StudentGender] ([StudentGenderID])
);

