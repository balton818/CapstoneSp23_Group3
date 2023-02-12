CREATE PROCEDURE [dbo].[add_pantry_item]
	@userId INT,
	@ingredientName NVARCHAR(20),
	@quantity INT
AS
	INSERT INTO [dbo].[pantry](user_id, [ingredient_name], quantity) VALUES (@userId, @ingredientName, @quantity);
	DECLARE @pantryId INT = SCOPE_IDENTITY();
	EXEC [dbo].get_pantry_item @pantryId;
