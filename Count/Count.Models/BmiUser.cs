
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Count.Models
{
    public class BmiUser
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Bmi Bmi { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double CalculatedBmi { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }


    }
}
