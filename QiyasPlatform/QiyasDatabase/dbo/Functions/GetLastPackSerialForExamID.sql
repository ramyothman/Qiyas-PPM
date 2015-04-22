-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION GetLastPackSerialForExamID 
(
	-- Add the parameters for the function here
	@ExamID int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result int

	select @Result = Max(PackSerial) from [PPM].[BookPackItem] as bi inner join [PPM].[BookPackingOperation]  as bo
	on  bi.BookPackingOperationID = bo.BookPackingOperationID inner join [PPM].[BookPrintingOperation] as bp
	on bo.BookPrintingOperationID = bp.BookPrintingOperationID
	where ExamID = @ExamID
	-- Add the T-SQL statements to compute the return value here
	if(@Result is null)
		Set @Result = 0

	-- Return the result of the function
	RETURN @Result

END