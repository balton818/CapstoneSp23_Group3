using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel
{
    /// <summary>
    ///   View model for the meal plan page
    /// </summary>
    public class MealPlanViewModel
    {

        /// <summary>Gets or sets the current week's meal plan.</summary>
        /// <value>The first week plan.</value>
        public MealPlan? FirstWeekPlan { get; set; }
        /// <summary>Gets or sets the following week's meal plan.</summary>
        /// <value>The next week plan.</value>
        public MealPlan? NextWeekPlan { get; set; }
        /// <summary>Gets or sets a value indicating whether the current week is selected.</summary>
        /// <value>
        ///   <c>true</c> if [current week]; otherwise, <c>false</c>.</value>
        public bool CurrentWeek { get; set; } = true;

        /// <summary>Gets the logged in users meal plans.</summary>
        /// <param name="userId">The user ID the meal plan belongs to.</param>
        /// <param name="client">The client to connect to the backend.</param>
        public async Task GetMealPlans(int userId, HttpClient client)
        {
            var connection = new HttpClientConnection();
            this.FirstWeekPlan = await connection.GetPlan(userId, client, true);
            this.NextWeekPlan = await connection.GetPlan(userId, client, false);
        }

        /// <summary>Gets the meal planned for the given day and week.</summary>
        /// <param name="day">The day.</param>
        /// <param name="currentWeek">if set to <c>true</c> [current week].</param>
        /// <returns>
        ///   A collection of meals for the given day and week
        /// </returns>
        public List<Meal?> GetMealForDay(DayOfWeek day, bool currentWeek)
        {
            this.CurrentWeek = currentWeek;
            if (currentWeek && this.FirstWeekPlan != null)
            {
                return this.FirstWeekPlan!.Meals[day];
            }
            else if (this.NextWeekPlan != null)
            {
                return this.NextWeekPlan!.Meals[day];
            }

            return new List<Meal?>();
        }

        /// <summary>Adds meal to plan.</summary>
        /// <param name="recipe">The recipe being added.</param>
        /// <param name="day">The day the recipe is being added to.</param>
        /// <param name="type">The type of meal the recipe will be IE MealType.</param>
        /// <param name="client">The client to connect to the backend.</param>
        /// <param name="current">current indicated which week is selected true for current week false for next week..</param>
        public void AddToPlan(Recipe recipe, DayOfWeek? day, MealType? type, HttpClient client, bool? current)
        {
            HttpClientConnection connection = new HttpClientConnection();
            if (type != null && day != null)
            {
                Meal meal = new Meal
                {
                    MealType = (MealType)type,
                    Recipe = recipe,
                    DayOfWeek = (DayOfWeek)day
                };

                if (current == true && this.FirstWeekPlan != null)
                {
                    this.FirstWeekPlan = connection.AddToPlan((int)this.FirstWeekPlan.MealPlanId!, client, meal).Result;

                }
                else if (this.NextWeekPlan != null)
                {
                    this.NextWeekPlan = connection.AddToPlan((int)this.NextWeekPlan.MealPlanId, client, meal).Result;
                }
            }
        }

        /// <summary>Updates the meal plan if a meal already exists for the spot the user is editing.</summary>
        /// <param name="recipe">The recipe to be added to the plan.</param>
        /// <param name="day">The day the recipe will be added to.</param>
        /// <param name="type">The type the mealtype for the plan.</param>
        /// <param name="client">The client to connect to the backed.</param>
        /// <param name="current">The current week, true if current, false if next.</param>
        public void UpdatePlan(Recipe recipe, DayOfWeek? day, MealType? type, HttpClient client, bool? current)
        {
            HttpClientConnection connection = new HttpClientConnection();
            if (type != null && day != null)
            {
                Meal meal = new Meal
                {
                    MealType = (MealType)type,
                    Recipe = recipe,
                    DayOfWeek = (DayOfWeek)day
                };

                List<Meal?> meals = this.GetMealForDay((DayOfWeek)day, (bool)current);
                var mealToReplace = this.getMealId(meals, (MealType)type);

                if (current == true)
                {
                    _ = connection.UpdatePlan(mealToReplace, client, meal).Result;

                }
                else
                {
                    _ = connection.UpdatePlan(mealToReplace, client, meal).Result;
                }

            }
        }

        /// <summary>Removes the meal from plan.</summary>
        /// <param name="client">The client to connect t the backend.</param>
        /// <param name="mealToRemove">The meal to remove from the plan.</param>
        /// <param name="dayOfWeek">The day of week where the meal being removed is planned.</param>
        /// <param name="mealType">Type of the meal that is being removed.</param>
        public void RemoveMealFromPlan(HttpClient client, string? mealToRemove, DayOfWeek dayOfWeek,
            MealType mealType)
        {
            HttpClientConnection connection = new HttpClientConnection();
            int? mealId;
            if (this.CurrentWeek)
            {
                var meals = this.FirstWeekPlan!.Meals[dayOfWeek];
                mealId = this.getMealId(meals!, mealType);
                this.FirstWeekPlan.Meals[dayOfWeek].RemoveAll(meal => meal!.MealId == mealId);
            }
            else
            {
                var meals = this.NextWeekPlan!.Meals[dayOfWeek];
                mealId = this.getMealId(meals!, mealType);
                this.NextWeekPlan.Meals[dayOfWeek].RemoveAll(meal => meal!.MealId == mealId);
            }

            connection.RemoveMeal(mealId, client);
        }

        private int? getMealId(List<Meal> meals, MealType type)
        {
            var mealId = -1;
            foreach (Meal meal in meals)
            {
                if (meal.MealType == type)
                {
                    mealId = (int)meal.MealId;
                }
            }

            return mealId;
        }

        /// <summary>Gets the recipe from the meal plan for the day and type passed in.</summary>
        /// <param name="day">The day where the recipe to get is stored.</param>
        /// <param name="type">The type of the recipe to get.</param>
        /// <returns>
        ///   the recipe stored in the given day and type for the selected week null if plans dont exist
        /// </returns>
        public Recipe GetRecipe(DayOfWeek day, MealType type)
        {
            if (this.CurrentWeek && this.FirstWeekPlan != null)
            {
                var meals = this.FirstWeekPlan.Meals[day];
                return this.findRecipe(meals!, type);
            }
            else if (this.NextWeekPlan != null)
            {
                var meals = this.NextWeekPlan.Meals[day];
                return this.findRecipe(meals!, type);
            }

            return null!;
        }

        private Recipe findRecipe(List<Meal> meals, MealType type)
        {
            foreach (Meal meal in meals)
            {
                if (meal.MealType == type)
                {
                    return meal.Recipe!;
                }
            }

            return null!;
        }

        /// <summary>Gets the date range for the specified weeks meal plan.</summary>
        /// <param name="currentWeek">if set to <c>true</c> [current week] else next week.</param>
        /// <returns>
        ///   the sunday - saturday date range for the specified week
        /// </returns>
        public DateOnly GetDate(bool currentWeek)
        {
            DateTime sundayDate;
            sundayDate = DateTime.Now.Subtract(new TimeSpan((int)DateTime.Now.DayOfWeek, 0, 0, 0));
            if (currentWeek && this.FirstWeekPlan != null)
            {
                return DateOnly.FromDateTime(this.FirstWeekPlan.MealPlanDate);
            }
            else if (!currentWeek && this.NextWeekPlan != null)
            {
                return DateOnly.FromDateTime(this.NextWeekPlan.MealPlanDate);
            }
            else if (currentWeek)
            {
                return DateOnly.FromDateTime(sundayDate);
            }
            else
            {

                return DateOnly.FromDateTime(sundayDate.AddDays(7));
            }

        }

        /// <summary>Checks if a recipe is currently planned on the given day for the given type.</summary>
        /// <param name="mealType">Type of the meal to be checked.</param>
        /// <param name="day">The day to check for the meal.</param>
        /// <returns>
        ///   true if meal is planned on the given day and type for the week selected false otherwise
        /// </returns>
        public bool CheckForRecipe(MealType mealType, DayOfWeek day)
        {

            if (this.CurrentWeek && this.FirstWeekPlan != null && this.NextWeekPlan != null)
            {
                var meals = this.FirstWeekPlan.Meals[day];
                return this.findRecipe(meals, mealType) != null;
            }
            else if (!this.CurrentWeek)
            {
                var meals = this.NextWeekPlan!.Meals[day];
                return this.findRecipe(meals, mealType) != null;
            }

            return false;
        }
    }
}
