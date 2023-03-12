﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            if (currentWeek)
            {
                return this.FirstWeekPlan!.meals[day];
            }
            else
            {
                return this.NextWeekPlan!.meals[day];
            }

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
    }
}
