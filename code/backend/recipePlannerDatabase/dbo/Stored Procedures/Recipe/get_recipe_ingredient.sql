CREATE PROCEDURE [dbo].[get_recipe_ingredient]
  @recipeIngredientId INT
AS
  SELECT 
		 [ingredient_id]    AS IngredientId
		,[recipe_id]        AS RecipeId  
		,[ingredient_name]  AS IngredientName
		,[quantity]         AS Quantity
		,[unit_id]          AS UnitId
  FROM [dbo].[recipe_ingredients]
  WHERE ingredient_id = @recipeIngredientId
