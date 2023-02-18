﻿CREATE PROCEDURE [dbo].[get_pantry_item]
	@pantryId INT
AS
	SELECT
		pantry_id 			as PantryId,
		user_id 			as UserId,
		ingredient_name 	as IngredientName,
		quantity 			as Quantity,
		unit_id				as UnitId
	FROM [dbo].pantry
	WHERE pantry_id = @pantryId
