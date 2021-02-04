using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Count.Models
{
    public class User:IdentityUser
    {
        public User()
        {
            List<BmiUser> UserBmis = new List<BmiUser>();
            List<Day> Days = new List<Day>();
            List<Post> Posts = new List<Post>(); 
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Age { get; set; }
        public List<Post> Posts { get; set; }
        public List<BmiUser> UserBmis { get; set; }
        public List<Day> Days { get; set; }

        public Country Country { get; set; }
        public Gender Gender { get; set; }

        public bool IsDelete { get; set; }
    }
}
