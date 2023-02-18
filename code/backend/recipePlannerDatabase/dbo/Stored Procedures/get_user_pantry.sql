CREATE PROCEDURE [dbo].[get_user_pantry]
	@userId INT
AS
	SELECT
		pantry_id 			AS PantryId,
		user_id 			AS UserId,
		ingredient_id		AS IngredientId,
		ingredient_name 	AS IngredientName,
		quantity 			AS Quantity,
		unit_id				AS UnitId
	FROM [dbo].pantry
	WHERE user_id = @userId
