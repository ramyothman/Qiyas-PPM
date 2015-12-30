CREATE VIEW dbo.ViewBookPackItemComplete
AS
SELECT PPM.BookPackItem.BookPackItemID, PPM.BookPackItem.BookPackingOperationID, PPM.BookPackItem.PackCode, PPM.BookPackItem.PackSerial, PPM.BookPackItem.Weight, PPM.BookPackItem.OperationStatusID, 
                  PPM.BookPackItem.ParentID, PPM.BookPackItem.StartBookSerial, PPM.BookPackItem.LastBookSerial, PPM.BookPackItem.BookPackItemOperationID, 
                  dbo.GetExamModelNameFromPackItem(PPM.BookPackItem.BookPackItemID) AS ExamModelName, PPM.BookPackingOperation.PackagingTypeID, PPM.BookPackingOperation.PackingCalculationTypeID, 
                  PPM.BookPackingOperation.AllocatedFrom, PPM.BookPackingOperation.PackingValue, PPM.BookPackingOperation.PackageTotal, PPM.BookPackingOperation.PackingParentID, 
                  PPM.BookPrintingOperation.BookPrintingOperationID, PPM.BookPrintingOperation.ExamID, PPM.BookPrintingOperation.PrintsForOneModel, PPM.BookPrintingOperation.ExamsNeededForA3, 
                  PPM.BookPrintingOperation.ExamsNeededForA4, PPM.BookPrintingOperation.ExamsNeededForCD, PPM.BookPrintingOperation.OperationStatusID AS PackageOperationStatusID, 
                  PPM.BookPrintingOperation.TotalExamModels, PPM.BookPrintingOperation.TotalPacks, PPM.Exam.ExamCode, PPM.Exam.ExamSpecialityID, PPM.Exam.StudentGenderID, PPM.Exam.NumberofPages, 
                  PPM.Exam.NumberofSections, PPM.Exam.Notes, PPM.Exam.TimeForSection, PPM.Exam.IsActive, dbo.GetBookCountForBookPackItem(PPM.BookPackItem.BookPackItemID) AS BooksCount, 
                  PPM.PackagingType.ExamModelCount AS PackageTypeExamModelCount, PPM.PackagingType.BooksPerPackage AS PackageTypeBooksPerPackage, PPM.PackagingType.Total AS PackageTypeTotal
FROM     PPM.BookPackItem INNER JOIN
                  PPM.BookPackingOperation ON PPM.BookPackItem.BookPackingOperationID = PPM.BookPackingOperation.BookPackingOperationID INNER JOIN
                  PPM.BookPrintingOperation ON PPM.BookPackingOperation.BookPrintingOperationID = PPM.BookPrintingOperation.BookPrintingOperationID INNER JOIN
                  PPM.Exam ON PPM.BookPrintingOperation.ExamID = PPM.Exam.ExamID INNER JOIN
                  PPM.PackagingType ON PPM.BookPackingOperation.PackagingTypeID = PPM.PackagingType.PackagingTypeID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewBookPackItemComplete';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'Widths = 11
         Column = 1992
         Alias = 2088
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewBookPackItemComplete';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[26] 4[35] 2[20] 3) )"
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
         Top = -120
         Left = 0
      End
      Begin Tables = 
         Begin Table = "BookPackItem (PPM)"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 298
               Right = 319
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BookPackingOperation (PPM)"
            Begin Extent = 
               Top = 7
               Left = 367
               Bottom = 299
               Right = 634
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BookPrintingOperation (PPM)"
            Begin Extent = 
               Top = 7
               Left = 686
               Bottom = 256
               Right = 948
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Exam (PPM)"
            Begin Extent = 
               Top = 259
               Left = 682
               Bottom = 422
               Right = 900
            End
            DisplayFlags = 280
            TopColumn = 10
         End
         Begin Table = "PackagingType (PPM)"
            Begin Extent = 
               Top = 302
               Left = 48
               Bottom = 465
               Right = 261
            End
            DisplayFlags = 280
            TopColumn = 6
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
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
      Begin Column', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewBookPackItemComplete';





