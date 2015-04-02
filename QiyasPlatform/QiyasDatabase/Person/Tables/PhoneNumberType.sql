CREATE TABLE [Person].[PhoneNumberType] (
    [PhoneNumberTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50) NULL,
    [ModifiedDate]      DATETIME      CONSTRAINT [DF_PhoneNumberType_ModifiedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_PhoneNumberType] PRIMARY KEY CLUSTERED ([PhoneNumberTypeId] ASC)
);

