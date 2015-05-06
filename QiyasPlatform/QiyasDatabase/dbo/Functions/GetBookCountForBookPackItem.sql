-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
Create FUNCTION [dbo].[GetBookCountForBookPackItem]
(
	-- Add the parameters for the function here
	@BookPackItemID int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result int, @ID int, @PackageTypeID int, @ParentID int

	
	select @ParentID = ParentID from [PPM].[BookPackItem] where BookPackItemID = @BookPackItemID
	if(@ParentID is null)
	begin
		select @ID = BookPackingOperationID from [PPM].[BookPackItem] where BookPackItemID = @BookPackItemID
		select @PackageTypeID =  [PackagingTypeID] from [PPM].[BookPackingOperation] where BookPackingOperationID = @ID
	end
	else
	begin
		select @ID = BookPackItemOperationID from [PPM].[BookPackItem] where BookPackItemID = @BookPackItemID
		select @PackageTypeID =  [PackagingTypeID] from [PPM].BookPackItemOperation where BookPackItemOperationID = @ID
	end
	
	
	-- Add the T-SQL statements to compute the return value here
	SELECT @Result =  Total from [PPM].PackagingType where PackagingTypeID = @PackageTypeID
	if(@Result is null)
		Set @Result = 0
	-- Return the result of the function
	RETURN @Result

END