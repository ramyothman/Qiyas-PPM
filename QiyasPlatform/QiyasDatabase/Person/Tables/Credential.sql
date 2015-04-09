﻿CREATE TABLE [Person].[Credential] (
    [BusinessEntityId]            INT              NOT NULL,
    [Username]                    NVARCHAR (60)    NOT NULL,
    [PasswordHash]                NVARCHAR (128)   NOT NULL,
    [PasswordSalt]                NVARCHAR (50)    NULL,
    [ActivationCode]              NVARCHAR (128)   NULL,
    [IsActivated]                 BIT              NULL,
    [IsActive]                    BIT              NOT NULL,
    [RowGuid]                     UNIQUEIDENTIFIER CONSTRAINT [DF_Credential_RowGuid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [Email]                       NVARCHAR (100)   NULL,
    [EmailConfirmationCode]       NVARCHAR (150)   NULL,
    [EmailConfirmed]              BIT              NULL,
    [PhoneNumber]                 NVARCHAR (20)    NULL,
    [PhoneNumberConfirmationCode] NVARCHAR (150)   NULL,
    [PhoneNumberConfirmed]        BIT              NULL,
    [LockoutEnabled]              BIT              NULL,
    [TwoFactorEnabled]            BIT              NULL,
    [LockoutEndDateUtc]           DATETIME         NULL,
    [AccessFailedCount]           INT              NULL,
    [ModifiedDate]                DATETIME         CONSTRAINT [DF_Credential_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreatedDate]                 DATETIME         NULL,
    [CreatedBy]                   INT              NULL,
    [ModifiedBy]                  INT              NULL,
    CONSTRAINT [PK_Credential] PRIMARY KEY CLUSTERED ([BusinessEntityId] ASC),
    CONSTRAINT [FK_Credential_Person] FOREIGN KEY ([BusinessEntityId]) REFERENCES [Person].[Person] ([BusinessEntityId]) ON DELETE CASCADE ON UPDATE CASCADE
);
