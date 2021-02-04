using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Areas.Admin.ViewModels.MealFood
{
    public class CreateMealFoodVM
    {
        public int MealId { get; set; }
        public List<Count.Models.Food> Foods { get; set; }
    }
}
