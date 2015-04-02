CREATE TABLE [Person].[ContactType] (
    [ContactTypeId] INT           NOT NULL,
    [Name]          NVARCHAR (50) NULL,
    [ModifiedDate]  DATETIME      CONSTRAINT [DF_ContactType_ModifiedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_ContactType] PRIMARY KEY CLUSTERED ([ContactTypeId] ASC)
);

