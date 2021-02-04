using System;
using System.Collections.Generic;
using System.Text;

namespace Count.Models
{
    public class MealFood: RootMealFood
    {
        public int MealId { get; set; }
        public Meal Meal { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }

    }
}
