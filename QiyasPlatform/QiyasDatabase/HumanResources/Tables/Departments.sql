CREATE TABLE [HumanResources].[Departments] (
    [DepartmentId]          INT           IDENTITY (1, 1) NOT NULL,
    [OrganizationId]        INT           NULL,
    [DepartmentName]        NVARCHAR (50) NULL,
    [DepartmentDescription] NVARCHAR (50) NULL,
    [Phone1]                NVARCHAR (20) NULL,
    [Phone2]                NVARCHAR (20) NULL,
    [Fax1]                  NVARCHAR (20) NULL,
    [Fax2]                  NVARCHAR (20) NULL,
    [AddressLine1]          NVARCHAR (60) NULL,
    [AddressLine2]          NVARCHAR (60) NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED ([DepartmentId] ASC),
    CONSTRAINT [FK_Departments_Organizations] FOREIGN KEY ([OrganizationId]) REFERENCES [HumanResources].[Organizations] ([OrganizationId]) ON DELETE CASCADE ON UPDATE CASCADE
);

