CREATE TABLE [HumanResources].[DepartmentLanguages] (
    [DepartmentLanguagesId] INT           IDENTITY (1, 1) NOT NULL,
    [DepartmentId]          INT           NULL,
    [LanguageId]            INT           NULL,
    [DepartmentName]        NVARCHAR (50) NULL,
    [DepartmentDescription] NVARCHAR (50) NULL,
    [AddressLine1]          NVARCHAR (60) NULL,
    [AddressLine2]          NVARCHAR (60) NULL,
    CONSTRAINT [PK_DepartmentLanguages] PRIMARY KEY CLUSTERED ([DepartmentLanguagesId] ASC),
    CONSTRAINT [FK_DepartmentLanguages_Departments] FOREIGN KEY ([DepartmentId]) REFERENCES [HumanResources].[Departments] ([DepartmentId]) ON DELETE CASCADE ON UPDATE CASCADE
);

