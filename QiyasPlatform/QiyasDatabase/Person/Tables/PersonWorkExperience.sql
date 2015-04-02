CREATE TABLE [Person].[PersonWorkExperience] (
    [PersonWorkExperienceId] INT            IDENTITY (1, 1) NOT NULL,
    [PersonId]               INT            NULL,
    [Employer]               NVARCHAR (150) NULL,
    [PositionHeld]           NVARCHAR (150) NULL,
    [Responsibilities]       NTEXT          NULL,
    [StartDate]              DATETIME       NULL,
    [EndDate]                DATETIME       NULL,
    CONSTRAINT [PK_PersonWorkExperience] PRIMARY KEY CLUSTERED ([PersonWorkExperienceId] ASC)
);

