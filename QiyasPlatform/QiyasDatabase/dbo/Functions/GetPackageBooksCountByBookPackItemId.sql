-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION GetPackageBooksCountByBookPackItemId
(
	-- Add the parameters for the function here
	@BookPackItemID int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result int

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result  = [BooksPerPackage] from [PPM].[PackagingType] 
	where [PackagingTypeID] in (select [PackagingTypeID] from [PPM].[BookPackingOperation] 
	where [BookPackingOperationID] in (select [BookPackingOperationID] from [PPM].[BookPackItem]
	where [BookPackItemID] = @BookPackItemID))
					

	-- Return the result of the function
	RETURN @Result

END