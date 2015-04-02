CREATE TABLE [Person].[PersonDependent] (
    [PersonDependentId] INT            IDENTITY (1, 1) NOT NULL,
    [PersonId]          INT            NULL,
    [DependentName]     NVARCHAR (150) NULL,
    [Gender]            NCHAR (1)      NULL,
    [Age]               INT            NULL,
    [DateOfBirth]       DATETIME       NULL,
    [Relation]          NVARCHAR (50)  NULL,
    [DateModified]      DATETIME       NULL,
    CONSTRAINT [PK_PersonDependent] PRIMARY KEY CLUSTERED ([PersonDependentId] ASC)
);

