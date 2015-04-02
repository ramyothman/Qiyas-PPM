CREATE TABLE [Person].[PersonType] (
    [PersonTypeId]        INT      IDENTITY (1, 1) NOT NULL,
    [BusinessEntityId]    INT      NULL,
    [PersonPersonTypesId] INT      NULL,
    [ModifiedDate]        DATETIME CONSTRAINT [DF_PersonTypes_ModifiedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_PersonTypes] PRIMARY KEY CLUSTERED ([PersonTypeId] ASC),
    CONSTRAINT [FK_PersonTypes_Person] FOREIGN KEY ([BusinessEntityId]) REFERENCES [Person].[Person] ([BusinessEntityId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PersonTypes_PersonType] FOREIGN KEY ([PersonPersonTypesId]) REFERENCES [Person].[PersonPersonTypes] ([PersonPersonTypesId])
);

