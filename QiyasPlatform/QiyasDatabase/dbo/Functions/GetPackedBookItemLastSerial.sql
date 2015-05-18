-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetPackedBookItemLastSerial] 
(
	-- Add the parameters for the function here
	@BookPackItemID int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result int
	Declare @SubBookPackItemID int
	Declare @InitialSerial int

	select @InitialSerial = [StartBookSerial] from [PPM].[BookPackItem] WHERE [BookPackItemID] = @BookPackItemID
	if @InitialSerial is null
		set @InitialSerial = 0
	-- Add the T-SQL statements to compute the return value here
	SELECT @SubBookPackItemID = Max(BookPackItemID) from [PPM].[BookPackItem] where ParentBookPackItemID = @BookPackItemID
	select @Result = [LastBookSerial] from [PPM].[BookPackItem] where BookPackItemID = @SubBookPackItemID
	if @Result is null
		set @Result = @InitialSerial

	-- Return the result of the function
	RETURN @Result

END