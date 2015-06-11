-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION HaveA3Packs 
(
	-- Add the parameters for the function here
	@ID int
)
RETURNS bit
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result bit
	DECLARE @PackageTypeID int
	DECLARE @BookPackingOperationID int
	-- Add the T-SQL statements to compute the return value here
	SELECT @PackageTypeID = [PackagingTypeID] from [PPM].[PackagingType] where ExamModelCount = 1 and BooksPerPackage = 3
	SELECT @BookPackingOperationID =   [BookPackingOperationID] from [PPM].[BookPackingOperation] where [PackagingTypeID] = @PackageTypeID and [BookPrintingOperationID] = @ID
	
	if(@BookPackingOperationID is null)
		Set @Result = 0
	else
		Set @Result = 1

	-- Return the result of the function
	RETURN @Result

END