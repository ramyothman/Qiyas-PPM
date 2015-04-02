CREATE TABLE [Person].[PersonLanguageProficiency] (
    [PersonLanguageProficiencyId] INT IDENTITY (1, 1) NOT NULL,
    [PersonId]                    INT NULL,
    [LanguageId]                  INT NULL,
    [CanRead]                     BIT NULL,
    [CanWrite]                    BIT NULL,
    [CanSpeak]                    BIT NULL,
    CONSTRAINT [PK_PersonLanguageProficiency] PRIMARY KEY CLUSTERED ([PersonLanguageProficiencyId] ASC),
    CONSTRAINT [FK_PersonLanguageProficiency_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person].[Person] ([BusinessEntityId]) ON DELETE CASCADE ON UPDATE CASCADE
);

