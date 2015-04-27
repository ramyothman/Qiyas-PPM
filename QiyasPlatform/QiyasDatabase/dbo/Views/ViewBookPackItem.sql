CREATE VIEW dbo.ViewBookPackItem
AS
SELECT PPM.BookPackItem.BookPackItemID, PPM.BookPackItem.BookPackingOperationID, PPM.BookPackItem.PackCode, PPM.BookPackItem.PackSerial, PPM.BookPackItem.Weight, PPM.BookPackItem.ParentID, 
                  PPM.BookPackItem.OperationStatusID, PPM.BookPrintingOperation.ExamID, PPM.BookPrintingOperation.BookPrintingOperationID, PPM.BookPackingOperation.PackagingTypeID, 
                  PPM.BookPackingOperation.PackingCalculationTypeID, PPM.BookPackingOperation.PackageTotal, PPM.BookPackingOperation.PackingValue, PPM.BookPackingOperation.AllocatedFrom, 
                  PPM.BookPrintingOperation.PrintsForOneModel, PPM.BookPrintingOperation.ExamsNeededForA3, PPM.Exam.ExamCode, PPM.Exam.ExamSpecialityID, PPM.ExamSpeciality.Name AS SpecialityName
FROM     PPM.BookPackItem INNER JOIN
                  PPM.BookPackingOperation ON PPM.BookPackItem.BookPackingOperationID = PPM.BookPackingOperation.BookPackingOperationID INNER JOIN
                  PPM.BookPrintingOperation ON PPM.BookPackingOperation.BookPrintingOperationID = PPM.BookPrintingOperation.BookPrintingOperationID INNER JOIN
                  PPM.Exam ON PPM.BookPrintingOperation.ExamID = PPM.Exam.ExamID INNER JOIN
                  PPM.ExamSpeciality ON PPM.Exam.ExamSpecialityID = PPM.ExamSpeciality.ExamSpecialityID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewBookPackItem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'idths = 11
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewBookPackItem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[47] 4[14] 2[20] 3) )"
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
         Begin Table = "BookPackItem (PPM)"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 310
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "BookPackingOperation (PPM)"
            Begin Extent = 
               Top = 7
               Left = 358
               Bottom = 170
               Right = 625
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "BookPrintingOperation (PPM)"
            Begin Extent = 
               Top = 7
               Left = 673
               Bottom = 170
               Right = 935
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Exam (PPM)"
            Begin Extent = 
               Top = 180
               Left = 833
               Bottom = 343
               Right = 1051
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ExamSpeciality (PPM)"
            Begin Extent = 
               Top = 205
               Left = 480
               Bottom = 368
               Right = 690
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
      Begin ColumnW', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewBookPackItem';

