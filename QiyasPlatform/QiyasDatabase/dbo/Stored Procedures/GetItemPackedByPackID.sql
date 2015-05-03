-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GetItemPackedByPackID] 
	-- Add the parameters for the stored procedure here
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * From [PPM].[BookPackItemOperation] where BookPackItemID = @ID and PackingParentID
	in (select BookPackItemOperationID from [PPM].[BookPackItem] where ParentID = @ID )
END