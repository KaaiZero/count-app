using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Count.Models
{
    public class Food
    {
        public Food()
        {
            List<MealFood> Meals = new List<MealFood>(); 
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Calories { get; set; }
        public List<MealFood> Meals { get; set; }
        public double Proteins { get; set; }
        public double Carbs { get; set; }
        public double Fats { get; set; }
        public bool IsDelete { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
