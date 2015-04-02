CREATE TABLE [Person].[PersonEducation] (
    [PersonEducationId] INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]          INT           NULL,
    [InstitutionName]   NVARCHAR (50) NULL,
    [Degree]            NVARCHAR (50) NULL,
    [StartDate]         DATETIME      NULL,
    [GraduationDate]    DATETIME      NULL,
    [FinalGrade]        NVARCHAR (50) NULL,
    CONSTRAINT [PK_PersonEducation] PRIMARY KEY CLUSTERED ([PersonEducationId] ASC)
);

