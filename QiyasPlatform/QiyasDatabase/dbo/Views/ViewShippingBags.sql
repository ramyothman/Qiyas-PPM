CREATE VIEW dbo.ViewShippingBags
AS
SELECT PPM.ShippingBag.ShippingBagID, PPM.ShippingBag.ExamCenterRequiredExamsID, PPM.ShippingBag.ShippingBagCode, PPM.ShippingBag.ShippingBagSerial, PPM.ShippingBag.BookCount, 
                  PPM.ShippingBag.PackCount, PPM.ShippingBagItem.BookPackItemID, PPM.BookPackItem.BookPackingOperationID, PPM.BookPackItem.PackCode, PPM.BookPackItem.PackSerial, PPM.BookPackItem.Weight, 
                  PPM.BookPackItem.OperationStatusID, PPM.BookPackItem.ParentID, PPM.BookPackItem.StartBookSerial, PPM.BookPackItem.LastBookSerial, PPM.BookPackItem.ParentBookPackItemID, 
                  PPM.BookPackItem.BookPackItemOperationID, PPM.BookPackingOperation.BookPrintingOperationID, PPM.BookPackingOperation.PackagingTypeID, PPM.PackagingType.Name AS PackagingName, 
                  PPM.BookPackingOperation.PackingValue, PPM.BookPackingOperation.PackageTotal, PPM.BookPackingOperation.NumberofBooksPerModel, PPM.BookPackingOperation.PackageTotalPerModel, 
                  PPM.Exam.ExamCode, PPM.ExamCenter.CenterCode, PPM.ExamCenter.Name AS CenterName, PPM.ExamPeriod.Name AS ExamPeriodName, PPM.ExamPeriod.ExamYear, PPM.Exam.Name AS ExamName, 
                  dbo.GetExamModelNameFromPackItem(PPM.BookPackItem.BookPackItemID) AS ModelName, PPM.Exam.ExamID, PPM.ShippingBagItem.ShippingBagItemID, PPM.PackagingType.BooksPerPackage, 
                  PPM.PackagingType.ExamModelCount, PPM.ExamCenter.ExaminationCenterID
FROM     PPM.ShippingBag INNER JOIN
                  PPM.ShippingBagItem ON PPM.ShippingBag.ShippingBagID = PPM.ShippingBagItem.ShippingBagID INNER JOIN
                  PPM.BookPackItem ON PPM.ShippingBagItem.BookPackItemID = PPM.BookPackItem.BookPackItemID INNER JOIN
                  PPM.BookPackingOperation ON PPM.BookPackItem.BookPackingOperationID = PPM.BookPackingOperation.BookPackingOperationID INNER JOIN
                  PPM.PackagingType ON PPM.BookPackingOperation.PackagingTypeID = PPM.PackagingType.PackagingTypeID INNER JOIN
                  PPM.BookPrintingOperation ON PPM.BookPackingOperation.BookPrintingOperationID = PPM.BookPrintingOperation.BookPrintingOperationID INNER JOIN
                  PPM.Exam ON PPM.BookPrintingOperation.ExamID = PPM.Exam.ExamID INNER JOIN
                  PPM.ExamCenterRequiredExams ON PPM.ShippingBag.ExamCenterRequiredExamsID = PPM.ExamCenterRequiredExams.ExamCenterRequiredExamsID INNER JOIN
                  PPM.ExamCenter ON PPM.ExamCenterRequiredExams.ExamCenterID = PPM.ExamCenter.ExaminationCenterID INNER JOIN
                  PPM.ExamPeriod ON PPM.ExamCenterRequiredExams.ExamPeriodID = PPM.ExamPeriod.ExamPeriodID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewShippingBags';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' Bottom = 339
               Right = 1161
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ExamCenterRequiredExams (PPM)"
            Begin Extent = 
               Top = 358
               Left = 331
               Bottom = 521
               Right = 622
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ExamCenter (PPM)"
            Begin Extent = 
               Top = 344
               Left = 48
               Bottom = 507
               Right = 283
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ExamPeriod (PPM)"
            Begin Extent = 
               Top = 330
               Left = 642
               Bottom = 525
               Right = 851
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
      Begin ColumnWidths = 32
         Width = 284
         Width = 1200
         Width = 2724
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
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1560
         Width = 1800
         Width = 1236
         Width = 1248
         Width = 2244
         Width = 2016
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewShippingBags';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[36] 4[6] 2[16] 3) )"
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
         Top = -360
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ShippingBag (PPM)"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 339
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ShippingBagItem (PPM)"
            Begin Extent = 
               Top = 7
               Left = 387
               Bottom = 160
               Right = 670
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BookPackItem (PPM)"
            Begin Extent = 
               Top = 2
               Left = 727
               Bottom = 165
               Right = 998
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BookPackingOperation (PPM)"
            Begin Extent = 
               Top = 195
               Left = 327
               Bottom = 358
               Right = 594
            End
            DisplayFlags = 280
            TopColumn = 11
         End
         Begin Table = "PackagingType (PPM)"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 261
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "BookPrintingOperation (PPM)"
            Begin Extent = 
               Top = 186
               Left = 651
               Bottom = 349
               Right = 913
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Exam (PPM)"
            Begin Extent = 
               Top = 176
               Left = 943
              ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewShippingBags';





