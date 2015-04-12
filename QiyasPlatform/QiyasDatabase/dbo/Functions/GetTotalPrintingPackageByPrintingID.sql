-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION GetTotalPrintingPackageByPrintingID
(
	-- Add the parameters for the function here
	@ID int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result int

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = SUM(PackageTotal) from [PPM].[BookPackingOperation] where [BookPrintingOperationID] = @ID
	if(@Result is null)
		Set @Result = 0
	-- Return the result of the function
	RETURN @Result

END