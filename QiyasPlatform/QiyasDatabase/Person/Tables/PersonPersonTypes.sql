CREATE TABLE [Person].[PersonPersonTypes] (
    [PersonPersonTypesId] INT              IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (50)    NOT NULL,
    [RowGuid]             UNIQUEIDENTIFIER CONSTRAINT [DF_PersonType_RowGuid] DEFAULT (newid()) NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_PersonType_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PersonType] PRIMARY KEY CLUSTERED ([PersonPersonTypesId] ASC)
);

