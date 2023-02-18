CREATE PROCEDURE [dbo].[search_ingredient]
    @search NVARCHAR(40) = ''
AS
    DECLARE @formattedSearch NVARCHAR(43) = '%' + @search + '%'
    SELECT 
        ingredient_id AS IngredientId,
        ingredient_name AS IngredientName
    FROM [dbo].[ingredient]
    WHERE ingredient_name LIKE @formattedSearch

