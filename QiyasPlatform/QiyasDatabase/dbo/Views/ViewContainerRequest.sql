CREATE VIEW dbo.ViewContainerRequest
AS
SELECT PPM.ContainerRequestPack.ContainerRequestPackID, PPM.ContainerRequestPack.ContainerRequestID, PPM.ContainerRequestPack.BookPackItemID, PPM.ContainerRequestPack.CreatedDate, 
                  PPM.ContainerRequestPack.CreatedBy, dbo.ViewBookPackItemComplete.BookPackingOperationID, dbo.ViewBookPackItemComplete.PackCode, dbo.ViewBookPackItemComplete.Weight, 
                  dbo.ViewBookPackItemComplete.PackSerial, dbo.ViewBookPackItemComplete.OperationStatusID, dbo.ViewBookPackItemComplete.ParentID, dbo.ViewBookPackItemComplete.StartBookSerial, 
                  dbo.ViewBookPackItemComplete.LastBookSerial, dbo.ViewBookPackItemComplete.BookPackItemOperationID, dbo.ViewBookPackItemComplete.ExamModelName, dbo.ViewBookPackItemComplete.PackagingTypeID, 
                  dbo.ViewBookPackItemComplete.PackingCalculationTypeID, dbo.ViewBookPackItemComplete.AllocatedFrom, dbo.ViewBookPackItemComplete.PackingValue, dbo.ViewBookPackItemComplete.PackageTotal, 
                  dbo.ViewBookPackItemComplete.PackingParentID, dbo.ViewBookPackItemComplete.BookPrintingOperationID, dbo.ViewBookPackItemComplete.ExamID, dbo.ViewBookPackItemComplete.PrintsForOneModel, 
                  dbo.ViewBookPackItemComplete.ExamsNeededForA3, dbo.ViewBookPackItemComplete.ExamsNeededForA4, dbo.ViewBookPackItemComplete.ExamsNeededForCD, 
                  dbo.ViewBookPackItemComplete.PackageOperationStatusID, dbo.ViewBookPackItemComplete.TotalExamModels, dbo.ViewBookPackItemComplete.TotalPacks, dbo.ViewBookPackItemComplete.ExamCode, 
                  dbo.ViewBookPackItemComplete.ExamSpecialityID, dbo.ViewBookPackItemComplete.StudentGenderID, dbo.ViewBookPackItemComplete.NumberofPages, dbo.ViewBookPackItemComplete.NumberofSections, 
                  dbo.ViewBookPackItemComplete.Notes, dbo.ViewBookPackItemComplete.TimeForSection, dbo.ViewBookPackItemComplete.IsActive, dbo.ViewBookPackItemComplete.BooksCount, 
                  dbo.ViewBookPackItemComplete.PackageTypeExamModelCount, dbo.ViewBookPackItemComplete.PackageTypeBooksPerPackage, dbo.ViewBookPackItemComplete.PackageTypeTotal, 
                  PPM.ContainerRequest.ExamCenterRequiredExamsID
FROM     PPM.ContainerRequestPack INNER JOIN
                  dbo.ViewBookPackItemComplete ON PPM.ContainerRequestPack.BookPackItemID = dbo.ViewBookPackItemComplete.BookPackItemID INNER JOIN
                  PPM.ContainerRequest ON PPM.ContainerRequestPack.ContainerRequestID = PPM.ContainerRequest.ContainerRequestID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewContainerRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ContainerRequestPack (PPM)"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 305
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "ViewBookPackItemComplete"
            Begin Extent = 
               Top = 7
               Left = 353
               Bottom = 170
               Right = 653
            End
            DisplayFlags = 280
            TopColumn = 34
         End
         Begin Table = "ContainerRequest (PPM)"
            Begin Extent = 
               Top = 7
               Left = 701
               Bottom = 170
               Right = 992
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewContainerRequest';





