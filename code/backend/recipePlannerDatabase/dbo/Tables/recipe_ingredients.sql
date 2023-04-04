CREATE TABLE [dbo].[recipe_ingredients] (
    [recipe_ingredient_id] INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY CLUSTERED ([recipe_ingredient_id] ASC),
    [recipe_id]            INT           NOT NULL,
    [ingredient_name]      NVARCHAR (40) NOT NULL,
    [quantity]             INT           NOT NULL,
    [unit_id]              INT           DEFAULT 0 NOT NULL,
    CONSTRAINT [FK_recipe_ingredients_measurement_unit] FOREIGN KEY ([unit_id]) REFERENCES [dbo].[measurment_unit] ([unit_id]) ON UPDATE CASCADE,
    CONSTRAINT [FK_recipe_ingredients_recipe] FOREIGN KEY ([recipe_id]) REFERENCES [dbo].[recipe] ([recipe_id]) ON DELETE CASCADE ON UPDATE CASCADE,
    UNIQUE NONCLUSTERED ([recipe_id] ASC, [ingredient_name] ASC)
);
