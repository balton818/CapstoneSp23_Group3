CREATE PROCEDURE [dbo].[get_ingredients_for recipe]
  @recipeId INT
AS
  SELECT 
		 [ingredient_id]    AS IngredientId
		,[recipe_id]        AS RecipeId  
		,[ingredient_name]  AS IngredientName
		,[quantity]         AS Quantity
		,[unit_id]          AS UnitId
  FROM [dbo].[recipe_ingredients]
  WHERE recipe_id = @recipeId
