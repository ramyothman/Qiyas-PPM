-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION GetItemPackSpeciality
(
	-- Add the parameters for the function here
	@ItemPackID int
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result nvarchar(50)

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = SpecialityName from  ViewBookPackItem where [BookPackItemID] = @ItemPackID

	-- Return the result of the function
	RETURN @Result

END