CREATE TABLE [PPM].[ContainerRequestPack] (
    [ContainerRequestPackID] INT      IDENTITY (1, 1) NOT NULL,
    [ContainerRequestID]     INT      NULL,
    [BookPackItemID]         INT      NULL,
    [CreatedDate]            DATETIME NULL,
    [CreatedBy]              DATETIME NULL,
    CONSTRAINT [PK_ContainerRequestPack] PRIMARY KEY CLUSTERED ([ContainerRequestPackID] ASC),
    CONSTRAINT [FK_ContainerRequestPack_BookPackItem] FOREIGN KEY ([BookPackItemID]) REFERENCES [PPM].[BookPackItem] ([BookPackItemID]),
    CONSTRAINT [FK_ContainerRequestPack_ContainerRequest] FOREIGN KEY ([ContainerRequestID]) REFERENCES [PPM].[ContainerRequest] ([ContainerRequestID]) ON DELETE CASCADE ON UPDATE CASCADE
);

