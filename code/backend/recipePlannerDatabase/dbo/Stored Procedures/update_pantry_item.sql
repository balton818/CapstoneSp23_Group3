CREATE PROCEDURE [dbo].[update_pantry_item]
	@pantryId INT,
	@ingredientName NVARCHAR(40),
	@quantity INT,
	@unitId INT
AS
	UPDATE [dbo].[pantry]
	SET ingredient_name 	= @ingredientName, 
		quantity 			= @quantity,
		unit_id 			= @unitId
	WHERE pantry_id = @pantryId

	EXEC [dbo].[get_pantry_item] @pantryId

