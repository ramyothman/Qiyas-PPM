CREATE TABLE [PPM].[RequestWithdrawDetailItem] (
    [RequestWithdrawDetailItemID] INT IDENTITY (1, 1) NOT NULL,
    [RequestWithdrawDetailID]     INT NULL,
    [PackagingTypeID]             INT NULL,
    [PackCount]                   INT NULL,
    CONSTRAINT [PK_RequestWithdrawDetailItem] PRIMARY KEY CLUSTERED ([RequestWithdrawDetailItemID] ASC),
    CONSTRAINT [FK_RequestWithdrawDetailItem_PackagingType] FOREIGN KEY ([PackagingTypeID]) REFERENCES [PPM].[PackagingType] ([PackagingTypeID]),
    CONSTRAINT [FK_RequestWithdrawDetailItem_RequestWithdrawDetail] FOREIGN KEY ([RequestWithdrawDetailID]) REFERENCES [PPM].[RequestWithdrawDetail] ([RequestWithdrawDetailID]) ON DELETE CASCADE ON UPDATE CASCADE
);

