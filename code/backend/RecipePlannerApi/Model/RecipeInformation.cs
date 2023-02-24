namespace RecipePlannerApi.Model {
    public class RecipeInformation {
        public string Summary { get; set; }
        public string Image { get; set; }
        public string ImageType { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<RecipesStep> Steps { get; set; }
    }
}
