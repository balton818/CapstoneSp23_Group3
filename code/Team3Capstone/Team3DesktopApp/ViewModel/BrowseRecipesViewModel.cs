using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel
{
    public class BrowseRecipesViewModel
    {
        #region Properties

        /// <summary>Gets or sets the recipes.</summary>
        /// <value>The recipes.</value>
        public List<Recipe> Recipes { get; set; }

        public string AppliedRecipeType { get; set; } = "";
        public string AppliedDietType { get; set; } = "";

        /// <summary>Gets or sets the selected recipe title.</summary>
        /// <value>The selected recipe title.</value>
        public string SelectedRecipeTitle { get; set; }

        public int NumberOfRecipes { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; } = 0;

        #endregion

        #region Methods

        /// <summary>Gets the recipes.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///     <br />
        /// </returns>
        public List<Recipe> BrowseRecipes(HttpClient client)
        {
            this.Recipes = new List<Recipe>();
            var connection = new HttpClientConnection();
            var retrieved =
                connection.BrowseRecipes(client, this.AppliedRecipeType, this.AppliedDietType, this.CurrentPage);


            this.Recipes.AddRange(retrieved.Result);


            if (this.NumberOfRecipes == 0)
            {
                this.setRecipesInfo();
            }


            return this.Recipes;
        }

        private void setRecipesInfo()
        {
            this.NumberOfRecipes = this.Recipes.Count;
            this.NumberOfPages = this.NumberOfRecipes / 20;
            this.CurrentPage = 0;
        }

        #endregion
    }
}
