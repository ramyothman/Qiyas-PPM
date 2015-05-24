CREATE VIEW dbo.ViewWithdrawalReport
AS
SELECT PPM.RequestWithdrawDetail.RequestWithdrawDetailID, PPM.RequestWithdrawDetail.RequestWithdrawID, PPM.RequestWithdrawDetail.ExamRequirementItemID, PPM.RequestWithdrawDetail.PrintsForOneModel, 
                  PPM.RequestWithdrawDetailItem.PackagingTypeID, PPM.RequestWithdrawDetailItem.PackCount, PPM.PackagingType.Name AS PackageName, 
                  dbo.GetExamModelNameForRequestWithdrawItems(PPM.RequestWithdrawDetailItem.RequestWithdrawDetailItemID) AS ExamModel, PPM.RequestWithdraw.ExamCenterRequiredExamsID, 
                  PPM.ExamCenterRequiredExams.ExamPeriodID, PPM.ExamCenterRequiredExams.ExamCenterID, PPM.ExamCenterRequiredExams.RequestPreparationStatusID, PPM.ExamPeriod.Name AS PeriodName, 
                  PPM.ExamCenter.Name AS CenterName, PPM.ExamRequirementItem.ExamID, PPM.Exam.ExamCode
FROM     PPM.RequestWithdrawDetail INNER JOIN
                  PPM.RequestWithdrawDetailItem ON PPM.RequestWithdrawDetail.RequestWithdrawDetailID = PPM.RequestWithdrawDetailItem.RequestWithdrawDetailID INNER JOIN
                  PPM.PackagingType ON PPM.RequestWithdrawDetailItem.PackagingTypeID = PPM.PackagingType.PackagingTypeID INNER JOIN
                  PPM.RequestWithdraw ON PPM.RequestWithdrawDetail.RequestWithdrawID = PPM.RequestWithdraw.RequestWithdrawID INNER JOIN
                  PPM.ExamCenterRequiredExams ON PPM.RequestWithdraw.ExamCenterRequiredExamsID = PPM.ExamCenterRequiredExams.ExamCenterRequiredExamsID INNER JOIN
                  PPM.ExamPeriod ON PPM.ExamCenterRequiredExams.ExamPeriodID = PPM.ExamPeriod.ExamPeriodID INNER JOIN
                  PPM.ExamCenter ON PPM.ExamCenterRequiredExams.ExamCenterID = PPM.ExamCenter.ExaminationCenterID INNER JOIN
                  PPM.ExamRequirementItem ON PPM.RequestWithdrawDetail.ExamRequirementItemID = PPM.ExamRequirementItem.ExamRequirementItemID INNER JOIN
                  PPM.Exam ON PPM.ExamRequirementItem.ExamID = PPM.Exam.ExamID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewWithdrawalReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'= 48
               Bottom = 501
               Right = 283
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ExamRequirementItem (PPM)"
            Begin Extent = 
               Top = 378
               Left = 657
               Bottom = 541
               Right = 948
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Exam (PPM)"
            Begin Extent = 
               Top = 433
               Left = 334
               Bottom = 596
               Right = 552
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
      Begin ColumnWidths = 17
         Width = 284
         Width = 2256
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewWithdrawalReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[37] 4[3] 2[32] 3) )"
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
         Begin Table = "RequestWithdrawDetail (PPM)"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 388
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RequestWithdrawDetailItem (PPM)"
            Begin Extent = 
               Top = 6
               Left = 434
               Bottom = 182
               Right = 731
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PackagingType (PPM)"
            Begin Extent = 
               Top = 7
               Left = 779
               Bottom = 170
               Right = 992
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RequestWithdraw (PPM)"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 339
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ExamCenterRequiredExams (PPM)"
            Begin Extent = 
               Top = 175
               Left = 779
               Bottom = 338
               Right = 1070
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "ExamPeriod (PPM)"
            Begin Extent = 
               Top = 247
               Left = 387
               Bottom = 410
               Right = 596
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ExamCenter (PPM)"
            Begin Extent = 
               Top = 338
               Left ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewWithdrawalReport';

