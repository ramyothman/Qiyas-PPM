/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
delete from [PPM].[OperationStatus]
SET IDENTITY_INSERT [PPM].[OperationStatus] ON 

GO
INSERT [PPM].[OperationStatus] ([OperationStatusID], [Name]) VALUES (1, N'طباعة')
GO
INSERT [PPM].[OperationStatus] ([OperationStatusID], [Name]) VALUES (2, N'تحزيم')
GO
INSERT [PPM].[OperationStatus] ([OperationStatusID], [Name]) VALUES (3, N'وزن')
GO
INSERT [PPM].[OperationStatus] ([OperationStatusID], [Name]) VALUES (4, N'استلام')
GO
INSERT [PPM].[OperationStatus] ([OperationStatusID], [Name]) VALUES (5, N'تخزين')
GO
INSERT [PPM].[OperationStatus] ([OperationStatusID], [Name]) VALUES (6, N'مصروف')
GO
SET IDENTITY_INSERT [PPM].[OperationStatus] OFF
GO