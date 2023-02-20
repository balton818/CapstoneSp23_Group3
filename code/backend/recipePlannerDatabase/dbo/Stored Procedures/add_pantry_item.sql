CREATE PROCEDURE [dbo].[add_pantry_item]
	@userId INT,
	@ingredientName NVARCHAR(40),
	@ingredientId INT = NULL,
	@quantity INT,
	@unitId INT
AS
	INSERT INTO [dbo].[pantry](user_id, ingredient_id, [ingredient_name], quantity, unit_id) VALUES (@userId, @ingredientId, @ingredientName, @quantity, @unitId);
	DECLARE @pantryId INT = SCOPE_IDENTITY();
	EXEC [dbo].get_pantry_item @pantryId;
