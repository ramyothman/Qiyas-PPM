CREATE TABLE [Person].[PersonInternship] (
    [PersonInternshipId] INT            IDENTITY (1, 1) NOT NULL,
    [PersonId]           INT            NULL,
    [Service]            NVARCHAR (50)  NULL,
    [Institution]        NVARCHAR (150) NULL,
    [Evaluation]         NVARCHAR (50)  NULL,
    [StartDate]          DATETIME       NULL,
    [EndDate]            DATETIME       NULL,
    CONSTRAINT [PK_PersonInternship] PRIMARY KEY CLUSTERED ([PersonInternshipId] ASC)
);

