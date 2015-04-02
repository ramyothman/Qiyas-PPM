CREATE TABLE [Person].[BusinessEntityContact] (
    [ContactTypeId]           INT              NOT NULL,
    [RowGuid]                 UNIQUEIDENTIFIER CONSTRAINT [DF_BusinessEntityContact_RowGuid] DEFAULT (newid()) ROWGUIDCOL NULL,
    [ModifiedDate]            DATETIME         CONSTRAINT [DF_BusinessEntityContact_ModifiedDate] DEFAULT (getdate()) NULL,
    [PersonId]                INT              NOT NULL,
    [BusinessEntityContactId] INT              NOT NULL,
    [ContactId]               INT              NULL,
    CONSTRAINT [PK_BusinessEntityContact_1] PRIMARY KEY CLUSTERED ([BusinessEntityContactId] ASC),
    CONSTRAINT [FK_BusinessEntityContact_ContactType] FOREIGN KEY ([ContactTypeId]) REFERENCES [Person].[ContactType] ([ContactTypeId]),
    CONSTRAINT [FK_BusinessEntityContact_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person].[Person] ([BusinessEntityId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BusinessEntityContact_Person1] FOREIGN KEY ([ContactId]) REFERENCES [Person].[Person] ([BusinessEntityId])
);

