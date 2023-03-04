CREATE PROCEDURE [dbo].[get_meal_plan_meals]
	@mealPlanId INT
AS
	SELECT
		m.meal_id 			AS mealId,
		m.meal_plan_id 		AS mealPlanId,
		m.day_of_week		AS dayOfWeek,	
		m.meal_type 		AS mealType,
		r.recipe_id 		AS recipeId,
		r.api_id			AS api_id,
		r.title,
		r.image_url 		AS imageUrl,
		r.image_type 		AS imageType
	FROM meal m
	JOIN recipe r on r.recipe_id = m.recipe_id
	WHERE meal_plan_id = @mealPlanId
