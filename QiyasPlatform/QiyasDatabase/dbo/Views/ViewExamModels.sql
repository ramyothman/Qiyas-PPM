CREATE VIEW [dbo].[ViewExamModels]
AS
SELECT PPM.Exam.ExamID, PPM.Exam.ExamCode, PPM.Exam.Name AS ExamName, PPM.ExamModelItem.ExamModelID, PPM.ExamModel.Name AS ExamModel
FROM     PPM.ExamModel INNER JOIN
                  PPM.ExamModelItem ON PPM.ExamModel.ExamModelID = PPM.ExamModelItem.ExamModelID INNER JOIN
                  PPM.Exam ON PPM.ExamModelItem.ExamID = PPM.Exam.ExamID
GO



GO


