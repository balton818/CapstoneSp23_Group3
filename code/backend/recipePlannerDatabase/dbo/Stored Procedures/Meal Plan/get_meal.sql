CREATE PROCEDURE [dbo].[get_meal]
	@mealId INT
AS
	SELECT
		m.meal_id 			AS MealId,
		m.meal_plan_id 		AS MealPlanId,
		m.day_of_week		AS DayOfWeek,	
		m.meal_type 		AS MealType,
		r.recipe_id 		AS RecipeId,
		r.api_id			AS Api_id,
		r.title				AS Title,
		r.image_url 		AS ImageUrl,
		r.image_type 		AS ImageType
	FROM meal m
	JOIN recipe r on r.recipe_id = m.recipe_id
	WHERE meal_id = @mealId
