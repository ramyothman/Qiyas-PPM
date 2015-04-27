-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION GetTotalBookPrintPackages 
(
	-- Add the parameters for the function here
	@PrintID int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result int

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = count(BookPackItemID) From [PPM].[BookPackItem] where  [BookPackingOperationID] 
					in (select [BookPackingOperationID] from [PPM].[BookPackingOperation] where [BookPrintingOperationID] = @PrintID)

	-- Return the result of the function
	RETURN @Result

END