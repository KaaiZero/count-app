using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Count.Models
{
    public class Day
    {
        public Day()
        {
            List<Meal> Meals = new List<Meal>();
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<Meal> Meals { get; set; }
        public int AllDayCalories { get; set; }//acumulutive from each meal in Meals
        public bool IsDeleted { get; set; }
        public bool IsComplete { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        

    }
}
