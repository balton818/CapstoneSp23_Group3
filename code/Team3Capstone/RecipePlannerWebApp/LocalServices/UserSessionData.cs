﻿using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Model;

namespace RecipePlannerWebApp.LocalServices
{
    public class UserSessionData
    {
        public User CurrentUser { get; set; } = new User();
        
        public string? CurrentRecipeTitle { get; set; }

        public BrowseRecipeRequest? lastExplorePageRequest { get; set; }

        public Meal? SelectedMeal { get; set; }
        public bool NewMeal { get; set; }
    }
}
