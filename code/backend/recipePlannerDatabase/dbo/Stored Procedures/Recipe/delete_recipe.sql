CREATE PROCEDURE [dbo].[delete_recipe]
	@recipeId INT
AS
	DELETE FROM [dbo].[recipe] WHERE recipe_id = @recipeId
