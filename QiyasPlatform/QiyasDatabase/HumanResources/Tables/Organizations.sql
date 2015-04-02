CREATE TABLE [HumanResources].[Organizations] (
    [OrganizationId]          INT           IDENTITY (1, 1) NOT NULL,
    [OrganizationName]        NVARCHAR (50) NULL,
    [OrganizationDescription] NVARCHAR (50) NULL,
    [Phone1]                  NVARCHAR (20) NULL,
    [Phone2]                  NVARCHAR (20) NULL,
    [Phone3]                  NVARCHAR (20) NULL,
    [Fax1]                    NVARCHAR (20) NULL,
    [Fax2]                    NVARCHAR (20) NULL,
    [AddressLine1]            NVARCHAR (60) NULL,
    [AddressLine2]            NVARCHAR (60) NULL,
    CONSTRAINT [PK_Organizations] PRIMARY KEY CLUSTERED ([OrganizationId] ASC)
);

