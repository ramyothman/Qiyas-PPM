CREATE TABLE [HumanResources].[Divisions] (
    [DivisionId]          INT           IDENTITY (1, 1) NOT NULL,
    [DepartmentId]        INT           NULL,
    [DivisionName]        NVARCHAR (50) NULL,
    [DivisionDescription] NVARCHAR (50) NULL,
    [Phone1]              NVARCHAR (20) NULL,
    [Phone2]              NVARCHAR (20) NULL,
    [Fax1]                NVARCHAR (20) NULL,
    [Fax2]                NVARCHAR (20) NULL,
    CONSTRAINT [PK_Divisions] PRIMARY KEY CLUSTERED ([DivisionId] ASC),
    CONSTRAINT [FK_Divisions_Departments] FOREIGN KEY ([DepartmentId]) REFERENCES [HumanResources].[Departments] ([DepartmentId]) ON DELETE CASCADE ON UPDATE CASCADE
);

