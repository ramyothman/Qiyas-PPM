CREATE TABLE [PPM].[ShippingBag] (
    [ShippingBagID]             INT           NOT NULL,
    [ExamCenterRequiredExamsID] INT           NULL,
    [ShippingBagCode]           NVARCHAR (20) NULL,
    [CreatedBy]                 INT           NULL,
    [ModifiedBy]                INT           NULL,
    [CreatedDate]               DATETIME      NULL,
    [ModifiedDate]              DATETIME      NULL,
    CONSTRAINT [PK_ShippingBag] PRIMARY KEY CLUSTERED ([ShippingBagID] ASC),
    CONSTRAINT [FK_ShippingBag_ExamCenterRequiredExams] FOREIGN KEY ([ExamCenterRequiredExamsID]) REFERENCES [PPM].[ExamCenterRequiredExams] ([ExamCenterRequiredExamsID]) ON DELETE CASCADE ON UPDATE CASCADE
);

