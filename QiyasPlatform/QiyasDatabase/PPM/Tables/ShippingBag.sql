CREATE TABLE [PPM].[ShippingBag] (
    [ShippingBagID]             INT           IDENTITY (1, 1) NOT NULL,
    [ExamCenterRequiredExamsID] INT           NULL,
    [ShippingBagCode]           NVARCHAR (20) NULL,
    [ShippingBagSerial]         INT           NULL,
    [BookCount]                 INT           NULL,
    [PackCount]                 INT           NULL,
    [CreatedBy]                 INT           NULL,
    [ModifiedBy]                INT           NULL,
    [CreatedDate]               DATETIME      NULL,
    [ModifiedDate]              DATETIME      NULL,
    CONSTRAINT [PK_ShippingBag] PRIMARY KEY CLUSTERED ([ShippingBagID] ASC),
    CONSTRAINT [FK_ShippingBag_ExamCenterRequiredExams] FOREIGN KEY ([ExamCenterRequiredExamsID]) REFERENCES [PPM].[ExamCenterRequiredExams] ([ExamCenterRequiredExamsID]) ON DELETE CASCADE ON UPDATE CASCADE
);



