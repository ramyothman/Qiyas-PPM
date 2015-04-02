CREATE TABLE [Person].[PersonPublication] (
    [PersonPublicationId]       INT            IDENTITY (1, 1) NOT NULL,
    [PersonId]                  INT            NULL,
    [PublicationTitle]          NVARCHAR (350) NULL,
    [PublicationAbstract]       NTEXT          NULL,
    [PublicationAttachmentPath] NVARCHAR (350) NULL,
    CONSTRAINT [PK_PersonPublication] PRIMARY KEY CLUSTERED ([PersonPublicationId] ASC)
);

