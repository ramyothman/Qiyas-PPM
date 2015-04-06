/****** Object:  Schema [ContentManagement]    Script Date: 4/6/2015 8:16:19 AM ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'ContentManagement')
EXEC sys.sp_executesql N'CREATE SCHEMA [ContentManagement]'

GO
/****** Object:  Schema [HumanResources]    Script Date: 4/6/2015 8:16:19 AM ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'HumanResources')
EXEC sys.sp_executesql N'CREATE SCHEMA [HumanResources]'

GO
/****** Object:  Schema [Organization]    Script Date: 4/6/2015 8:16:19 AM ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'Organization')
EXEC sys.sp_executesql N'CREATE SCHEMA [Organization]'

GO
/****** Object:  Schema [Person]    Script Date: 4/6/2015 8:16:19 AM ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'Person')
EXEC sys.sp_executesql N'CREATE SCHEMA [Person]'

GO
/****** Object:  Schema [PPM]    Script Date: 4/6/2015 8:16:19 AM ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'PPM')
EXEC sys.sp_executesql N'CREATE SCHEMA [PPM]'

GO
/****** Object:  Schema [RoleSecurity]    Script Date: 4/6/2015 8:16:19 AM ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'RoleSecurity')
EXEC sys.sp_executesql N'CREATE SCHEMA [RoleSecurity]'

GO
/****** Object:  Table [ContentManagement].[ContentEntity]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ContentManagement].[ContentEntity]') AND type in (N'U'))
BEGIN
CREATE TABLE [ContentManagement].[ContentEntity](
	[ContentEntityId] [int] IDENTITY(1,1) NOT NULL,
	[ContentEntityType] [char](2) NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ContentEntity] PRIMARY KEY CLUSTERED 
(
	[ContentEntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [ContentManagement].[SystemFolder]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ContentManagement].[SystemFolder]') AND type in (N'U'))
BEGIN
CREATE TABLE [ContentManagement].[SystemFolder](
	[SystemFolderId] [int] NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Path] [nvarchar](500) NULL,
 CONSTRAINT [PK_SystemFolder] PRIMARY KEY CLUSTERED 
(
	[SystemFolderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [ContentManagement].[SystemPage]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ContentManagement].[SystemPage]') AND type in (N'U'))
BEGIN
CREATE TABLE [ContentManagement].[SystemPage](
	[SystemPageId] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Path] [nvarchar](150) NULL,
	[SecurityAccessTypeId] [int] NULL,
	[IsActive] [bit] NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NULL,
	[ModifiedDate] [datetime] NULL,
	[SystemFolderId] [int] NULL,
 CONSTRAINT [PK_SystemPage] PRIMARY KEY CLUSTERED 
(
	[SystemPageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [HumanResources].[DepartmentLanguages]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[DepartmentLanguages]') AND type in (N'U'))
BEGIN
CREATE TABLE [HumanResources].[DepartmentLanguages](
	[DepartmentLanguagesId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NULL,
	[LanguageId] [int] NULL,
	[DepartmentName] [nvarchar](50) NULL,
	[DepartmentDescription] [nvarchar](50) NULL,
	[AddressLine1] [nvarchar](60) NULL,
	[AddressLine2] [nvarchar](60) NULL,
 CONSTRAINT [PK_DepartmentLanguages] PRIMARY KEY CLUSTERED 
(
	[DepartmentLanguagesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [HumanResources].[Departments]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[Departments]') AND type in (N'U'))
BEGIN
CREATE TABLE [HumanResources].[Departments](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NULL,
	[DepartmentName] [nvarchar](50) NULL,
	[DepartmentDescription] [nvarchar](50) NULL,
	[Phone1] [nvarchar](20) NULL,
	[Phone2] [nvarchar](20) NULL,
	[Fax1] [nvarchar](20) NULL,
	[Fax2] [nvarchar](20) NULL,
	[AddressLine1] [nvarchar](60) NULL,
	[AddressLine2] [nvarchar](60) NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [HumanResources].[Divisions]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[Divisions]') AND type in (N'U'))
BEGIN
CREATE TABLE [HumanResources].[Divisions](
	[DivisionId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NULL,
	[DivisionName] [nvarchar](50) NULL,
	[DivisionDescription] [nvarchar](50) NULL,
	[Phone1] [nvarchar](20) NULL,
	[Phone2] [nvarchar](20) NULL,
	[Fax1] [nvarchar](20) NULL,
	[Fax2] [nvarchar](20) NULL,
 CONSTRAINT [PK_Divisions] PRIMARY KEY CLUSTERED 
(
	[DivisionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [HumanResources].[Employees]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[Employees]') AND type in (N'U'))
BEGIN
CREATE TABLE [HumanResources].[Employees](
	[EmployeeId] [int] NOT NULL,
	[EmployeeNumber] [nvarchar](50) NULL,
	[DepartmentId] [int] NULL,
	[DivisionId] [int] NULL,
	[FormalSiteUrl] [nvarchar](250) NULL,
	[NationalIdNumber] [nvarchar](50) NULL,
	[NationalIdType] [nchar](1) NULL,
	[ManagerId] [int] NULL,
	[BirthDate] [datetime] NULL,
	[MaritalStatus] [nchar](1) NULL,
	[Gender] [nchar](1) NULL,
	[HireDate] [datetime] NULL,
	[SalariedFlag] [bit] NULL,
	[VacationHours] [smallint] NULL,
	[SickLeaveHours] [smallint] NULL,
	[CurrentFlag] [bit] NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NULL,
	[ModifiedDate] [datetime] NULL,
	[Position] [nvarchar](150) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [HumanResources].[OrganizationLanguages]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[OrganizationLanguages]') AND type in (N'U'))
BEGIN
CREATE TABLE [HumanResources].[OrganizationLanguages](
	[OrganizationLanguagesId] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NULL,
	[LanguageId] [int] NULL,
	[OrganizationName] [nvarchar](50) NULL,
	[OrganizationDescription] [nvarchar](50) NULL,
	[AddressLine1] [nvarchar](60) NULL,
	[AddressLine2] [nvarchar](60) NULL,
 CONSTRAINT [PK_OrganizationLanguages] PRIMARY KEY CLUSTERED 
(
	[OrganizationLanguagesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [HumanResources].[Organizations]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[Organizations]') AND type in (N'U'))
BEGIN
CREATE TABLE [HumanResources].[Organizations](
	[OrganizationId] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationName] [nvarchar](50) NULL,
	[OrganizationDescription] [nvarchar](50) NULL,
	[Phone1] [nvarchar](20) NULL,
	[Phone2] [nvarchar](20) NULL,
	[Phone3] [nvarchar](20) NULL,
	[Fax1] [nvarchar](20) NULL,
	[Fax2] [nvarchar](20) NULL,
	[AddressLine1] [nvarchar](60) NULL,
	[AddressLine2] [nvarchar](60) NULL,
 CONSTRAINT [PK_Organizations] PRIMARY KEY CLUSTERED 
(
	[OrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[Address]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[Address]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[Address](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[AddressLine1] [nvarchar](60) NULL,
	[AddressLine2] [nvarchar](60) NULL,
	[AddressLine3] [nvarchar](60) NULL,
	[CountryRegionCode] [char](3) NOT NULL,
	[City] [nvarchar](30) NULL,
	[StateProvinceId] [int] NULL,
	[PostalCode] [nvarchar](15) NULL,
	[ZipCode] [nvarchar](15) NULL,
	[SpatialLocation] [nvarchar](60) NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Person].[AddressType]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[AddressType]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[AddressType](
	[AddressTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_AddressType] PRIMARY KEY CLUSTERED 
(
	[AddressTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[BusinessEntity]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[BusinessEntity]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[BusinessEntity](
	[BusinessEntityId] [int] IDENTITY(1,1) NOT NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_BusinessEntity] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[BusinessEntityAddress]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[BusinessEntityAddress](
	[BusinessEntityAddressId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessEntityId] [int] NOT NULL,
	[AddressId] [int] NOT NULL,
	[AddressTypeId] [int] NOT NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_BusinessEntityAddress] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[BusinessEntityContact]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[BusinessEntityContact](
	[ContactTypeId] [int] NOT NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NULL,
	[ModifiedDate] [datetime] NULL,
	[PersonId] [int] NOT NULL,
	[BusinessEntityContactId] [int] NOT NULL,
	[ContactId] [int] NULL,
 CONSTRAINT [PK_BusinessEntityContact_1] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[City]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[City]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[City](
	[CityID] [int] IDENTITY(1,1) NOT NULL,
	[StateRegionID] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[CityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[ContactType]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[ContactType]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[ContactType](
	[ContactTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ContactType] PRIMARY KEY CLUSTERED 
(
	[ContactTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[CountryRegion]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[CountryRegion]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[CountryRegion](
	[CountryRegionCode] [char](3) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CountryRegion] PRIMARY KEY CLUSTERED 
(
	[CountryRegionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Person].[Credential]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[Credential]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[Credential](
	[BusinessEntityId] [int] NOT NULL,
	[Username] [nvarchar](60) NOT NULL,
	[PasswordHash] [nvarchar](128) NOT NULL,
	[PasswordSalt] [nvarchar](50) NULL,
	[ActivationCode] [nvarchar](128) NULL,
	[IsActivated] [bit] NULL,
	[IsActive] [bit] NOT NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Email] [nvarchar](100) NULL,
	[EmailConfirmationCode] [nvarchar](150) NULL,
	[EmailConfirmed] [bit] NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[PhoneNumberConfirmationCode] [nvarchar](150) NULL,
	[PhoneNumberConfirmed] [bit] NULL,
	[LockoutEnabled] [bit] NULL,
	[TwoFactorEnabled] [bit] NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[AccessFailedCount] [int] NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Credential] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[EducationType]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[EducationType]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[EducationType](
	[EducationTypeId] [int] IDENTITY(1,1) NOT NULL,
	[EducationTypeName] [nvarchar](50) NULL,
 CONSTRAINT [PK_EducationType] PRIMARY KEY CLUSTERED 
(
	[EducationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[EmailAddress]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[EmailAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[EmailAddress](
	[EmailAddressId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessEntityId] [int] NOT NULL,
	[EmailAddressTypeId] [int] NOT NULL,
	[Email] [nvarchar](60) NOT NULL,
	[EmailVerified] [bit] NULL,
	[EmailVerificationHash] [nvarchar](128) NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_EmailAddressId] PRIMARY KEY CLUSTERED 
(
	[EmailAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[EmailAddressType]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[EmailAddressType]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[EmailAddressType](
	[EmailAddressTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_EmailAddressType] PRIMARY KEY CLUSTERED 
(
	[EmailAddressTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[Person]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[Person]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[Person](
	[BusinessEntityId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[DisplayName] [nvarchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[NameStyle] [bit] NULL,
	[EmailPromotion] [int] NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[NationalityCode] [char](3) NULL,
	[Gender] [char](1) NULL,
	[DateofBirth] [datetime] NULL,
	[PersonImage] [nvarchar](250) NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Person].[PersonDependent]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[PersonDependent]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[PersonDependent](
	[PersonDependentId] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NULL,
	[DependentName] [nvarchar](150) NULL,
	[Gender] [nchar](1) NULL,
	[Age] [int] NULL,
	[DateOfBirth] [datetime] NULL,
	[Relation] [nvarchar](50) NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_PersonDependent] PRIMARY KEY CLUSTERED 
(
	[PersonDependentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[PersonEducation]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[PersonEducation]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[PersonEducation](
	[PersonEducationId] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NULL,
	[InstitutionName] [nvarchar](50) NULL,
	[Degree] [nvarchar](50) NULL,
	[StartDate] [datetime] NULL,
	[GraduationDate] [datetime] NULL,
	[FinalGrade] [nvarchar](50) NULL,
 CONSTRAINT [PK_PersonEducation] PRIMARY KEY CLUSTERED 
(
	[PersonEducationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[PersonExtra]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[PersonExtra]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[PersonExtra](
	[PersonExtraId] [int] NOT NULL,
	[NationalIdType] [nchar](1) NULL,
	[NationalId] [nvarchar](20) NULL,
	[Gender] [nchar](1) NULL,
	[Religion] [nvarchar](50) NULL,
	[BirthDate] [datetime] NULL,
	[BirthPlace] [nvarchar](50) NULL,
	[MaritalStatus] [nvarchar](50) NULL,
	[SpauseName] [nvarchar](50) NULL,
	[FatherGuardianName] [nvarchar](150) NULL,
	[FatherGuardianAddress] [nvarchar](250) NULL,
	[FatherGuardianContactNumber] [nvarchar](50) NULL,
	[EmergencyContactName] [nvarchar](150) NULL,
	[EmergencyContactAddress] [nvarchar](250) NULL,
	[EmergencyContactNumber] [nvarchar](50) NULL,
	[EmergencyContactEmail] [nvarchar](150) NULL,
	[SponsorId] [int] NULL,
	[SponsorStartDate] [datetime] NULL,
	[SponsorEndDate] [datetime] NULL,
	[SponsorCategoryId] [int] NULL,
	[IsGraduateTransfer] [bit] NULL,
	[ReasonForSeekingTransfer] [nvarchar](250) NULL,
	[LevelRequired] [nvarchar](50) NULL,
	[OtherInformation] [ntext] NULL,
 CONSTRAINT [PK_PersonExtra] PRIMARY KEY CLUSTERED 
(
	[PersonExtraId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[PersonInternship]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[PersonInternship]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[PersonInternship](
	[PersonInternshipId] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NULL,
	[Service] [nvarchar](50) NULL,
	[Institution] [nvarchar](150) NULL,
	[Evaluation] [nvarchar](50) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_PersonInternship] PRIMARY KEY CLUSTERED 
(
	[PersonInternshipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[PersonLanguageProficiency]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[PersonLanguageProficiency]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[PersonLanguageProficiency](
	[PersonLanguageProficiencyId] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NULL,
	[LanguageId] [int] NULL,
	[CanRead] [bit] NULL,
	[CanWrite] [bit] NULL,
	[CanSpeak] [bit] NULL,
 CONSTRAINT [PK_PersonLanguageProficiency] PRIMARY KEY CLUSTERED 
(
	[PersonLanguageProficiencyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[PersonLanguages]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[PersonLanguages]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[PersonLanguages](
	[PersonLanguageId] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NULL,
	[LanguageId] [int] NULL,
	[Title] [nvarchar](8) NULL,
	[FirstName] [nvarchar](50) NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Suffix] [nvarchar](60) NULL,
	[DisplayName] [nvarchar](250) NULL,
 CONSTRAINT [PK_PersonLanguages] PRIMARY KEY CLUSTERED 
(
	[PersonLanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[PersonPersonTypes]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[PersonPersonTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[PersonPersonTypes](
	[PersonPersonTypesId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[RowGuid] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PersonType] PRIMARY KEY CLUSTERED 
(
	[PersonPersonTypesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[PersonPhone]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[PersonPhone]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[PersonPhone](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BusinessEntityId] [int] NOT NULL,
	[PhoneNumber] [nvarchar](25) NOT NULL,
	[PhoneNumberTypeId] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[PhoneVerified] [bit] NULL,
	[PhoneVerificationCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_PersonPhone] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[PersonPublication]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[PersonPublication]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[PersonPublication](
	[PersonPublicationId] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NULL,
	[PublicationTitle] [nvarchar](350) NULL,
	[PublicationAbstract] [ntext] NULL,
	[PublicationAttachmentPath] [nvarchar](350) NULL,
 CONSTRAINT [PK_PersonPublication] PRIMARY KEY CLUSTERED 
(
	[PersonPublicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[PersonType]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[PersonType]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[PersonType](
	[PersonTypeId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessEntityId] [int] NULL,
	[PersonPersonTypesId] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_PersonTypes] PRIMARY KEY CLUSTERED 
(
	[PersonTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[PersonWorkExperience]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[PersonWorkExperience]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[PersonWorkExperience](
	[PersonWorkExperienceId] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NULL,
	[Employer] [nvarchar](150) NULL,
	[PositionHeld] [nvarchar](150) NULL,
	[Responsibilities] [ntext] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_PersonWorkExperience] PRIMARY KEY CLUSTERED 
(
	[PersonWorkExperienceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[PhoneNumberType]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[PhoneNumberType]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[PhoneNumberType](
	[PhoneNumberTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_PhoneNumberType] PRIMARY KEY CLUSTERED 
(
	[PhoneNumberTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Person].[StateProvince]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[StateProvince]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[StateProvince](
	[StateProvinceId] [int] IDENTITY(1,1) NOT NULL,
	[StateProvinceCode] [nchar](3) NULL,
	[CountryRegionCode] [char](3) NOT NULL,
	[IsOnlyStateProvince] [bit] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[RowGuid] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StateProvince] PRIMARY KEY CLUSTERED 
(
	[StateProvinceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [PPM].[BookPackingOperation]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PPM].[BookPackingOperation]') AND type in (N'U'))
BEGIN
CREATE TABLE [PPM].[BookPackingOperation](
	[BookPackingOperationID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreatorID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedByID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_BookPackingOperation] PRIMARY KEY CLUSTERED 
(
	[BookPackingOperationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [PPM].[BookPrintingOperation]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PPM].[BookPrintingOperation]') AND type in (N'U'))
BEGIN
CREATE TABLE [PPM].[BookPrintingOperation](
	[BookPrintingOperationID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ExamID] [int] NULL,
	[PrintsForOneModel] [int] NULL,
	[ExamsNeededForA3] [int] NULL,
	[ExamsNeededForA4] [int] NULL,
	[ExamsNeededForCD] [int] NULL,
	[OperationStatusID] [int] NULL,
	[CreatorID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedByID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_BookPrintingOperation] PRIMARY KEY CLUSTERED 
(
	[BookPrintingOperationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [PPM].[Exam]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PPM].[Exam]') AND type in (N'U'))
BEGIN
CREATE TABLE [PPM].[Exam](
	[ExamID] [int] IDENTITY(1,1) NOT NULL,
	[ExamCode] [nvarchar](12) NULL,
	[Name] [nvarchar](250) NULL,
	[ExamSpecialityID] [int] NULL,
	[StudentGenderID] [int] NULL,
	[NumberofSections] [int] NULL,
	[NumberofPages] [int] NULL,
	[TimeForSection] [int] NULL,
	[Notes] [ntext] NULL,
	[IsActive] [bit] NULL,
	[CreatorID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedByID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Exam] PRIMARY KEY CLUSTERED 
(
	[ExamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [PPM].[ExamCenter]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PPM].[ExamCenter]') AND type in (N'U'))
BEGIN
CREATE TABLE [PPM].[ExamCenter](
	[ExaminationCenterID] [int] IDENTITY(1,1) NOT NULL,
	[CenterCode] [nvarchar](12) NULL,
	[Name] [nvarchar](150) NULL,
	[CityID] [int] NULL,
	[StudentGenderID] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatorID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedByID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ExaminationCenter] PRIMARY KEY CLUSTERED 
(
	[ExaminationCenterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [PPM].[ExamModel]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PPM].[ExamModel]') AND type in (N'U'))
BEGIN
CREATE TABLE [PPM].[ExamModel](
	[ExamModelID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatorID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedByID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ExamModel] PRIMARY KEY CLUSTERED 
(
	[ExamModelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [PPM].[ExamPeriod]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PPM].[ExamPeriod]') AND type in (N'U'))
BEGIN
CREATE TABLE [PPM].[ExamPeriod](
	[ExamPeriodID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ExamTypeID] [int] NULL,
	[ExamYear] [int] NULL,
	[StudentGenderID] [int] NULL,
	[StartDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[CreatorID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedByID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_ExamPeriod] PRIMARY KEY CLUSTERED 
(
	[ExamPeriodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [PPM].[ExamSpeciality]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PPM].[ExamSpeciality]') AND type in (N'U'))
BEGIN
CREATE TABLE [PPM].[ExamSpeciality](
	[ExamSpecialityID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ExamTypeID] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatorID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedByID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ExamSpeciality] PRIMARY KEY CLUSTERED 
(
	[ExamSpecialityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [PPM].[ExamType]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PPM].[ExamType]') AND type in (N'U'))
BEGIN
CREATE TABLE [PPM].[ExamType](
	[ExaminationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatorID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedByID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ExaminationType] PRIMARY KEY CLUSTERED 
(
	[ExaminationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [PPM].[OperationStatus]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PPM].[OperationStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [PPM].[OperationStatus](
	[OperationStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_OperationStatus] PRIMARY KEY CLUSTERED 
(
	[OperationStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [PPM].[PackageWeight]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PPM].[PackageWeight]') AND type in (N'U'))
BEGIN
CREATE TABLE [PPM].[PackageWeight](
	[PackageWeightID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Weight] [decimal](18, 2) NULL,
	[PackageCode] [int] NULL,
	[CreatorID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedByID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_PackageWeight] PRIMARY KEY CLUSTERED 
(
	[PackageWeightID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [PPM].[PackagingType]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PPM].[PackagingType]') AND type in (N'U'))
BEGIN
CREATE TABLE [PPM].[PackagingType](
	[PackagingTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ExamModelCount] [int] NULL,
	[BooksPerPackage] [int] NULL,
	[Total] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatorID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedByID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_PackagingType] PRIMARY KEY CLUSTERED 
(
	[PackagingTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [PPM].[StudentGender]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PPM].[StudentGender]') AND type in (N'U'))
BEGIN
CREATE TABLE [PPM].[StudentGender](
	[StudentGenderID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_StudentGender] PRIMARY KEY CLUSTERED 
(
	[StudentGenderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [RoleSecurity].[PersonRole]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RoleSecurity].[PersonRole]') AND type in (N'U'))
BEGIN
CREATE TABLE [RoleSecurity].[PersonRole](
	[PersonRoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[PersonId] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_PersonRole] PRIMARY KEY CLUSTERED 
(
	[PersonRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [RoleSecurity].[Role]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RoleSecurity].[Role]') AND type in (N'U'))
BEGIN
CREATE TABLE [RoleSecurity].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [RoleSecurity].[RolePrivilege]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RoleSecurity].[RolePrivilege]') AND type in (N'U'))
BEGIN
CREATE TABLE [RoleSecurity].[RolePrivilege](
	[RolePrivilegeId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[ContentEntityId] [int] NULL,
	[SystemFunctionId] [int] NULL,
	[HasAccess] [bit] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_RolePrivilege] PRIMARY KEY CLUSTERED 
(
	[RolePrivilegeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [RoleSecurity].[SecurityAccessType]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RoleSecurity].[SecurityAccessType]') AND type in (N'U'))
BEGIN
CREATE TABLE [RoleSecurity].[SecurityAccessType](
	[SecurityAccessTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_SecurityAccessType] PRIMARY KEY CLUSTERED 
(
	[SecurityAccessTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [RoleSecurity].[SystemFunction]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RoleSecurity].[SystemFunction]') AND type in (N'U'))
BEGIN
CREATE TABLE [RoleSecurity].[SystemFunction](
	[SystemFunctionId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsBackendFunction] [bit] NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_SystemFunction] PRIMARY KEY CLUSTERED 
(
	[SystemFunctionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [RoleSecurity].[UserClaim]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RoleSecurity].[UserClaim]') AND type in (N'U'))
BEGIN
CREATE TABLE [RoleSecurity].[UserClaim](
	[UserClaimId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[ClaimValue] [nvarchar](150) NULL,
	[ClaimType] [nchar](150) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED 
(
	[UserClaimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [RoleSecurity].[UserLoginProvider]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RoleSecurity].[UserLoginProvider]') AND type in (N'U'))
BEGIN
CREATE TABLE [RoleSecurity].[UserLoginProvider](
	[UserLoginProviderId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[LoginProvider] [nvarchar](500) NULL,
	[ProviderKey] [nvarchar](150) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_UserLoginProvider] PRIMARY KEY CLUSTERED 
(
	[UserLoginProviderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [RoleSecurity].[UserMonitor]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RoleSecurity].[UserMonitor]') AND type in (N'U'))
BEGIN
CREATE TABLE [RoleSecurity].[UserMonitor](
	[UserMonitorId] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NULL,
	[IsSuccess] [bit] NULL,
	[UserIP] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_UserMonitor] PRIMARY KEY CLUSTERED 
(
	[UserMonitorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[ExamView]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ExamView]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[ExamView]
AS
SELECT PPM.Exam.ExamID, PPM.Exam.ExamCode, PPM.Exam.Name, PPM.Exam.ExamSpecialityID, PPM.Exam.StudentGenderID, PPM.Exam.NumberofSections, PPM.Exam.NumberofPages, PPM.Exam.TimeForSection, 
                  PPM.Exam.Notes, PPM.Exam.IsActive, PPM.Exam.CreatorID, PPM.Exam.CreatedDate, PPM.Exam.ModifiedByID, PPM.Exam.ModifiedDate, PPM.ExamSpeciality.Name AS ExamSpecialityName, 
                  PPM.ExamType.Name AS ExamTypeName, PPM.ExamSpeciality.ExamTypeID, PPM.StudentGender.Name AS StudentGenderName
FROM     PPM.Exam INNER JOIN
                  PPM.ExamSpeciality ON PPM.Exam.ExamSpecialityID = PPM.ExamSpeciality.ExamSpecialityID INNER JOIN
                  PPM.ExamType ON PPM.ExamSpeciality.ExamTypeID = PPM.ExamType.ExaminationTypeID INNER JOIN
                  PPM.StudentGender ON PPM.Exam.StudentGenderID = PPM.StudentGender.StudentGenderID
' 
GO
/****** Object:  View [dbo].[ViewExamCenter]    Script Date: 4/6/2015 8:16:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewExamCenter]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[ViewExamCenter]
AS
SELECT PPM.ExamCenter.ExaminationCenterID, PPM.ExamCenter.CenterCode, PPM.ExamCenter.Name, PPM.ExamCenter.CityID, PPM.ExamCenter.StudentGenderID, PPM.ExamCenter.IsActive, PPM.ExamCenter.CreatorID, 
                  PPM.ExamCenter.CreatedDate, PPM.ExamCenter.ModifiedByID, PPM.ExamCenter.ModifiedDate, Person.City.StateRegionID, Person.City.Name AS CityName, Person.StateProvince.Name AS StateProvinceName, 
                  Person.CountryRegion.Name AS CountryName
FROM     PPM.ExamCenter INNER JOIN
                  Person.City ON PPM.ExamCenter.CityID = Person.City.CityID INNER JOIN
                  Person.StateProvince ON Person.City.StateRegionID = Person.StateProvince.StateProvinceId INNER JOIN
                  Person.CountryRegion ON Person.StateProvince.CountryRegionCode = Person.CountryRegion.CountryRegionCode
' 
GO
SET IDENTITY_INSERT [Person].[BusinessEntity] ON 

GO
INSERT [Person].[BusinessEntity] ([BusinessEntityId], [RowGuid], [ModifiedDate]) VALUES (1, N'306c3c41-18d4-420b-9786-4fde57909945', CAST(0x0000A45D000A9B2A AS DateTime))
GO
INSERT [Person].[BusinessEntity] ([BusinessEntityId], [RowGuid], [ModifiedDate]) VALUES (5, N'509ec9de-abfa-4359-baab-f8a1836b4b11', CAST(0x0000A45D0013C95F AS DateTime))
GO
INSERT [Person].[BusinessEntity] ([BusinessEntityId], [RowGuid], [ModifiedDate]) VALUES (6, N'3531225d-436d-4b4c-92ff-e0d9300bd4f2', CAST(0x0000A45D00158577 AS DateTime))
GO
INSERT [Person].[BusinessEntity] ([BusinessEntityId], [RowGuid], [ModifiedDate]) VALUES (7, N'34db8ba5-0e93-4f58-add2-53b1a2960c00', CAST(0x0000A46500B650D2 AS DateTime))
GO
INSERT [Person].[BusinessEntity] ([BusinessEntityId], [RowGuid], [ModifiedDate]) VALUES (8, N'6f10c830-ac28-4296-9929-1085cb4ca678', CAST(0x0000A46500B81321 AS DateTime))
GO
INSERT [Person].[BusinessEntity] ([BusinessEntityId], [RowGuid], [ModifiedDate]) VALUES (9, N'bcbbe5a5-2980-400c-add8-4d142a026a61', CAST(0x0000A46500BDC95A AS DateTime))
GO
INSERT [Person].[BusinessEntity] ([BusinessEntityId], [RowGuid], [ModifiedDate]) VALUES (10, N'9eb171f9-b2be-438b-93c6-b7ba1fb962c7', CAST(0x0000A46500CB9D95 AS DateTime))
GO
INSERT [Person].[BusinessEntity] ([BusinessEntityId], [RowGuid], [ModifiedDate]) VALUES (11, N'03da52dc-af4e-4c86-95dd-9e59decd61b0', CAST(0x0000A46C00DE5923 AS DateTime))
GO
SET IDENTITY_INSERT [Person].[BusinessEntity] OFF
GO
SET IDENTITY_INSERT [Person].[City] ON 

GO
INSERT [Person].[City] ([CityID], [StateRegionID], [Name], [IsActive]) VALUES (1, 1, N'الرياض', 1)
GO
INSERT [Person].[City] ([CityID], [StateRegionID], [Name], [IsActive]) VALUES (3, 14, N'الدمام', NULL)
GO
SET IDENTITY_INSERT [Person].[City] OFF
GO
INSERT [Person].[CountryRegion] ([CountryRegionCode], [Name], [ModifiedDate]) VALUES (N'SAU', N'المملكة العربية السعودية', CAST(0x0000A46500D55813 AS DateTime))
GO
INSERT [Person].[Credential] ([BusinessEntityId], [Username], [PasswordHash], [PasswordSalt], [ActivationCode], [IsActivated], [IsActive], [RowGuid], [Email], [EmailConfirmationCode], [EmailConfirmed], [PhoneNumber], [PhoneNumberConfirmationCode], [PhoneNumberConfirmed], [LockoutEnabled], [TwoFactorEnabled], [LockoutEndDateUtc], [AccessFailedCount], [ModifiedDate], [CreatedDate], [CreatedBy], [ModifiedBy]) VALUES (1, N'ramy.mostafa@gmail.com', N'ACYwrRJNk030ni4SbrR551oWbWG2woYJrkYGnU6G6Gv59OdS4hbJGRiMR3s7qvCRgw==', N'2d135c2e-6dfc-4e38-a27f-99b40a51bea8', NULL, 1, 1, N'46774a27-8d69-44b3-a604-3b938e9f6c2e', N'ramy.mostafa@gmail.com', NULL, 1, NULL, NULL, NULL, 1, 1, NULL, 0, CAST(0x0000A45F00E01362 AS DateTime), NULL, NULL, NULL)
GO
INSERT [Person].[Credential] ([BusinessEntityId], [Username], [PasswordHash], [PasswordSalt], [ActivationCode], [IsActivated], [IsActive], [RowGuid], [Email], [EmailConfirmationCode], [EmailConfirmed], [PhoneNumber], [PhoneNumberConfirmationCode], [PhoneNumberConfirmed], [LockoutEnabled], [TwoFactorEnabled], [LockoutEndDateUtc], [AccessFailedCount], [ModifiedDate], [CreatedDate], [CreatedBy], [ModifiedBy]) VALUES (5, N'rmostafa', N'AGcllW0mHp3AMYtRkG470s2v3IEkiPQxfXc7fEPSnB7kdkjenseCK+CiGxDPmcr6Hw==', N'51f49c58-aff9-4146-8dbb-da13e91bdaef', NULL, 1, 1, N'a626c4a5-26b1-4400-9a6e-169b96e33f02', N'champion_rbm@hotmail.com', NULL, 1, NULL, NULL, NULL, 1, 1, NULL, 0, CAST(0x0000A4640173DA58 AS DateTime), CAST(0x0000A45D00140185 AS DateTime), NULL, NULL)
GO
INSERT [Person].[Credential] ([BusinessEntityId], [Username], [PasswordHash], [PasswordSalt], [ActivationCode], [IsActivated], [IsActive], [RowGuid], [Email], [EmailConfirmationCode], [EmailConfirmed], [PhoneNumber], [PhoneNumberConfirmationCode], [PhoneNumberConfirmed], [LockoutEnabled], [TwoFactorEnabled], [LockoutEndDateUtc], [AccessFailedCount], [ModifiedDate], [CreatedDate], [CreatedBy], [ModifiedBy]) VALUES (8, N'mohsen', N'ANylHX4BzD2c8BJi8zzLttCdTwIw40pYPmDK4BTHZ2PH0GJF8J7iXgr39H5cnsVQIA==', N'570b2102-8574-4583-b6ce-df1cddf37628', NULL, 1, 1, N'21815d26-424b-40b9-902e-f75e6e09c5a9', N'mohsen@qiyas.sa', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, CAST(0x0000A46500B8132F AS DateTime), CAST(0x0000A46500B8132F AS DateTime), NULL, NULL)
GO
INSERT [Person].[Credential] ([BusinessEntityId], [Username], [PasswordHash], [PasswordSalt], [ActivationCode], [IsActivated], [IsActive], [RowGuid], [Email], [EmailConfirmationCode], [EmailConfirmed], [PhoneNumber], [PhoneNumberConfirmationCode], [PhoneNumberConfirmed], [LockoutEnabled], [TwoFactorEnabled], [LockoutEndDateUtc], [AccessFailedCount], [ModifiedDate], [CreatedDate], [CreatedBy], [ModifiedBy]) VALUES (9, N'bassam', N'AA3hD9a1AfKv+ZDzqeMlHXCutGHhEmbC6We7EoQPuoAp2dNTz2LnMpfQV9/WZ/cfKg==', N'33eb173d-e6fc-4756-a9fd-46ee72517901', NULL, 1, 0, N'f9f9b25d-fbda-4ee6-9ba0-cd66f3983e59', N'bassam@qiyas.sa', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, CAST(0x0000A46500D0B566 AS DateTime), CAST(0x0000A46500BDC967 AS DateTime), NULL, NULL)
GO
INSERT [Person].[Credential] ([BusinessEntityId], [Username], [PasswordHash], [PasswordSalt], [ActivationCode], [IsActivated], [IsActive], [RowGuid], [Email], [EmailConfirmationCode], [EmailConfirmed], [PhoneNumber], [PhoneNumberConfirmationCode], [PhoneNumberConfirmed], [LockoutEnabled], [TwoFactorEnabled], [LockoutEndDateUtc], [AccessFailedCount], [ModifiedDate], [CreatedDate], [CreatedBy], [ModifiedBy]) VALUES (11, N'nayef', N'AHzqcYxpDiqQ9MIP7UIPiJxTXKhsgiEkeRAlfRIh9j0UFJ7oY5lm9LScK78RGyVWtA==', N'841eb05a-9661-4819-a381-7720fef53f43', NULL, 1, 1, N'ab089363-52af-453f-b5d1-47f1a8fbaa14', N'nayef@qiyas.sa', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, CAST(0x0000A46C00DE594D AS DateTime), CAST(0x0000A46C00DE594D AS DateTime), NULL, NULL)
GO
INSERT [Person].[Person] ([BusinessEntityId], [FirstName], [MiddleName], [LastName], [DisplayName], [Title], [NameStyle], [EmailPromotion], [RowGuid], [ModifiedDate], [CreatedDate], [NationalityCode], [Gender], [DateofBirth], [PersonImage]) VALUES (1, N'رامي', N'محمد', N'مصطفى', N'رامي مصطفى', NULL, NULL, NULL, N'fb234aad-730c-474a-bdfb-ee9b441e263c', CAST(0x0000A45D000AEC87 AS DateTime), CAST(0x0000A45D000AEC87 AS DateTime), N'EGY', N'M', CAST(0x000078EE00000000 AS DateTime), NULL)
GO
INSERT [Person].[Person] ([BusinessEntityId], [FirstName], [MiddleName], [LastName], [DisplayName], [Title], [NameStyle], [EmailPromotion], [RowGuid], [ModifiedDate], [CreatedDate], [NationalityCode], [Gender], [DateofBirth], [PersonImage]) VALUES (5, N'أحمد', N'', N'السياري', N'أحمد السياري', NULL, NULL, NULL, N'21fe7e10-4e7e-41a4-be04-0a086385c3d4', CAST(0x0000A45D0013CA53 AS DateTime), CAST(0x0000A45D0013CA53 AS DateTime), N'EGY', N'M', NULL, NULL)
GO
INSERT [Person].[Person] ([BusinessEntityId], [FirstName], [MiddleName], [LastName], [DisplayName], [Title], [NameStyle], [EmailPromotion], [RowGuid], [ModifiedDate], [CreatedDate], [NationalityCode], [Gender], [DateofBirth], [PersonImage]) VALUES (8, N'محسن', N'', N'', N'محسن', NULL, NULL, NULL, N'501b51f4-3323-4997-bf10-321fd18bc150', CAST(0x0000A46500B81328 AS DateTime), CAST(0x0000A46500B81328 AS DateTime), NULL, NULL, NULL, NULL)
GO
INSERT [Person].[Person] ([BusinessEntityId], [FirstName], [MiddleName], [LastName], [DisplayName], [Title], [NameStyle], [EmailPromotion], [RowGuid], [ModifiedDate], [CreatedDate], [NationalityCode], [Gender], [DateofBirth], [PersonImage]) VALUES (9, N'Bassam', N'Mohamed', N'Mostafa', N'Bassam Mohamed Mostafa', NULL, NULL, NULL, N'35a75797-1936-4559-99f9-b219be8d86c8', CAST(0x0000A46500D0B55C AS DateTime), CAST(0x0000A46500BDC961 AS DateTime), NULL, NULL, NULL, NULL)
GO
INSERT [Person].[Person] ([BusinessEntityId], [FirstName], [MiddleName], [LastName], [DisplayName], [Title], [NameStyle], [EmailPromotion], [RowGuid], [ModifiedDate], [CreatedDate], [NationalityCode], [Gender], [DateofBirth], [PersonImage]) VALUES (11, N'نايف', N'', N'الشايع', N'نايف الشايع', NULL, NULL, NULL, N'b2c74743-7485-4baf-9896-6f41e0517adb', CAST(0x0000A46C00DE593E AS DateTime), CAST(0x0000A46C00DE593E AS DateTime), NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [Person].[StateProvince] ON 

GO
INSERT [Person].[StateProvince] ([StateProvinceId], [StateProvinceCode], [CountryRegionCode], [IsOnlyStateProvince], [Name], [RowGuid], [ModifiedDate]) VALUES (1, NULL, N'SAU', NULL, N'الرياض', N'15052967-5bb2-4fe4-b54b-32f0ae35ee82', CAST(0x0000A46600B7C87C AS DateTime))
GO
INSERT [Person].[StateProvince] ([StateProvinceId], [StateProvinceCode], [CountryRegionCode], [IsOnlyStateProvince], [Name], [RowGuid], [ModifiedDate]) VALUES (3, NULL, N'SAU', NULL, N'القصيم', N'1c1e0dde-f46c-416b-a31f-ab68bdfe71d3', CAST(0x0000A46600BB0911 AS DateTime))
GO
INSERT [Person].[StateProvince] ([StateProvinceId], [StateProvinceCode], [CountryRegionCode], [IsOnlyStateProvince], [Name], [RowGuid], [ModifiedDate]) VALUES (4, NULL, N'SAU', NULL, N'مكة المكرمة', N'0b66cf50-b9f7-4a68-9338-ed4345a521aa', CAST(0x0000A46600BB155F AS DateTime))
GO
INSERT [Person].[StateProvince] ([StateProvinceId], [StateProvinceCode], [CountryRegionCode], [IsOnlyStateProvince], [Name], [RowGuid], [ModifiedDate]) VALUES (5, NULL, N'SAU', NULL, N'المدينة المنورة', N'e38e09ce-815d-44da-8ae3-e57b21c50665', CAST(0x0000A46600BB21CA AS DateTime))
GO
INSERT [Person].[StateProvince] ([StateProvinceId], [StateProvinceCode], [CountryRegionCode], [IsOnlyStateProvince], [Name], [RowGuid], [ModifiedDate]) VALUES (6, NULL, N'SAU', NULL, N'حائل', N'3bab3d35-d515-4c48-8ac0-327f9442a686', CAST(0x0000A46600BB308B AS DateTime))
GO
INSERT [Person].[StateProvince] ([StateProvinceId], [StateProvinceCode], [CountryRegionCode], [IsOnlyStateProvince], [Name], [RowGuid], [ModifiedDate]) VALUES (7, NULL, N'SAU', NULL, N'الجوف', N'ff628280-0e08-4ab0-96ed-6e577b60d6d5', CAST(0x0000A46600BB4E0E AS DateTime))
GO
INSERT [Person].[StateProvince] ([StateProvinceId], [StateProvinceCode], [CountryRegionCode], [IsOnlyStateProvince], [Name], [RowGuid], [ModifiedDate]) VALUES (8, NULL, N'SAU', NULL, N'تبوك', N'0726b005-fdbc-4270-b3eb-d9f2378a6cec', CAST(0x0000A46600BB5892 AS DateTime))
GO
INSERT [Person].[StateProvince] ([StateProvinceId], [StateProvinceCode], [CountryRegionCode], [IsOnlyStateProvince], [Name], [RowGuid], [ModifiedDate]) VALUES (9, NULL, N'SAU', NULL, N'عرعر', N'e7e783f7-fba8-4a5f-8c6a-f0fba70a0b7d', CAST(0x0000A46600BB6437 AS DateTime))
GO
INSERT [Person].[StateProvince] ([StateProvinceId], [StateProvinceCode], [CountryRegionCode], [IsOnlyStateProvince], [Name], [RowGuid], [ModifiedDate]) VALUES (10, NULL, N'SAU', NULL, N'أبها', N'51f0d59f-a763-4075-b046-230cc2def27a', CAST(0x0000A46600BB6D66 AS DateTime))
GO
INSERT [Person].[StateProvince] ([StateProvinceId], [StateProvinceCode], [CountryRegionCode], [IsOnlyStateProvince], [Name], [RowGuid], [ModifiedDate]) VALUES (11, NULL, N'SAU', NULL, N'جازان', N'8c3c75f9-2008-4b9d-a6a9-c8461206d5a8', CAST(0x0000A46600BB78EA AS DateTime))
GO
INSERT [Person].[StateProvince] ([StateProvinceId], [StateProvinceCode], [CountryRegionCode], [IsOnlyStateProvince], [Name], [RowGuid], [ModifiedDate]) VALUES (12, NULL, N'SAU', NULL, N'نجران', N'014e8358-f937-4c0f-874a-84d9ee140e60', CAST(0x0000A46600BB85E6 AS DateTime))
GO
INSERT [Person].[StateProvince] ([StateProvinceId], [StateProvinceCode], [CountryRegionCode], [IsOnlyStateProvince], [Name], [RowGuid], [ModifiedDate]) VALUES (13, NULL, N'SAU', NULL, N'الباحة', N'c78f1b1e-e3a5-4e6e-ac93-72b2d40a8a90', CAST(0x0000A46600BB9011 AS DateTime))
GO
INSERT [Person].[StateProvince] ([StateProvinceId], [StateProvinceCode], [CountryRegionCode], [IsOnlyStateProvince], [Name], [RowGuid], [ModifiedDate]) VALUES (14, NULL, N'SAU', NULL, N'الدمام', N'786b8945-abed-4926-aaab-60c8fb96e7e9', CAST(0x0000A46600BB9AD9 AS DateTime))
GO
SET IDENTITY_INSERT [Person].[StateProvince] OFF
GO
SET IDENTITY_INSERT [PPM].[BookPrintingOperation] ON 

GO
INSERT [PPM].[BookPrintingOperation] ([BookPrintingOperationID], [Name], [ExamID], [PrintsForOneModel], [ExamsNeededForA3], [ExamsNeededForA4], [ExamsNeededForCD], [OperationStatusID], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (2, NULL, 0, 10, 10, NULL, 10, 1, NULL, CAST(0x0000A472007EFBF8 AS DateTime), NULL, CAST(0x0000A472007EFBF8 AS DateTime))
GO
SET IDENTITY_INSERT [PPM].[BookPrintingOperation] OFF
GO
SET IDENTITY_INSERT [PPM].[Exam] ON 

GO
INSERT [PPM].[Exam] ([ExamID], [ExamCode], [Name], [ExamSpecialityID], [StudentGenderID], [NumberofSections], [NumberofPages], [TimeForSection], [Notes], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (0, N'11430', N'اختبار قدرات عامة تخصص أدبي طلاب', 3, 1, 1, 1, 1, NULL, 1, NULL, CAST(0x0000A46B015CBADA AS DateTime), NULL, CAST(0x0000A46B015CBA72 AS DateTime))
GO
INSERT [PPM].[Exam] ([ExamID], [ExamCode], [Name], [ExamSpecialityID], [StudentGenderID], [NumberofSections], [NumberofPages], [TimeForSection], [Notes], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (1, N'1230', N'اختبار قدرات عامة تخصص أدبي طالبات', 3, 2, 4, 3, 3, NULL, 1, NULL, CAST(0x0000A46C00E4E9B0 AS DateTime), NULL, CAST(0x0000A46C00E4E9B0 AS DateTime))
GO
SET IDENTITY_INSERT [PPM].[Exam] OFF
GO
SET IDENTITY_INSERT [PPM].[ExamCenter] ON 

GO
INSERT [PPM].[ExamCenter] ([ExaminationCenterID], [CenterCode], [Name], [CityID], [StudentGenderID], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (2, N'123', N'ُExam Center 1', 1, 2, 1, NULL, CAST(0x0000A46C00E2ED20 AS DateTime), NULL, CAST(0x0000A46C00E2ED1F AS DateTime))
GO
SET IDENTITY_INSERT [PPM].[ExamCenter] OFF
GO
SET IDENTITY_INSERT [PPM].[ExamModel] ON 

GO
INSERT [PPM].[ExamModel] ([ExamModelID], [Name], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (4, N'أ', 1, NULL, CAST(0x0000A46B00E7584C AS DateTime), NULL, CAST(0x0000A46B00E7584C AS DateTime))
GO
INSERT [PPM].[ExamModel] ([ExamModelID], [Name], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (5, N'ب', 1, NULL, CAST(0x0000A46B00E75DE7 AS DateTime), NULL, CAST(0x0000A46B00E75DE7 AS DateTime))
GO
INSERT [PPM].[ExamModel] ([ExamModelID], [Name], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (6, N'ج', 1, NULL, CAST(0x0000A46C00E1C91D AS DateTime), NULL, CAST(0x0000A46C00E1C91C AS DateTime))
GO
SET IDENTITY_INSERT [PPM].[ExamModel] OFF
GO
SET IDENTITY_INSERT [PPM].[ExamPeriod] ON 

GO
INSERT [PPM].[ExamPeriod] ([ExamPeriodID], [Name], [ExamTypeID], [ExamYear], [StudentGenderID], [StartDate], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate], [EndDate]) VALUES (1, N'قدرات عامة 1430 طلاب و طالبات', 1, 1430, 3, CAST(0x0000A47300000000 AS DateTime), 1, NULL, CAST(0x0000A46C00C2939B AS DateTime), NULL, CAST(0x0000A46C00C2939B AS DateTime), CAST(0x0000A47E00000000 AS DateTime))
GO
INSERT [PPM].[ExamPeriod] ([ExamPeriodID], [Name], [ExamTypeID], [ExamYear], [StudentGenderID], [StartDate], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate], [EndDate]) VALUES (2, N'تحصيلي 1430 طلاب و طالبات', 2, 1430, 3, CAST(0x0000A46C00000000 AS DateTime), 1, NULL, CAST(0x0000A46C00E2331D AS DateTime), NULL, CAST(0x0000A46C00E2331C AS DateTime), CAST(0x0000A47200000000 AS DateTime))
GO
SET IDENTITY_INSERT [PPM].[ExamPeriod] OFF
GO
SET IDENTITY_INSERT [PPM].[ExamSpeciality] ON 

GO
INSERT [PPM].[ExamSpeciality] ([ExamSpecialityID], [Name], [ExamTypeID], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (2, N'علمي', 1, 1, NULL, CAST(0x0000A46B00DEE02D AS DateTime), NULL, CAST(0x0000A46B00DEE02D AS DateTime))
GO
INSERT [PPM].[ExamSpeciality] ([ExamSpecialityID], [Name], [ExamTypeID], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (3, N'أدبي', 1, 1, NULL, CAST(0x0000A46B01099896 AS DateTime), NULL, CAST(0x0000A46B01099895 AS DateTime))
GO
INSERT [PPM].[ExamSpeciality] ([ExamSpecialityID], [Name], [ExamTypeID], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (4, N'test', 1, NULL, NULL, CAST(0x0000A46C00E30D52 AS DateTime), NULL, CAST(0x0000A46C00E30D52 AS DateTime))
GO
SET IDENTITY_INSERT [PPM].[ExamSpeciality] OFF
GO
SET IDENTITY_INSERT [PPM].[ExamType] ON 

GO
INSERT [PPM].[ExamType] ([ExaminationTypeID], [Name], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (1, N'قدرات عامة', 1, NULL, NULL, NULL, CAST(0x0000A46B012D2E28 AS DateTime))
GO
INSERT [PPM].[ExamType] ([ExaminationTypeID], [Name], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (2, N'تحصيلي', 1, NULL, NULL, NULL, NULL)
GO
INSERT [PPM].[ExamType] ([ExaminationTypeID], [Name], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (3, N'جامعيين', 1, NULL, NULL, NULL, NULL)
GO
INSERT [PPM].[ExamType] ([ExaminationTypeID], [Name], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (4, N'كفايات لغة إنجليزية', 1, NULL, NULL, NULL, CAST(0x0000A46B00C35CE2 AS DateTime))
GO
SET IDENTITY_INSERT [PPM].[ExamType] OFF
GO
SET IDENTITY_INSERT [PPM].[OperationStatus] ON 

GO
INSERT [PPM].[OperationStatus] ([OperationStatusID], [Name]) VALUES (1, N'طباعة')
GO
INSERT [PPM].[OperationStatus] ([OperationStatusID], [Name]) VALUES (2, N'تحزيم')
GO
SET IDENTITY_INSERT [PPM].[OperationStatus] OFF
GO
SET IDENTITY_INSERT [PPM].[PackageWeight] ON 

GO
INSERT [PPM].[PackageWeight] ([PackageWeightID], [Name], [Weight], [PackageCode], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (3, NULL, CAST(50.00 AS Decimal(18, 2)), 10, NULL, CAST(0x0000A46C00E140AA AS DateTime), NULL, CAST(0x0000A46C00E140AA AS DateTime))
GO
SET IDENTITY_INSERT [PPM].[PackageWeight] OFF
GO
SET IDENTITY_INSERT [PPM].[PackagingType] ON 

GO
INSERT [PPM].[PackagingType] ([PackagingTypeID], [Name], [ExamModelCount], [BooksPerPackage], [Total], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (2, N'متعدد(3x2)', 2, 3, 6, NULL, NULL, CAST(0x0000A467008732B5 AS DateTime), NULL, CAST(0x0000A467008732B6 AS DateTime))
GO
INSERT [PPM].[PackagingType] ([PackagingTypeID], [Name], [ExamModelCount], [BooksPerPackage], [Total], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (3, N'متعدد(3x5)', 5, 3, 15, NULL, NULL, CAST(0x0000A4670087467E AS DateTime), NULL, CAST(0x0000A4670087467E AS DateTime))
GO
INSERT [PPM].[PackagingType] ([PackagingTypeID], [Name], [ExamModelCount], [BooksPerPackage], [Total], [IsActive], [CreatorID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (1002, N'متعدد(2x5)', 5, 2, 10, 1, NULL, CAST(0x0000A46C00E1192F AS DateTime), NULL, CAST(0x0000A46C00E11930 AS DateTime))
GO
SET IDENTITY_INSERT [PPM].[PackagingType] OFF
GO
INSERT [PPM].[StudentGender] ([StudentGenderID], [Name]) VALUES (1, N'طلاب')
GO
INSERT [PPM].[StudentGender] ([StudentGenderID], [Name]) VALUES (2, N'طالبات')
GO
INSERT [PPM].[StudentGender] ([StudentGenderID], [Name]) VALUES (3, N'طلاب و طالبات')
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[ContentManagement].[DF_ContentEntity_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [ContentManagement].[ContentEntity] ADD  CONSTRAINT [DF_ContentEntity_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[ContentManagement].[DF_SystemPage_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [ContentManagement].[SystemPage] ADD  CONSTRAINT [DF_SystemPage_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[ContentManagement].[DF_SystemPage_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [ContentManagement].[SystemPage] ADD  CONSTRAINT [DF_SystemPage_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[HumanResources].[DF_Employees_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [HumanResources].[Employees] ADD  CONSTRAINT [DF_Employees_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_Address_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[Address] ADD  CONSTRAINT [DF_Address_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_Address_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[Address] ADD  CONSTRAINT [DF_Address_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_AddressType_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[AddressType] ADD  CONSTRAINT [DF_AddressType_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_AddressType_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[AddressType] ADD  CONSTRAINT [DF_AddressType_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_BusinessEntity_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[BusinessEntity] ADD  CONSTRAINT [DF_BusinessEntity_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_BusinessEntity_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[BusinessEntity] ADD  CONSTRAINT [DF_BusinessEntity_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_BusinessEntityAddress_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[BusinessEntityAddress] ADD  CONSTRAINT [DF_BusinessEntityAddress_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_BusinessEntityAddress_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[BusinessEntityAddress] ADD  CONSTRAINT [DF_BusinessEntityAddress_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_BusinessEntityContact_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[BusinessEntityContact] ADD  CONSTRAINT [DF_BusinessEntityContact_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_BusinessEntityContact_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[BusinessEntityContact] ADD  CONSTRAINT [DF_BusinessEntityContact_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_ContactType_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[ContactType] ADD  CONSTRAINT [DF_ContactType_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_CountryRegion_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[CountryRegion] ADD  CONSTRAINT [DF_CountryRegion_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_Credential_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[Credential] ADD  CONSTRAINT [DF_Credential_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_Credential_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[Credential] ADD  CONSTRAINT [DF_Credential_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_EmailAddress_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[EmailAddress] ADD  CONSTRAINT [DF_EmailAddress_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_EmailAddress_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[EmailAddress] ADD  CONSTRAINT [DF_EmailAddress_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_EmailAddressType_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[EmailAddressType] ADD  CONSTRAINT [DF_EmailAddressType_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_Person_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[Person] ADD  CONSTRAINT [DF_Person_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_Person_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[Person] ADD  CONSTRAINT [DF_Person_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_Person_CreatedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[Person] ADD  CONSTRAINT [DF_Person_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_PersonType_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[PersonPersonTypes] ADD  CONSTRAINT [DF_PersonType_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_PersonType_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[PersonPersonTypes] ADD  CONSTRAINT [DF_PersonType_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_PersonPhone_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[PersonPhone] ADD  CONSTRAINT [DF_PersonPhone_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_PersonTypes_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[PersonType] ADD  CONSTRAINT [DF_PersonTypes_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_PhoneNumberType_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[PhoneNumberType] ADD  CONSTRAINT [DF_PhoneNumberType_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_StateProvince_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[StateProvince] ADD  CONSTRAINT [DF_StateProvince_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person].[DF_StateProvince_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[StateProvince] ADD  CONSTRAINT [DF_StateProvince_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[RoleSecurity].[DF_PersonRole_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [RoleSecurity].[PersonRole] ADD  CONSTRAINT [DF_PersonRole_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[RoleSecurity].[DF_Role_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [RoleSecurity].[Role] ADD  CONSTRAINT [DF_Role_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[RoleSecurity].[DF_Role_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [RoleSecurity].[Role] ADD  CONSTRAINT [DF_Role_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[RoleSecurity].[DF_RolePrivilege_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [RoleSecurity].[RolePrivilege] ADD  CONSTRAINT [DF_RolePrivilege_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[RoleSecurity].[DF_SecurityAccessType_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [RoleSecurity].[SecurityAccessType] ADD  CONSTRAINT [DF_SecurityAccessType_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[RoleSecurity].[DF_SecurityAccessType_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [RoleSecurity].[SecurityAccessType] ADD  CONSTRAINT [DF_SecurityAccessType_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[RoleSecurity].[DF_SystemFunction_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [RoleSecurity].[SystemFunction] ADD  CONSTRAINT [DF_SystemFunction_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[RoleSecurity].[DF_SystemFunction_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [RoleSecurity].[SystemFunction] ADD  CONSTRAINT [DF_SystemFunction_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[RoleSecurity].[DF_UserClaim_CreatedDate]') AND type = 'D')
BEGIN
ALTER TABLE [RoleSecurity].[UserClaim] ADD  CONSTRAINT [DF_UserClaim_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[RoleSecurity].[DF_UserLoginProvider_CreatedDate]') AND type = 'D')
BEGIN
ALTER TABLE [RoleSecurity].[UserLoginProvider] ADD  CONSTRAINT [DF_UserLoginProvider_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
END

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ContentManagement].[FK_SystemFolder_ContentEntity]') AND parent_object_id = OBJECT_ID(N'[ContentManagement].[SystemFolder]'))
ALTER TABLE [ContentManagement].[SystemFolder]  WITH CHECK ADD  CONSTRAINT [FK_SystemFolder_ContentEntity] FOREIGN KEY([SystemFolderId])
REFERENCES [ContentManagement].[ContentEntity] ([ContentEntityId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ContentManagement].[FK_SystemFolder_ContentEntity]') AND parent_object_id = OBJECT_ID(N'[ContentManagement].[SystemFolder]'))
ALTER TABLE [ContentManagement].[SystemFolder] CHECK CONSTRAINT [FK_SystemFolder_ContentEntity]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ContentManagement].[FK_SystemPage_ContentEntity]') AND parent_object_id = OBJECT_ID(N'[ContentManagement].[SystemPage]'))
ALTER TABLE [ContentManagement].[SystemPage]  WITH CHECK ADD  CONSTRAINT [FK_SystemPage_ContentEntity] FOREIGN KEY([SystemPageId])
REFERENCES [ContentManagement].[ContentEntity] ([ContentEntityId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ContentManagement].[FK_SystemPage_ContentEntity]') AND parent_object_id = OBJECT_ID(N'[ContentManagement].[SystemPage]'))
ALTER TABLE [ContentManagement].[SystemPage] CHECK CONSTRAINT [FK_SystemPage_ContentEntity]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ContentManagement].[FK_SystemPage_SecurityAccessType]') AND parent_object_id = OBJECT_ID(N'[ContentManagement].[SystemPage]'))
ALTER TABLE [ContentManagement].[SystemPage]  WITH CHECK ADD  CONSTRAINT [FK_SystemPage_SecurityAccessType] FOREIGN KEY([SecurityAccessTypeId])
REFERENCES [RoleSecurity].[SecurityAccessType] ([SecurityAccessTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ContentManagement].[FK_SystemPage_SecurityAccessType]') AND parent_object_id = OBJECT_ID(N'[ContentManagement].[SystemPage]'))
ALTER TABLE [ContentManagement].[SystemPage] CHECK CONSTRAINT [FK_SystemPage_SecurityAccessType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ContentManagement].[FK_SystemPage_SystemFolder]') AND parent_object_id = OBJECT_ID(N'[ContentManagement].[SystemPage]'))
ALTER TABLE [ContentManagement].[SystemPage]  WITH CHECK ADD  CONSTRAINT [FK_SystemPage_SystemFolder] FOREIGN KEY([SystemFolderId])
REFERENCES [ContentManagement].[SystemFolder] ([SystemFolderId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ContentManagement].[FK_SystemPage_SystemFolder]') AND parent_object_id = OBJECT_ID(N'[ContentManagement].[SystemPage]'))
ALTER TABLE [ContentManagement].[SystemPage] CHECK CONSTRAINT [FK_SystemPage_SystemFolder]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_DepartmentLanguages_Departments]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[DepartmentLanguages]'))
ALTER TABLE [HumanResources].[DepartmentLanguages]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentLanguages_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [HumanResources].[Departments] ([DepartmentId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_DepartmentLanguages_Departments]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[DepartmentLanguages]'))
ALTER TABLE [HumanResources].[DepartmentLanguages] CHECK CONSTRAINT [FK_DepartmentLanguages_Departments]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_Departments_Organizations]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Departments]'))
ALTER TABLE [HumanResources].[Departments]  WITH CHECK ADD  CONSTRAINT [FK_Departments_Organizations] FOREIGN KEY([OrganizationId])
REFERENCES [HumanResources].[Organizations] ([OrganizationId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_Departments_Organizations]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Departments]'))
ALTER TABLE [HumanResources].[Departments] CHECK CONSTRAINT [FK_Departments_Organizations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_Divisions_Departments]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Divisions]'))
ALTER TABLE [HumanResources].[Divisions]  WITH CHECK ADD  CONSTRAINT [FK_Divisions_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [HumanResources].[Departments] ([DepartmentId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_Divisions_Departments]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Divisions]'))
ALTER TABLE [HumanResources].[Divisions] CHECK CONSTRAINT [FK_Divisions_Departments]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_Employees_Departments]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employees]'))
ALTER TABLE [HumanResources].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [HumanResources].[Departments] ([DepartmentId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_Employees_Departments]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employees]'))
ALTER TABLE [HumanResources].[Employees] CHECK CONSTRAINT [FK_Employees_Departments]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_Employees_Divisions]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employees]'))
ALTER TABLE [HumanResources].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Divisions] FOREIGN KEY([DivisionId])
REFERENCES [HumanResources].[Divisions] ([DivisionId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_Employees_Divisions]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employees]'))
ALTER TABLE [HumanResources].[Employees] CHECK CONSTRAINT [FK_Employees_Divisions]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_Employees_Employees]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employees]'))
ALTER TABLE [HumanResources].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Employees] FOREIGN KEY([ManagerId])
REFERENCES [HumanResources].[Employees] ([EmployeeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_Employees_Employees]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employees]'))
ALTER TABLE [HumanResources].[Employees] CHECK CONSTRAINT [FK_Employees_Employees]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_Employees_Person]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employees]'))
ALTER TABLE [HumanResources].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Person] FOREIGN KEY([EmployeeId])
REFERENCES [Person].[Person] ([BusinessEntityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_Employees_Person]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employees]'))
ALTER TABLE [HumanResources].[Employees] CHECK CONSTRAINT [FK_Employees_Person]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_OrganizationLanguages_Organizations]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[OrganizationLanguages]'))
ALTER TABLE [HumanResources].[OrganizationLanguages]  WITH CHECK ADD  CONSTRAINT [FK_OrganizationLanguages_Organizations] FOREIGN KEY([OrganizationId])
REFERENCES [HumanResources].[Organizations] ([OrganizationId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_OrganizationLanguages_Organizations]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[OrganizationLanguages]'))
ALTER TABLE [HumanResources].[OrganizationLanguages] CHECK CONSTRAINT [FK_OrganizationLanguages_Organizations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_Address_CountryRegion]') AND parent_object_id = OBJECT_ID(N'[Person].[Address]'))
ALTER TABLE [Person].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_CountryRegion] FOREIGN KEY([CountryRegionCode])
REFERENCES [Person].[CountryRegion] ([CountryRegionCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_Address_CountryRegion]') AND parent_object_id = OBJECT_ID(N'[Person].[Address]'))
ALTER TABLE [Person].[Address] CHECK CONSTRAINT [FK_Address_CountryRegion]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_Address_StateProvince]') AND parent_object_id = OBJECT_ID(N'[Person].[Address]'))
ALTER TABLE [Person].[Address]  WITH NOCHECK ADD  CONSTRAINT [FK_Address_StateProvince] FOREIGN KEY([StateProvinceId])
REFERENCES [Person].[StateProvince] ([StateProvinceId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_Address_StateProvince]') AND parent_object_id = OBJECT_ID(N'[Person].[Address]'))
ALTER TABLE [Person].[Address] NOCHECK CONSTRAINT [FK_Address_StateProvince]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityAddress_Address]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]'))
ALTER TABLE [Person].[BusinessEntityAddress]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEntityAddress_Address] FOREIGN KEY([AddressId])
REFERENCES [Person].[Address] ([AddressId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityAddress_Address]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]'))
ALTER TABLE [Person].[BusinessEntityAddress] CHECK CONSTRAINT [FK_BusinessEntityAddress_Address]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityAddress_AddressType]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]'))
ALTER TABLE [Person].[BusinessEntityAddress]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEntityAddress_AddressType] FOREIGN KEY([AddressTypeId])
REFERENCES [Person].[AddressType] ([AddressTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityAddress_AddressType]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]'))
ALTER TABLE [Person].[BusinessEntityAddress] CHECK CONSTRAINT [FK_BusinessEntityAddress_AddressType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityAddress_BusinessEntity]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]'))
ALTER TABLE [Person].[BusinessEntityAddress]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEntityAddress_BusinessEntity] FOREIGN KEY([BusinessEntityId])
REFERENCES [Person].[BusinessEntity] ([BusinessEntityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityAddress_BusinessEntity]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]'))
ALTER TABLE [Person].[BusinessEntityAddress] CHECK CONSTRAINT [FK_BusinessEntityAddress_BusinessEntity]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityContact_ContactType]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]'))
ALTER TABLE [Person].[BusinessEntityContact]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEntityContact_ContactType] FOREIGN KEY([ContactTypeId])
REFERENCES [Person].[ContactType] ([ContactTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityContact_ContactType]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]'))
ALTER TABLE [Person].[BusinessEntityContact] CHECK CONSTRAINT [FK_BusinessEntityContact_ContactType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityContact_Person]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]'))
ALTER TABLE [Person].[BusinessEntityContact]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEntityContact_Person] FOREIGN KEY([PersonId])
REFERENCES [Person].[Person] ([BusinessEntityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityContact_Person]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]'))
ALTER TABLE [Person].[BusinessEntityContact] CHECK CONSTRAINT [FK_BusinessEntityContact_Person]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityContact_Person1]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]'))
ALTER TABLE [Person].[BusinessEntityContact]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEntityContact_Person1] FOREIGN KEY([ContactId])
REFERENCES [Person].[Person] ([BusinessEntityId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityContact_Person1]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]'))
ALTER TABLE [Person].[BusinessEntityContact] CHECK CONSTRAINT [FK_BusinessEntityContact_Person1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_City_StateProvince]') AND parent_object_id = OBJECT_ID(N'[Person].[City]'))
ALTER TABLE [Person].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_StateProvince] FOREIGN KEY([StateRegionID])
REFERENCES [Person].[StateProvince] ([StateProvinceId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_City_StateProvince]') AND parent_object_id = OBJECT_ID(N'[Person].[City]'))
ALTER TABLE [Person].[City] CHECK CONSTRAINT [FK_City_StateProvince]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_Credential_Person]') AND parent_object_id = OBJECT_ID(N'[Person].[Credential]'))
ALTER TABLE [Person].[Credential]  WITH CHECK ADD  CONSTRAINT [FK_Credential_Person] FOREIGN KEY([BusinessEntityId])
REFERENCES [Person].[Person] ([BusinessEntityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_Credential_Person]') AND parent_object_id = OBJECT_ID(N'[Person].[Credential]'))
ALTER TABLE [Person].[Credential] CHECK CONSTRAINT [FK_Credential_Person]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_EmailAddress_EmailAddressType]') AND parent_object_id = OBJECT_ID(N'[Person].[EmailAddress]'))
ALTER TABLE [Person].[EmailAddress]  WITH CHECK ADD  CONSTRAINT [FK_EmailAddress_EmailAddressType] FOREIGN KEY([EmailAddressTypeId])
REFERENCES [Person].[EmailAddressType] ([EmailAddressTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_EmailAddress_EmailAddressType]') AND parent_object_id = OBJECT_ID(N'[Person].[EmailAddress]'))
ALTER TABLE [Person].[EmailAddress] CHECK CONSTRAINT [FK_EmailAddress_EmailAddressType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_EmailAddress_Person]') AND parent_object_id = OBJECT_ID(N'[Person].[EmailAddress]'))
ALTER TABLE [Person].[EmailAddress]  WITH CHECK ADD  CONSTRAINT [FK_EmailAddress_Person] FOREIGN KEY([BusinessEntityId])
REFERENCES [Person].[Person] ([BusinessEntityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_EmailAddress_Person]') AND parent_object_id = OBJECT_ID(N'[Person].[EmailAddress]'))
ALTER TABLE [Person].[EmailAddress] CHECK CONSTRAINT [FK_EmailAddress_Person]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_Person_BusinessEntity]') AND parent_object_id = OBJECT_ID(N'[Person].[Person]'))
ALTER TABLE [Person].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_BusinessEntity] FOREIGN KEY([BusinessEntityId])
REFERENCES [Person].[BusinessEntity] ([BusinessEntityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_Person_BusinessEntity]') AND parent_object_id = OBJECT_ID(N'[Person].[Person]'))
ALTER TABLE [Person].[Person] CHECK CONSTRAINT [FK_Person_BusinessEntity]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonLanguageProficiency_Person]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonLanguageProficiency]'))
ALTER TABLE [Person].[PersonLanguageProficiency]  WITH CHECK ADD  CONSTRAINT [FK_PersonLanguageProficiency_Person] FOREIGN KEY([PersonId])
REFERENCES [Person].[Person] ([BusinessEntityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonLanguageProficiency_Person]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonLanguageProficiency]'))
ALTER TABLE [Person].[PersonLanguageProficiency] CHECK CONSTRAINT [FK_PersonLanguageProficiency_Person]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonLanguages_Person]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonLanguages]'))
ALTER TABLE [Person].[PersonLanguages]  WITH CHECK ADD  CONSTRAINT [FK_PersonLanguages_Person] FOREIGN KEY([PersonId])
REFERENCES [Person].[Person] ([BusinessEntityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonLanguages_Person]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonLanguages]'))
ALTER TABLE [Person].[PersonLanguages] CHECK CONSTRAINT [FK_PersonLanguages_Person]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonPhone_Person]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonPhone]'))
ALTER TABLE [Person].[PersonPhone]  WITH CHECK ADD  CONSTRAINT [FK_PersonPhone_Person] FOREIGN KEY([BusinessEntityId])
REFERENCES [Person].[Person] ([BusinessEntityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonPhone_Person]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonPhone]'))
ALTER TABLE [Person].[PersonPhone] CHECK CONSTRAINT [FK_PersonPhone_Person]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonPhone_PhoneNumberType]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonPhone]'))
ALTER TABLE [Person].[PersonPhone]  WITH CHECK ADD  CONSTRAINT [FK_PersonPhone_PhoneNumberType] FOREIGN KEY([PhoneNumberTypeId])
REFERENCES [Person].[PhoneNumberType] ([PhoneNumberTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonPhone_PhoneNumberType]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonPhone]'))
ALTER TABLE [Person].[PersonPhone] CHECK CONSTRAINT [FK_PersonPhone_PhoneNumberType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonTypes_Person]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonType]'))
ALTER TABLE [Person].[PersonType]  WITH CHECK ADD  CONSTRAINT [FK_PersonTypes_Person] FOREIGN KEY([BusinessEntityId])
REFERENCES [Person].[Person] ([BusinessEntityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonTypes_Person]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonType]'))
ALTER TABLE [Person].[PersonType] CHECK CONSTRAINT [FK_PersonTypes_Person]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonTypes_PersonType]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonType]'))
ALTER TABLE [Person].[PersonType]  WITH CHECK ADD  CONSTRAINT [FK_PersonTypes_PersonType] FOREIGN KEY([PersonPersonTypesId])
REFERENCES [Person].[PersonPersonTypes] ([PersonPersonTypesId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonTypes_PersonType]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonType]'))
ALTER TABLE [Person].[PersonType] CHECK CONSTRAINT [FK_PersonTypes_PersonType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_StateProvince_CountryRegion]') AND parent_object_id = OBJECT_ID(N'[Person].[StateProvince]'))
ALTER TABLE [Person].[StateProvince]  WITH CHECK ADD  CONSTRAINT [FK_StateProvince_CountryRegion] FOREIGN KEY([CountryRegionCode])
REFERENCES [Person].[CountryRegion] ([CountryRegionCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_StateProvince_CountryRegion]') AND parent_object_id = OBJECT_ID(N'[Person].[StateProvince]'))
ALTER TABLE [Person].[StateProvince] CHECK CONSTRAINT [FK_StateProvince_CountryRegion]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_BookPrintingOperation_Exam]') AND parent_object_id = OBJECT_ID(N'[PPM].[BookPrintingOperation]'))
ALTER TABLE [PPM].[BookPrintingOperation]  WITH CHECK ADD  CONSTRAINT [FK_BookPrintingOperation_Exam] FOREIGN KEY([ExamID])
REFERENCES [PPM].[Exam] ([ExamID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_BookPrintingOperation_Exam]') AND parent_object_id = OBJECT_ID(N'[PPM].[BookPrintingOperation]'))
ALTER TABLE [PPM].[BookPrintingOperation] CHECK CONSTRAINT [FK_BookPrintingOperation_Exam]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_BookPrintingOperation_OperationStatus]') AND parent_object_id = OBJECT_ID(N'[PPM].[BookPrintingOperation]'))
ALTER TABLE [PPM].[BookPrintingOperation]  WITH CHECK ADD  CONSTRAINT [FK_BookPrintingOperation_OperationStatus] FOREIGN KEY([OperationStatusID])
REFERENCES [PPM].[OperationStatus] ([OperationStatusID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_BookPrintingOperation_OperationStatus]') AND parent_object_id = OBJECT_ID(N'[PPM].[BookPrintingOperation]'))
ALTER TABLE [PPM].[BookPrintingOperation] CHECK CONSTRAINT [FK_BookPrintingOperation_OperationStatus]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_Exam_ExamSpeciality]') AND parent_object_id = OBJECT_ID(N'[PPM].[Exam]'))
ALTER TABLE [PPM].[Exam]  WITH CHECK ADD  CONSTRAINT [FK_Exam_ExamSpeciality] FOREIGN KEY([ExamSpecialityID])
REFERENCES [PPM].[ExamSpeciality] ([ExamSpecialityID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_Exam_ExamSpeciality]') AND parent_object_id = OBJECT_ID(N'[PPM].[Exam]'))
ALTER TABLE [PPM].[Exam] CHECK CONSTRAINT [FK_Exam_ExamSpeciality]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_Exam_StudentGender]') AND parent_object_id = OBJECT_ID(N'[PPM].[Exam]'))
ALTER TABLE [PPM].[Exam]  WITH CHECK ADD  CONSTRAINT [FK_Exam_StudentGender] FOREIGN KEY([StudentGenderID])
REFERENCES [PPM].[StudentGender] ([StudentGenderID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_Exam_StudentGender]') AND parent_object_id = OBJECT_ID(N'[PPM].[Exam]'))
ALTER TABLE [PPM].[Exam] CHECK CONSTRAINT [FK_Exam_StudentGender]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_ExamCenter_City]') AND parent_object_id = OBJECT_ID(N'[PPM].[ExamCenter]'))
ALTER TABLE [PPM].[ExamCenter]  WITH CHECK ADD  CONSTRAINT [FK_ExamCenter_City] FOREIGN KEY([CityID])
REFERENCES [Person].[City] ([CityID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_ExamCenter_City]') AND parent_object_id = OBJECT_ID(N'[PPM].[ExamCenter]'))
ALTER TABLE [PPM].[ExamCenter] CHECK CONSTRAINT [FK_ExamCenter_City]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_ExamCenter_StudentGender]') AND parent_object_id = OBJECT_ID(N'[PPM].[ExamCenter]'))
ALTER TABLE [PPM].[ExamCenter]  WITH CHECK ADD  CONSTRAINT [FK_ExamCenter_StudentGender] FOREIGN KEY([StudentGenderID])
REFERENCES [PPM].[StudentGender] ([StudentGenderID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_ExamCenter_StudentGender]') AND parent_object_id = OBJECT_ID(N'[PPM].[ExamCenter]'))
ALTER TABLE [PPM].[ExamCenter] CHECK CONSTRAINT [FK_ExamCenter_StudentGender]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_ExamPeriod_ExamType]') AND parent_object_id = OBJECT_ID(N'[PPM].[ExamPeriod]'))
ALTER TABLE [PPM].[ExamPeriod]  WITH CHECK ADD  CONSTRAINT [FK_ExamPeriod_ExamType] FOREIGN KEY([ExamTypeID])
REFERENCES [PPM].[ExamType] ([ExaminationTypeID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_ExamPeriod_ExamType]') AND parent_object_id = OBJECT_ID(N'[PPM].[ExamPeriod]'))
ALTER TABLE [PPM].[ExamPeriod] CHECK CONSTRAINT [FK_ExamPeriod_ExamType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_ExamPeriod_StudentGender]') AND parent_object_id = OBJECT_ID(N'[PPM].[ExamPeriod]'))
ALTER TABLE [PPM].[ExamPeriod]  WITH CHECK ADD  CONSTRAINT [FK_ExamPeriod_StudentGender] FOREIGN KEY([StudentGenderID])
REFERENCES [PPM].[StudentGender] ([StudentGenderID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_ExamPeriod_StudentGender]') AND parent_object_id = OBJECT_ID(N'[PPM].[ExamPeriod]'))
ALTER TABLE [PPM].[ExamPeriod] CHECK CONSTRAINT [FK_ExamPeriod_StudentGender]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_ExamSpeciality_ExamType]') AND parent_object_id = OBJECT_ID(N'[PPM].[ExamSpeciality]'))
ALTER TABLE [PPM].[ExamSpeciality]  WITH CHECK ADD  CONSTRAINT [FK_ExamSpeciality_ExamType] FOREIGN KEY([ExamTypeID])
REFERENCES [PPM].[ExamType] ([ExaminationTypeID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[PPM].[FK_ExamSpeciality_ExamType]') AND parent_object_id = OBJECT_ID(N'[PPM].[ExamSpeciality]'))
ALTER TABLE [PPM].[ExamSpeciality] CHECK CONSTRAINT [FK_ExamSpeciality_ExamType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[RoleSecurity].[FK_PersonRole_Credential]') AND parent_object_id = OBJECT_ID(N'[RoleSecurity].[PersonRole]'))
ALTER TABLE [RoleSecurity].[PersonRole]  WITH CHECK ADD  CONSTRAINT [FK_PersonRole_Credential] FOREIGN KEY([PersonId])
REFERENCES [Person].[Credential] ([BusinessEntityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[RoleSecurity].[FK_PersonRole_Credential]') AND parent_object_id = OBJECT_ID(N'[RoleSecurity].[PersonRole]'))
ALTER TABLE [RoleSecurity].[PersonRole] CHECK CONSTRAINT [FK_PersonRole_Credential]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[RoleSecurity].[FK_PersonRole_Role]') AND parent_object_id = OBJECT_ID(N'[RoleSecurity].[PersonRole]'))
ALTER TABLE [RoleSecurity].[PersonRole]  WITH CHECK ADD  CONSTRAINT [FK_PersonRole_Role] FOREIGN KEY([RoleId])
REFERENCES [RoleSecurity].[Role] ([RoleId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[RoleSecurity].[FK_PersonRole_Role]') AND parent_object_id = OBJECT_ID(N'[RoleSecurity].[PersonRole]'))
ALTER TABLE [RoleSecurity].[PersonRole] CHECK CONSTRAINT [FK_PersonRole_Role]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[RoleSecurity].[FK_RolePrivilege_ContentEntity]') AND parent_object_id = OBJECT_ID(N'[RoleSecurity].[RolePrivilege]'))
ALTER TABLE [RoleSecurity].[RolePrivilege]  WITH CHECK ADD  CONSTRAINT [FK_RolePrivilege_ContentEntity] FOREIGN KEY([ContentEntityId])
REFERENCES [ContentManagement].[ContentEntity] ([ContentEntityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[RoleSecurity].[FK_RolePrivilege_ContentEntity]') AND parent_object_id = OBJECT_ID(N'[RoleSecurity].[RolePrivilege]'))
ALTER TABLE [RoleSecurity].[RolePrivilege] CHECK CONSTRAINT [FK_RolePrivilege_ContentEntity]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[RoleSecurity].[FK_RolePrivilege_Role]') AND parent_object_id = OBJECT_ID(N'[RoleSecurity].[RolePrivilege]'))
ALTER TABLE [RoleSecurity].[RolePrivilege]  WITH CHECK ADD  CONSTRAINT [FK_RolePrivilege_Role] FOREIGN KEY([RoleId])
REFERENCES [RoleSecurity].[Role] ([RoleId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[RoleSecurity].[FK_RolePrivilege_Role]') AND parent_object_id = OBJECT_ID(N'[RoleSecurity].[RolePrivilege]'))
ALTER TABLE [RoleSecurity].[RolePrivilege] CHECK CONSTRAINT [FK_RolePrivilege_Role]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[RoleSecurity].[FK_RolePrivilege_SystemFunction]') AND parent_object_id = OBJECT_ID(N'[RoleSecurity].[RolePrivilege]'))
ALTER TABLE [RoleSecurity].[RolePrivilege]  WITH CHECK ADD  CONSTRAINT [FK_RolePrivilege_SystemFunction] FOREIGN KEY([SystemFunctionId])
REFERENCES [RoleSecurity].[SystemFunction] ([SystemFunctionId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[RoleSecurity].[FK_RolePrivilege_SystemFunction]') AND parent_object_id = OBJECT_ID(N'[RoleSecurity].[RolePrivilege]'))
ALTER TABLE [RoleSecurity].[RolePrivilege] CHECK CONSTRAINT [FK_RolePrivilege_SystemFunction]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[RoleSecurity].[FK_UserClaim_Person]') AND parent_object_id = OBJECT_ID(N'[RoleSecurity].[UserClaim]'))
ALTER TABLE [RoleSecurity].[UserClaim]  WITH CHECK ADD  CONSTRAINT [FK_UserClaim_Person] FOREIGN KEY([UserId])
REFERENCES [Person].[Person] ([BusinessEntityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[RoleSecurity].[FK_UserClaim_Person]') AND parent_object_id = OBJECT_ID(N'[RoleSecurity].[UserClaim]'))
ALTER TABLE [RoleSecurity].[UserClaim] CHECK CONSTRAINT [FK_UserClaim_Person]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[RoleSecurity].[FK_UserLoginProvider_Person]') AND parent_object_id = OBJECT_ID(N'[RoleSecurity].[UserLoginProvider]'))
ALTER TABLE [RoleSecurity].[UserLoginProvider]  WITH CHECK ADD  CONSTRAINT [FK_UserLoginProvider_Person] FOREIGN KEY([UserId])
REFERENCES [Person].[Person] ([BusinessEntityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[RoleSecurity].[FK_UserLoginProvider_Person]') AND parent_object_id = OBJECT_ID(N'[RoleSecurity].[UserLoginProvider]'))
ALTER TABLE [RoleSecurity].[UserLoginProvider] CHECK CONSTRAINT [FK_UserLoginProvider_Person]
GO
