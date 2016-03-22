CREATE VIEW [dbo].[ViewExamCenter]
AS
SELECT PPM.ExamCenter.ExaminationCenterID, PPM.ExamCenter.CenterCode, PPM.ExamCenter.Name, PPM.ExamCenter.CityID, PPM.ExamCenter.StudentGenderID, PPM.ExamCenter.IsActive, PPM.ExamCenter.CreatorID, 
                  PPM.ExamCenter.CreatedDate, PPM.ExamCenter.ModifiedByID, PPM.ExamCenter.ModifiedDate, Person.City.StateRegionID, Person.City.Name AS CityName, Person.StateProvince.Name AS StateProvinceName, 
                  Person.CountryRegion.Name AS CountryName
FROM     PPM.ExamCenter INNER JOIN
                  Person.City ON PPM.ExamCenter.CityID = Person.City.CityID INNER JOIN
                  Person.StateProvince ON Person.City.StateRegionID = Person.StateProvince.StateProvinceId INNER JOIN
                  Person.CountryRegion ON Person.StateProvince.CountryRegionCode = Person.CountryRegion.CountryRegionCode

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
         Begin Table = "ExamCenter (PPM)"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 299
               Right = 283
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "City (Person)"
            Begin Extent = 
               Top = 7
               Left = 331
               Bottom = 247
               Right = 525
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "StateProvince (Person)"
            Begin Extent = 
               Top = 7
               Left = 572
               Bottom = 258
               Right = 801
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CountryRegion (Person)"
            Begin Extent = 
               Top = 7
               Left = 849
               Bottom = 148
               Right = 1077
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
      Begin ColumnWidths = 11
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
       ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewExamCenter';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'  Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewExamCenter';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewExamCenter';

