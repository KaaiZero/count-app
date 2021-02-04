using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Count.Models
{
    public class Meal
    {
        public Meal()
        {
            List<MealFood> Foods = new List<MealFood>(); 
        }
        public int Id { get; set; }
        public string CourceTitle { get; set; }
        public List<MealFood>Foods  { get; set; }
        public double AllCalories { get; set; }
        public bool IsComplete { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Day")]
        public int DayId { get; set; }
        public Day Day { get; set; }


    }
}
