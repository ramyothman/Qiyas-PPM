-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION GetExamModelNameFromPackItem
(
	-- Add the parameters for the function here
	@BookPackItemID int
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result nvarchar(50)
	Declare @ID int
	-- Add the T-SQL statements to compute the return value here
	DECLARE @BookPackItemModelID int, @BookPackItemTempID int, @Count int, @Counter int, @Name nvarchar(50);

	select @Count = count(BookPackItemModelID) from PPM.BookPackItemModel where BookPackItemID = @BookPackItemID
	set @Counter = 1
	Set @Result = ''

	DECLARE ExamModel_Cursor CURSOR FOR
SELECT PPM.BookPackItemModel.BookPackItemModelID, Name 
FROM PPM.BookPackItemModel inner join PPM.ExamModel on PPM.BookPackItemModel.ExamModelID = PPM.ExamModel.ExamModelID
WHERE PPM.BookPackItemModel.BookPackItemID = @BookPackItemID;
OPEN ExamModel_Cursor;
FETCH NEXT FROM ExamModel_Cursor INTO @BookPackItemModelID, @Name;
WHILE @@FETCH_STATUS = 0
   BEGIN
	  Set @Result = @Result + @Name

	  if (@Counter < @Count)
	  begin
		Set @Result = @Result + ' - '
	  end
	  set @Counter = @Counter + 1
      FETCH NEXT FROM ExamModel_Cursor INTO @BookPackItemModelID, @Name;
   END;
CLOSE ExamModel_Cursor;
DEALLOCATE ExamModel_Cursor;


	

	-- Return the result of the function
	RETURN @Result

END