CREATE PROCEDURE [dbo].[remove_pantry_item]
	@pantryId INT
AS
	DELETE FROM [dbo].[pantry] where pantry_id = @pantryId
