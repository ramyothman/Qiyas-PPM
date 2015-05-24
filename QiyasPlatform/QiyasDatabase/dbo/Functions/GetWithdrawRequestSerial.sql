-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION GetWithdrawRequestSerial 
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
	SELECT @Result = max([RequestOrder]) from [PPM].[RequestWithdraw] where [ExamCenterRequiredExamsID] = @ID
	if(@Result is null)
		set @Result = 0
	set @Result = @Result + 1

	-- Return the result of the function
	RETURN @Result

END