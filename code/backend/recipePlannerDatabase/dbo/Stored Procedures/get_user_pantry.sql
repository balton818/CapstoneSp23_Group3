CREATE PROCEDURE [dbo].[get_user_pantry]
	@userId INT
AS
	SELECT
		pantry_id 			as PantryId,
		user_id 			as UserId,
		ingredient_name 	as IngredientName,
		quantity 			as Quantity
	FROM [dbo].pantry
	WHERE user_id = @userId
