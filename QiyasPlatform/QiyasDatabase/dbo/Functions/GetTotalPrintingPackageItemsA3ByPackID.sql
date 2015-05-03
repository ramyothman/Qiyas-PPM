-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
create FUNCTION [dbo].[GetTotalPrintingPackageItemsA3ByPackID]
(
	-- Add the parameters for the function here
	@ID int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result int
	
	Declare @PackageTypeID int
	Select @PackageTypeID = PackagingTypeID from [PPM].[PackagingType] where BooksPerPackage = 3 and ExamModelCount = 1
	
	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = SUM(PackageTotal * dbo.GetPackageTypeTotal([PackagingTypeID]) ) from [PPM].BookPackItemOperation where BookPackItemID = @ID and [PackagingTypeID] = @PackageTypeID and [PackingCalculationTypeID] <> 2
	if(@Result is null)
		Set @Result = 0
	-- Return the result of the function
	RETURN @Result

END