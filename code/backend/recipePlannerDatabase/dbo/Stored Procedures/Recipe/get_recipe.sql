CREATE PROCEDURE [dbo].[get_recipe]
	@recipeId INT
AS
	SELECT
		recipe_id 		AS recipeId
		, title
		, api_id		AS apiId
		, image_url		AS imageUrl
		, image_type 	AS imageType
	FROM [dbo].[recipe]
	WHERE recipe_id = @recipeId
