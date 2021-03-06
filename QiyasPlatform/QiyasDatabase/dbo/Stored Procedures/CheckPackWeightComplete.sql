﻿-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CheckPackWeightComplete] 
	-- Add the parameters for the stored procedure here
	@PrintID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @Result int
	Declare @PrintRequestStatus int
    -- Insert statements for procedure here
	SELECT @Result = count(BookPackItemID) from [PPM].[BookPackItem] where [BookPackingOperationID] 
					in (select [BookPackingOperationID] from [PPM].[BookPackingOperation] where [BookPrintingOperationID] = @PrintID) and 
					[Weight] is null
	

	if(@Result is null OR @Result = 0)
	begin
		select @PrintRequestStatus = [OperationStatusID] from [PPM].[BookPrintingOperation] where [BookPrintingOperationID] = @PrintID
		if(@PrintRequestStatus <= 3)
			Update [PPM].[BookPrintingOperation] set [OperationStatusID] = 4 where [BookPrintingOperationID] = @PrintID
	end
	else 
	begin
	SELECT @Result = count(BookPackItemID) from [PPM].[BookPackItem] where [BookPackingOperationID] 
					in (select [BookPackingOperationID] from [PPM].[BookPackingOperation] where [BookPrintingOperationID] = @PrintID) and 
					[Weight] is not null
		if(@Result > 0)
		begin
			select @PrintRequestStatus = [OperationStatusID] from [PPM].[BookPrintingOperation] where [BookPrintingOperationID] = @PrintID
			if(@PrintRequestStatus <= 2)
				Update [PPM].[BookPrintingOperation] set [OperationStatusID] = 3 where [BookPrintingOperationID] = @PrintID
		end
	end
END