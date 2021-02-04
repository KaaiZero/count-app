using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Models.User
{
    public class EditUserrBindingModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public Country Country { get; set; }
        public bool IsDelete { get; set; }
    }
}
