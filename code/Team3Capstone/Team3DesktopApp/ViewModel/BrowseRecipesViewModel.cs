using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class BrowseRecipesViewModel
    {
        #region Properties

        /// <summary>Gets or sets the recipes for browsing.</summary>
        /// <value>The list of recipes.</value>
        public List<Recipe>? Recipes { get; set; }

        /// <summary>Gets or sets the meal type for filtering recipes.</summary>
        /// <value>The type of the applied recipe.</value>
        public string AppliedRecipeType { get; set; } = "";

        /// <summary>Gets or sets the type for filtering by diet.</summary>
        /// <value>The type of the applied diet.</value>
        public string AppliedDietType { get; set; } = "";

        /// <summary>Gets or sets the selected recipe title.</summary>
        /// <value>The selected recipe title.</value>
        public string? SelectedRecipeTitle { get; set; }

        /// <summary>Gets or sets the number of recipes that a user can browse.</summary>
        /// <value>The total number of recipes the user can currently browse</value>
        public int NumberOfRecipes { get; set; }

        /// <summary>Gets or sets the number of pages the user can move through.</summary>
        /// <value>The number of pages of browsable recipes.</value>
        public int NumberOfPages { get; set; }

        /// <summary>Gets or sets the current page the user is browsing.</summary>
        /// <value>The current page being browsed.</value>
        public int CurrentPage { get; set; }

        /// <summary>Gets or sets the name to search by.</summary>
        /// <value>The name to search by.</value>
        public string SearchName { get; set; } = "";

        /// <summary>Gets or sets the type of the applied cuisines for filtering.</summary>
        /// <value>The type of cuisines to filter by.</value>
        public string? AppliedCuisineType { get; set; }

        #endregion

        #region Methods

        /// <summary>Browses the recipes.</summary>
        /// <param name="client">The client used for connecting to the backend.</param>
        /// <param name="userId"> The logged in users user ID</param>
        /// <returns>
        ///   the list of recipes browsable by the user based on filter types and search term entered.
        /// </returns>
        public List<Recipe>? BrowseRecipes(HttpClient client, int userId)
        {
            this.Recipes = new List<Recipe>();
            var connection = new HttpClientConnection();
            var retrieved =
                connection.BrowseRecipes(userId, client, this.AppliedRecipeType, this.AppliedDietType, this.CurrentPage,
                    this.SearchName, this.AppliedCuisineType);

            var recipes = JsonConvert.DeserializeObject<List<Recipe>>(retrieved.Result.GetValue("recipes")!.ToString());
            this.NumberOfRecipes = (int)retrieved.Result.GetValue("totalNumberOfRecipes")!;

            if (recipes != null)
            {
                this.Recipes.AddRange(recipes);
            }

            if (this.NumberOfRecipes / 20 > 45)
            {
                this.NumberOfPages = 45;
            }
            else
            {
                this.NumberOfPages = this.NumberOfRecipes / 20;
            }


            if (this.NumberOfRecipes == 0)
            {
                this.setRecipesInfo();
            }


            return this.Recipes;
        }

        private void setRecipesInfo()
        {
            this.NumberOfPages = this.NumberOfRecipes / 20;
            this.CurrentPage = 0;
        }

        #endregion
    }
}
