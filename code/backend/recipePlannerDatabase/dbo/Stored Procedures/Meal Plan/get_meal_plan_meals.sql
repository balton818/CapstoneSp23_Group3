CREATE PROCEDURE [dbo].[get_meal_plan_meals]
	@mealPlanId INT
AS
	SELECT
		m.meal_id 			AS MealId,
		m.meal_plan_id 		AS MealPlanId,
		m.day_of_week		AS DayOfWeek,
		m.meal_type 		AS MealType,
		r.recipe_id 		AS RecipeId,
		r.api_id			AS ApiId,
		r.title				AS Title,
		r.image_url 		AS ImageUrl,
		r.image_type 		AS ImageType,
		DATEADD(day, m.day_of_week - 1, mp.start_date)	AS DATE
	FROM meal m
	JOIN recipe r on r.recipe_id = m.recipe_id
	JOIN meal_plan mp on mp.meal_plan_id = m.meal_plan_id
	WHERE m.meal_plan_id = @mealPlanId
