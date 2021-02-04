using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Services.Inderfaces
{
    public interface IDayService
    {
        public Task<User> FindUserById(string id);
        public Task<User> FindUserByUsername(string username);
        public Task<Day> FindDay(int id);
        public Task<Meal> FindMeal(int id);

        public Task<List<Day>> AllDaysOfUser(string username);
        public Task<List<Meal>> AllMealsOfDay(int id);
        public Task CreateDay(Day model);
        public Task EditDay(Day model);
        public Task DeleteDay(Day model);
    }
}
