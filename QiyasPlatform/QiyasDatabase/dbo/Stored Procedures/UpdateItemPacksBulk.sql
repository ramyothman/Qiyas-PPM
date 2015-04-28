-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE UpdateItemPacksBulk
	-- Add the parameters for the stored procedure here
	@PrintID int, 
	@StatusID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update [PPM].[BookPackItem] set [OperationStatusID] = @StatusID
	where BookPackingOperationID in (select BookPackingOperationID from [PPM].[BookPackingOperation]
	where [BookPrintingOperationID] = @PrintID)
END