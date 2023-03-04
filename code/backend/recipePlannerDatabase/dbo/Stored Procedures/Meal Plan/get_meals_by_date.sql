CREATE PROCEDURE [dbo].[get_meals_by_date]
	@date DATE
AS
	DECLARE @firstOfWeek DATE = get_first_of_week(@date)
	DECLARE @mealPlanId INT = 	(SELECT 
									meal_plan_id
								FROM [dbo].[meal_plan]
								WHERE @firstOfWeek = meal_plan_date)
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
