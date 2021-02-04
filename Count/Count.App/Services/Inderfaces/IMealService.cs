using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Services.Inderfaces
{
    public interface IMealService
    {
        public Task<User> FindUserById(string id);
        public Task<User> FindUserByUsername(string username);
        public Task<Meal> FindMeal(int id);
        public Task<Day> FindDay(int id);
        public Task<Food> FindFood(int id);
        public Task<Food> FindFoodByName(string name);
        public Task<MealFood> FindMealFoodById(int id);
        public Task CreateMeal(Meal model);
        public Task EditMeal(Meal model);
        public Task DeleteMeal(Meal model);
        public Task CreateMealFood(MealFood model);
        public Task RemoveFoodFromMeal(int id);
        public Task<List<MealFood>> AllFoodsOfMeal(int id);
    }
}
