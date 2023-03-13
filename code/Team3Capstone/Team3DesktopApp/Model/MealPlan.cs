using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3DesktopApp.Model
{
    public class MealPlan
    {
        public int? MealPlanId { get; set; }
        public DateTime MealPlanDate { get; set; }
        public int UserId { get; set; }
        public Dictionary<DayOfWeek, List<Meal?>> meals { get; set; } = new Dictionary<DayOfWeek, List<Meal?>>();
    }
}
