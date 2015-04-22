-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[DeleteBookPackingOperationByBookPrintingID]
	-- Add the parameters for the stored procedure here
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete From PPM.ShippingBagItem where BookPackItemID in (select BookPackItemID from [PPM].[BookPackItem] where BookPackingOperationID IN (select BookPackingOperationID From [PPM].[BookPackingOperation] where BookPrintingOperationID = @ID))
	Delete From PPM.BookPackItemOperation where BookPackItemID in (select BookPackItemID from [PPM].[BookPackItem] where BookPackingOperationID IN (select BookPackingOperationID From [PPM].[BookPackingOperation] where BookPrintingOperationID = @ID))
	Delete From PPM.BookPackItemModel where BookPackItemID in (select BookPackItemID from [PPM].[BookPackItem] where BookPackingOperationID IN (select BookPackingOperationID From [PPM].[BookPackingOperation] where BookPrintingOperationID = @ID))
	Delete From PPM.ContainerRequestPack where BookPackItemID in (select BookPackItemID from [PPM].[BookPackItem] where BookPackingOperationID IN (select BookPackingOperationID From [PPM].[BookPackingOperation] where BookPrintingOperationID = @ID))
	Delete From [PPM].[BookPackingOperation] where BookPrintingOperationID = @ID
	
	Update [PPM].[BookPrintingOperation] set [OperationStatusID] = 1 where [BookPrintingOperationID] = @ID
END