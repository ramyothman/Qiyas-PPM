CREATE TABLE [PPM].[PackagingType] (
    [PackagingTypeID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (50) NULL,
    [ExamModelCount]  INT           NULL,
    [BooksPerPackage] INT           NULL,
    [Total]           INT           NULL,
    [IsActive]        BIT           NULL,
    [CreatorID]       INT           NULL,
    [CreatedDate]     DATETIME      NULL,
    [ModifiedByID]    INT           NULL,
    [ModifiedDate]    DATETIME      NULL,
    CONSTRAINT [PK_PackagingType] PRIMARY KEY CLUSTERED ([PackagingTypeID] ASC)
);

