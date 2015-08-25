-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetShippingBookPackSerials]
(
	-- Add the parameters for the function here
	@ShippingBagID int,
	@PackagingTypeID int,
	@ExamID int,
	@ModelName nvarchar(50)
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result nvarchar(50)
	Declare @ID int
	-- Add the T-SQL statements to compute the return value here
	DECLARE @ShippingBagItemID int, @ShippingItemID int, @Count int, @Counter int, @Name nvarchar(50);

	select @Count = count(ShippingBagItemID) from dbo.ViewShippingBags where ShippingBagID = @ShippingBagID and PackagingTypeID = @PackagingTypeID and ExamID = @ExamID and ModelName = @ModelName

	set @Counter = 1
	Set @Result = ''

	DECLARE ShippingBag_Cursor CURSOR FOR
SELECT ShippingBagItemID, BookPackItemID, PackSerial 
From dbo.ViewShippingBags
WHERE ShippingBagID = @ShippingBagID and PackagingTypeID = @PackagingTypeID and ExamID = @ExamID and ModelName = @ModelName;
OPEN ShippingBag_Cursor;
FETCH NEXT FROM ShippingBag_Cursor INTO @ShippingBagItemID, @ShippingItemID, @Name;
WHILE @@FETCH_STATUS = 0
   BEGIN
	  Set @Result = @Result + @Name

	  if (@Counter < @Count)
	  begin
		Set @Result = @Result + ' - '
	  end
	  set @Counter = @Counter + 1
      FETCH NEXT FROM ShippingBag_Cursor INTO @ShippingBagItemID, @ShippingItemID, @Name;
   END;
CLOSE ShippingBag_Cursor;
DEALLOCATE ShippingBag_Cursor;


	

	-- Return the result of the function
	RETURN @Result

END