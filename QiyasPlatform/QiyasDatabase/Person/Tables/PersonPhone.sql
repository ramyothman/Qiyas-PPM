CREATE TABLE [Person].[PersonPhone] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [BusinessEntityId]      INT           NOT NULL,
    [PhoneNumber]           NVARCHAR (25) NOT NULL,
    [PhoneNumberTypeId]     INT           NOT NULL,
    [ModifiedDate]          DATETIME      CONSTRAINT [DF_PersonPhone_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    [PhoneVerified]         BIT           NULL,
    [PhoneVerificationCode] NVARCHAR (50) NULL,
    CONSTRAINT [PK_PersonPhone] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonPhone_Person] FOREIGN KEY ([BusinessEntityId]) REFERENCES [Person].[Person] ([BusinessEntityId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PersonPhone_PhoneNumberType] FOREIGN KEY ([PhoneNumberTypeId]) REFERENCES [Person].[PhoneNumberType] ([PhoneNumberTypeId])
);

