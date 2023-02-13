using System;
using System.Collections.Generic;
using System.Net.Http;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

public class FoundRecipeViewModel
{

    public List<Recipe> Recipes { get; set; }
    public string SelectedRecipeTitle { get; set; }

    public List<Recipe> GetRecipes(int userId)
    {
        this.Recipes = new List<Recipe>();
        HttpClientConnection connection = new HttpClientConnection();
        var retrieved = connection.GetRecipes(userId);
        if (retrieved.Result != null)
        {
            this.Recipes.AddRange(retrieved.Result);
        }

        return this.Recipes;
    }


}