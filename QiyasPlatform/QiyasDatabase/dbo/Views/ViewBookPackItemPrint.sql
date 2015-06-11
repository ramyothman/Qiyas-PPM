CREATE VIEW dbo.ViewBookPackItemPrint
AS
SELECT PPM.BookPackItem.BookPackItemID, PPM.BookPackItem.BookPackingOperationID, PPM.BookPackItem.PackCode, PPM.BookPackItem.PackSerial, PPM.BookPackItem.Weight, PPM.BookPackItem.OperationStatusID, 
                  PPM.BookPackItem.ParentID, dbo.GetItemPackSpeciality(PPM.BookPackItem.BookPackItemID) AS Speciality, dbo.GetItemPackExamandExamModel(PPM.BookPackItem.BookPackItemID) AS ModelandNumber, 
                  PPM.BookPackingOperation.BookPrintingOperationID, CASE WHEN ParentID IS NULL THEN PPM.PackagingType.Name ELSE pt1.Name END AS PackageTypeName, PPM.BookPackItem.StartBookSerial, 
                  PPM.BookPackItem.LastBookSerial, PPM.BookPackingOperation.PackagingTypeID, pio.PackagingTypeID AS ChildPackagingTypeID, PPM.PackagingType.ExamModelCount, PPM.PackagingType.BooksPerPackage, 
                  pt1.ExamModelCount AS ChildExamModelCount, pt1.BooksPerPackage AS ChildBooksPerPackage
FROM     PPM.BookPackItem INNER JOIN
                  PPM.BookPackingOperation ON PPM.BookPackItem.BookPackingOperationID = PPM.BookPackingOperation.BookPackingOperationID INNER JOIN
                  PPM.PackagingType ON PPM.BookPackingOperation.PackagingTypeID = PPM.PackagingType.PackagingTypeID LEFT OUTER JOIN
                  PPM.BookPackItemOperation AS pio ON pio.BookPackItemOperationID = PPM.BookPackItem.BookPackItemOperationID LEFT OUTER JOIN
                  PPM.PackagingType AS pt1 ON pt1.PackagingTypeID = pio.PackagingTypeID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewBookPackItemPrint';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[10] 3) )"
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
         Top = -240
         Left = 0
      End
      Begin Tables = 
         Begin Table = "BookPackItem (PPM)"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 310
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BookPackingOperation (PPM)"
            Begin Extent = 
               Top = 7
               Left = 358
               Bottom = 293
               Right = 625
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "PackagingType (PPM)"
            Begin Extent = 
               Top = 7
               Left = 673
               Bottom = 170
               Right = 886
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pio"
            Begin Extent = 
               Top = 192
               Left = 63
               Bottom = 413
               Right = 334
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "pt1"
            Begin Extent = 
               Top = 175
               Left = 673
               Bottom = 338
               Right = 886
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
      Begin ColumnWidths = 16
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
         Width = 1200
         Width = 1200
         Width = 1200
  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewBookPackItemPrint';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'       Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewBookPackItemPrint';



