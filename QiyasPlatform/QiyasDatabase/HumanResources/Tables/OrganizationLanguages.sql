CREATE TABLE [HumanResources].[OrganizationLanguages] (
    [OrganizationLanguagesId] INT           IDENTITY (1, 1) NOT NULL,
    [OrganizationId]          INT           NULL,
    [LanguageId]              INT           NULL,
    [OrganizationName]        NVARCHAR (50) NULL,
    [OrganizationDescription] NVARCHAR (50) NULL,
    [AddressLine1]            NVARCHAR (60) NULL,
    [AddressLine2]            NVARCHAR (60) NULL,
    CONSTRAINT [PK_OrganizationLanguages] PRIMARY KEY CLUSTERED ([OrganizationLanguagesId] ASC),
    CONSTRAINT [FK_OrganizationLanguages_Organizations] FOREIGN KEY ([OrganizationId]) REFERENCES [HumanResources].[Organizations] ([OrganizationId]) ON DELETE CASCADE ON UPDATE CASCADE
);

