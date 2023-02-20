CREATE PROCEDURE [dbo].[update_pantry_item]
	@pantryId INT,
	@ingredientName NVARCHAR(40),
	@quantity INT
AS
	UPDATE [dbo].[pantry]
	SET ingredient_name 	= @ingredientName, 
		quantity 			= @quantity
	WHERE pantry_id = @pantryId

	EXEC [dbo].[get_pantry_item] @pantryId

