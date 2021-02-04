using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Areas.Admin.Services.Interfaces
{
    public interface IAdminFoodService
    {
        public Task<Food> FindFood(int id);
        public Task<Meal> FindMeal(int id);

        public Task<User> FindUserById(string id);
        public Task<User> FindUserByUsername(string username);

        public Task<List<Food>> AllFoods(); 

        public Task CreateFood(Food model);
        public Task EditFood(Food model);
        public Task DeleteFood(Food model); 



    }
}
