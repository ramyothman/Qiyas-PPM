-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
Create FUNCTION [dbo].[GetExamModelNameForRequestWithdrawItems]
(
	-- Add the parameters for the function here
	@RequestWithdrawDetailItemID int
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result nvarchar(50)
	Declare @ID int
	-- Add the T-SQL statements to compute the return value here
	DECLARE @ExamModelItemID int, @ExamModelID int, @Count int, @Counter int, @Name nvarchar(50);

	select @Count = count(RequestWithdrawDetailItemID) from PPM.RequestWithdrawDetailItemModel where RequestWithdrawDetailItemID = @RequestWithdrawDetailItemID
	set @Counter = 1
	Set @Result = ''

	DECLARE ExamModel_Cursor CURSOR FOR
SELECT PPM.RequestWithdrawDetailItemModel.RequestWithdrawDetailItemID as ExamModelItemID, PPM.RequestWithdrawDetailItemModel.ExamModelID, Name 
FROM PPM.RequestWithdrawDetailItemModel inner join PPM.ExamModel on PPM.RequestWithdrawDetailItemModel.ExamModelID = PPM.ExamModel.ExamModelID
WHERE PPM.RequestWithdrawDetailItemModel.RequestWithdrawDetailItemID = @RequestWithdrawDetailItemID;
OPEN ExamModel_Cursor;
FETCH NEXT FROM ExamModel_Cursor INTO @ExamModelItemID, @ExamModelID, @Name;
WHILE @@FETCH_STATUS = 0
   BEGIN
	  Set @Result = @Result + @Name

	  if (@Counter < @Count)
	  begin
		Set @Result = @Result + ' - '
	  end
	  set @Counter = @Counter + 1
      FETCH NEXT FROM ExamModel_Cursor INTO @ExamModelItemID, @ExamModelID, @Name;
   END;
CLOSE ExamModel_Cursor;
DEALLOCATE ExamModel_Cursor;


	

	-- Return the result of the function
	RETURN @Result

END