-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION GetExamModelName
(
	-- Add the parameters for the function here
	@ExamID int
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result nvarchar(50)
	Declare @ID int
	-- Add the T-SQL statements to compute the return value here
	DECLARE @ExamModelItemID int, @ExamModelID int, @Count int, @Counter int, @Name nvarchar(50);

	select @Count = count(ExamModelItemID) from PPM.ExamModelItem where ExamID = @ExamID
	set @Counter = 1
	Set @Result = ''

	DECLARE ExamModel_Cursor CURSOR FOR
SELECT PPM.ExamModelItem.ExamModelItemID, PPM.ExamModelItem.ExamModelID, Name 
FROM PPM.ExamModelItem inner join PPM.ExamModel on PPM.ExamModelItem.ExamModelID = PPM.ExamModel.ExamModelID
WHERE PPM.ExamModelItem.ExamID = @ExamID;
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