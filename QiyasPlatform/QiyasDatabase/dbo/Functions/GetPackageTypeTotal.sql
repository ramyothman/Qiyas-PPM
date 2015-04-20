-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION GetPackageTypeTotal 
(
	-- Add the parameters for the function here
	 @PackageTypeID int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result int

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result =  Total from [PPM].[PackagingType] as pt where pt.PackagingTypeID = @PackageTypeID
	if(@Result is null)
		Set @Result = 0
	-- Return the result of the function
	RETURN @Result

END