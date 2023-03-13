using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel
{
    public class MealPlanViewModel
    {

        public MealPlan? FirstWeekPlan { get; set; }
        public MealPlan? NextWeekPlan { get; set; }
        public bool CurrentWeek { get; set; } = true;

        public async Task getMealPlans(int userId, HttpClient client)
        {
            var connection = new HttpClientConnection();
            this.FirstWeekPlan = await connection.GetPlan(userId, client, true);
            this.NextWeekPlan = await connection.GetPlan(userId, client, false);
        }

        public List<Meal?> getMealForDay(DayOfWeek day, bool currentWeek)
        {
            this.CurrentWeek = currentWeek;
            if (currentWeek && this.FirstWeekPlan != null)
            {
                return this.FirstWeekPlan!.meals[day];
            }
            else if (this.NextWeekPlan != null)
            {
                return this.NextWeekPlan!.meals[day];
            }

            return new List<Meal?>();
        }

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

                if (current == true)
                {
                    this.FirstWeekPlan = connection.AddToPlan((int)this.FirstWeekPlan.MealPlanId, client, meal).Result;

                }
                else
                {
                    this.NextWeekPlan = connection.AddToPlan((int)this.NextWeekPlan.MealPlanId, client, meal).Result;
                }
            }
        }

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

                List<Meal> meals = this.getMealForDay((DayOfWeek)day, (bool)current);
                var mealToReplace = getMealId(meals, (MealType)type);

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

        public void RemoveMealFromPlan(HttpClient client, string mealToRemove, DayOfWeek dayOfWeek,
            MealType mealType)
        {
            HttpClientConnection connection = new HttpClientConnection();
            int? mealId;
            if (this.CurrentWeek)
            {
                var meals = this.FirstWeekPlan.meals[dayOfWeek];
                mealId = this.getMealId(meals, mealType);
                this.FirstWeekPlan.meals[dayOfWeek].RemoveAll(meal => meal.MealId == mealId);
            }
            else
            {
                var meals = this.NextWeekPlan.meals[dayOfWeek];
                mealId = this.getMealId(meals, mealType);
                this.NextWeekPlan.meals[dayOfWeek].RemoveAll(meal => meal.MealId == mealId);
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

        public Recipe getRecipe(string recipeName, DayOfWeek day, MealType type)
        {
            if (this.CurrentWeek)
            {
                var meals = this.FirstWeekPlan.meals[day];
                return this.findRecipe(meals, type);
            }
            else
            {
                var meals = this.NextWeekPlan.meals[day];
                return this.findRecipe(meals, type);
            }
        }

        private Recipe findRecipe(List<Meal> meals, MealType type)
        {
            foreach (Meal meal in meals)
            {
                if (meal.MealType == type)
                {
                    return meal.Recipe;
                }
            }

            return null;
        }

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

        public bool CheckForRecipe(MealType mealType, DayOfWeek day, bool current)
        {

            if (this.CurrentWeek && this.FirstWeekPlan != null && this.NextWeekPlan != null)
            {
                var meals = this.FirstWeekPlan.meals[day];
                return this.findRecipe(meals, mealType) != null;
            }
            else if (!this.CurrentWeek)
            {
                var meals = this.NextWeekPlan.meals[day];
                return this.findRecipe(meals, mealType) != null;
            }

            return false;
        }
    }
}
