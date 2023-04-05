CREATE PROCEDURE [dbo].[get_meal_plan_api_recipes]
  @mealPlanId INT
AS
  SELECT api_id AS Id
  FROM meal_plan mp
  JOIN meal m on m.meal_plan_id = mp.meal_plan_id
  JOIN recipe r on r.recipe_id = m.recipe_id
  WHERE m.meal_plan_id = @mealPlanId
  
