CREATE TABLE [PPM].[OperationStatus] (
    [OperationStatusID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50) NULL,
    CONSTRAINT [PK_OperationStatus] PRIMARY KEY CLUSTERED ([OperationStatusID] ASC)
);

