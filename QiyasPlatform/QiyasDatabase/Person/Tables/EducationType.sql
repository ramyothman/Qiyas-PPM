CREATE TABLE [Person].[EducationType] (
    [EducationTypeId]   INT           IDENTITY (1, 1) NOT NULL,
    [EducationTypeName] NVARCHAR (50) NULL,
    CONSTRAINT [PK_EducationType] PRIMARY KEY CLUSTERED ([EducationTypeId] ASC)
);

