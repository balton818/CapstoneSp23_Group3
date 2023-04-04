CREATE PROCEDURE [dbo].[delete_recipe_ingredient]
  @recipeIngredientId INT
AS
  DELETE FROM [dbo].[recipe_ingredients]
    WHERE recipe_ingredient_id = @recipeIngredientId
