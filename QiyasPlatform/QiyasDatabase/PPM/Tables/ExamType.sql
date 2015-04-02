CREATE TABLE [PPM].[ExamType] (
    [ExaminationTypeID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50) NULL,
    [IsActive]          BIT           NULL,
    [CreatorID]         INT           NULL,
    [CreatedDate]       DATETIME      NULL,
    [ModifiedByID]      INT           NULL,
    [ModifiedDate]      DATETIME      NULL,
    CONSTRAINT [PK_ExaminationType] PRIMARY KEY CLUSTERED ([ExaminationTypeID] ASC)
);

