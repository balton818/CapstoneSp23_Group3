CREATE PROCEDURE [dbo].[get_recipe]
	@recipeId INT = NULL,
	@apiId INT = NULL
AS

	SELECT TOP 1
		recipe_id 		AS recipeId
		, title
		, api_id		AS apiId
		, image_url		AS imageUrl
		, image_type 	AS imageType
	FROM [dbo].[recipe]
	WHERE (recipe_id IS NOT NULL AND recipe_id = @recipeId) 
		OR (api_id IS NOT NULL AND api_id = @apiId)
