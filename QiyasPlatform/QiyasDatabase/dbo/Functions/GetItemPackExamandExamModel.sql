-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION GetItemPackExamandExamModel
(
	-- Add the parameters for the function here
	@ItemPackID int
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result nvarchar(50)
	DECLARE @ExamCode nvarchar(50)
	DECLARE @ExamModel nvarchar(50)
	Declare @ExamModelCode nvarchar(50)
	declare @au_id int
	-- Add the T-SQL statements to compute the return value here
	SELECT @ExamCode = ExamCode from  dbo.ViewBookPackItem where [BookPackItemID] = @ItemPackID

	select @au_id = min( BookPackItemModelID ) from PPM.BookPackItemModel where BookPackItemID = @ItemPackID
	Set @Result = ''
	while @au_id is not null
	begin
		select @ExamModel = ExamModelID from PPM.BookPackItemModel where BookPackItemModelID = @au_id
		select @ExamModelCode = Name from [PPM].[ExamModel] where [ExamModelID] = @ExamModel
		select @au_id = min( BookPackItemModelID ) from PPM.BookPackItemModel where BookPackItemID = @ItemPackID and BookPackItemModelID > @au_id
		set @Result = @Result + @ExamModelCode 
		if(@au_id is not null)
			Set @Result = @Result + '-'
	end
	-- Return the result of the function
	set @Result = @ExamCode + '/' + @Result
	RETURN @Result

END