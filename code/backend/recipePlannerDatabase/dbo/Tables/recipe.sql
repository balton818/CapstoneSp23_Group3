CREATE TABLE [dbo].[recipe]
(
	[recipe_id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [title] NVARCHAR(150) NOT NULL, 
    [api_id] INT NOT NULL,
    [image_url] NVARCHAR(150) NOT NULL, 
    [image_type] NVARCHAR(50) NOT NULL 
)
