CREATE TABLE [PPM].[RequestPreparationStatus] (
    [RequestPreparationStatusID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]                       NVARCHAR (50) NULL,
    CONSTRAINT [PK_RequestPreparationStatus] PRIMARY KEY CLUSTERED ([RequestPreparationStatusID] ASC)
);

