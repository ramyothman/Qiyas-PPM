CREATE TABLE [Person].[Person] (
    [BusinessEntityId] INT              NOT NULL,
    [FirstName]        NVARCHAR (50)    NULL,
    [MiddleName]       NVARCHAR (50)    NULL,
    [LastName]         NVARCHAR (50)    NULL,
    [DisplayName]      NVARCHAR (50)    NULL,
    [Title]            NVARCHAR (50)    NULL,
    [NameStyle]        BIT              NULL,
    [EmailPromotion]   INT              NULL,
    [RowGuid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Person_RowGuid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Person_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreatedDate]      DATETIME         CONSTRAINT [DF_Person_CreatedDate] DEFAULT (getdate()) NULL,
    [NationalityCode]  CHAR (3)         NULL,
    [Gender]           CHAR (1)         NULL,
    [DateofBirth]      DATETIME         NULL,
    [PersonImage]      NVARCHAR (250)   NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([BusinessEntityId] ASC),
    CONSTRAINT [FK_Person_BusinessEntity] FOREIGN KEY ([BusinessEntityId]) REFERENCES [Person].[BusinessEntity] ([BusinessEntityId]) ON DELETE CASCADE ON UPDATE CASCADE
);

