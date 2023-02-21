CREATE PROCEDURE [dbo].[get_cuisines] AS
	SELECT 
		cuisine AS Value 
	FROM [dbo].[cuisine]