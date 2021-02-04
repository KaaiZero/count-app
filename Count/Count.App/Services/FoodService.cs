using Count.App.Services.Inderfaces;
using Count.Data;
using Count.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Services
{
    public class FoodService: IFoodService
    {
        private readonly CountDbContext _dbContext;
        public FoodService(CountDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Food>> AllFoods()
        {
            List<Food> list = await _dbContext.Foods
                .Include(f => f.Meals)
                .Include(f => f.User)
                .ToListAsync();
            return list;
        }

        public async Task CreateFood(Food model)
        {
            var newFood = new Food();
            newFood.Calories = model.Calories;
            newFood.Carbs = model.Carbs;
            newFood.Fats = model.Fats;
            newFood.Name = model.Name;
            newFood.Proteins = model.Proteins;
            newFood.UserId = model.UserId;
            newFood.Quantity = model.Quantity;

            await _dbContext.Foods.AddAsync(newFood);
            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteFood(Food model)
        {
            var food = await FindFood(model.Id);


            _dbContext.Foods.Remove(food);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditFood(Food model)
        {
            var food = await FindFood(model.Id);
            food.Calories = model.Calories;
            food.Id = model.Id;
            food.Carbs = model.Carbs;
            food.Fats = model.Fats;
            food.Name = model.Name;
            food.Proteins = model.Proteins;
            food.UserId = model.UserId;
            food.Quantity = model.Quantity;

            _dbContext.Foods.Update(food);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Food> FindFood(int id)
        {
            var food = await _dbContext.Foods
                .Include(f => f.Meals)
                .Include(f => f.User)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (food == null)
            {
                throw new NullReferenceException($"No food with id:{id}");
            }
            return food;
        }

        public async Task<Meal> FindMeal(int id)
        {
            var meal = await _dbContext.Meals
                .Include(m => m.Foods)
                .Include(m => m.Day)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                throw new NullReferenceException($"No meal with id:{id}");
            }
            return meal;
        }

        public async Task<User> FindUserById(string id)
        {
            var user = await _dbContext.Users
                .Include(u => u.UserBmis)
                .Include(u => u.Posts)
                .Include(u => u.Days)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new NullReferenceException($"No user with id:{id}.");
            }
            return user;
        }

        public async Task<User> FindUserByUsername(string username)
        {

            var user = await _dbContext.Users
                .Include(u => u.UserBmis)
                .Include(u => u.Posts)
                .Include(u => u.Days)
                .FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                throw new NullReferenceException($"No user with username:{username}.");
            }
            return user;
        }
    }
}
