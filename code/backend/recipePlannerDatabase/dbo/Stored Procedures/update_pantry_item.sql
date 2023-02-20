CREATE PROCEDURE [dbo].[update_pantry_item]
	@pantryId INT,
	@ingredientId INT = NULL,
	@ingredientName NVARCHAR(40),
	@quantity INT,
	@unitId INT
AS
	UPDATE [dbo].[pantry]
	SET ingredient_name 	= @ingredientName,
		ingredient_id		= @ingredientId, 
		quantity 			= @quantity,
		unit_id 			= @unitId
	WHERE pantry_id = @pantryId

	EXEC [dbo].[get_pantry_item] @pantryId

