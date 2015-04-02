CREATE TABLE [Person].[PersonLanguages] (
    [PersonLanguageId] INT            IDENTITY (1, 1) NOT NULL,
    [PersonId]         INT            NULL,
    [LanguageId]       INT            NULL,
    [Title]            NVARCHAR (8)   NULL,
    [FirstName]        NVARCHAR (50)  NULL,
    [MiddleName]       NVARCHAR (50)  NULL,
    [LastName]         NVARCHAR (50)  NULL,
    [Suffix]           NVARCHAR (60)  NULL,
    [DisplayName]      NVARCHAR (250) NULL,
    CONSTRAINT [PK_PersonLanguages] PRIMARY KEY CLUSTERED ([PersonLanguageId] ASC),
    CONSTRAINT [FK_PersonLanguages_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person].[Person] ([BusinessEntityId]) ON DELETE CASCADE ON UPDATE CASCADE
);

