using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3DesktopApp.Model
{
    public class Meal
    {
        public int? MealId { get; set; }
        public int MealPlanId { get; set; }
        public DateTime Date { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public MealType MealType { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
