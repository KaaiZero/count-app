using Count.App.Areas.Admin.Services.Interfaces;
using Count.Data;
using Count.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Areas.Admin.Services
{
    public class AdminMealService : IAdminMealService
    {
        private readonly CountDbContext _dbContext;
        public AdminMealService(CountDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<MealFood>> AllFoodsOfMeal(int id)
        {
            var meal = await FindMeal(id);
            List<MealFood> allFoods = await _dbContext.MealFoods
                .Include(mf=>mf.Food)
                .Include(mf=>mf.Meal)
                .Where(mf => mf.MealId == id).ToListAsync();
            return allFoods; 
        }
        public async Task CreateMealFood(MealFood model)
        {
            var meal = await FindMeal(model.MealId);
            var food = await FindFood(model.FoodId);
            var mf = new MealFood();
            mf.FoodId = food.Id;
            mf.MealId = meal.Id;

            await _dbContext.MealFoods.AddAsync(mf);
            await _dbContext.SaveChangesAsync();

        }
        public async Task RemoveFoodFromMeal(int id)
        {
            var mealfood = await _dbContext.MealFoods.FirstOrDefaultAsync(mf => mf.Id==id);
            if (mealfood == null)
            {
                throw new NullReferenceException($"No such mealfood obj.");
            }
            _dbContext.MealFoods.Remove(mealfood);
            await _dbContext.SaveChangesAsync();
        }
        public async Task CreateMeal(Meal model)
        {
            var meal = new Meal();
            meal.AllCalories = model.AllCalories;
            meal.CourceTitle = model.CourceTitle;
            meal.DayId = model.DayId;

           await _dbContext.Meals.AddAsync(meal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteMeal(Meal model)
        {
            var meal = await FindMeal(model.Id);
            meal.IsDeleted = true;
            _dbContext.Meals.Update(meal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditMeal(Meal model)
        {
            var meal = await FindMeal(model.Id);
            meal.Id = model.Id;
            meal.AllCalories = model.AllCalories;
            meal.CourceTitle = model.CourceTitle;
            meal.DayId = model.DayId;
            meal.IsComplete = model.IsComplete;

            _dbContext.Meals.Update(meal);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Day> FindDay(int id)
        {
            var day =await _dbContext.Days
                .Include(d => d.Meals)
                .Include(d => d.User)
                .FirstOrDefaultAsync(d=>d.Id==id);
            if (day == null)
            {
                throw new NullReferenceException($"No day with id:{id}");
            }
            return day;
        }
        public async Task<Meal> FindMeal(int id)
        {
            var meal = await _dbContext.Meals
                .Include(m => m.Foods)
                .Include(m => m.Day)
                .FirstOrDefaultAsync(m=>m.Id==id);
            if(meal==null)
            {
                throw new NullReferenceException($"No meal with id:{id}");
            }
            return meal;
        }
        public async Task<Food> FindFood(int id)
        {
            var food = await _dbContext.Foods
                .Include(f => f.User)
                .Include(f => f.Meals)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (food == null)
            {
                throw new NullReferenceException($"No food with id:{id}");
            }
            return food; 
        }
        public async Task<Food>FindFoodByName(string name)
        {
            var food = await _dbContext.Foods
                .Include(f => f.User)
                .Include(f => f.Meals)
                .FirstOrDefaultAsync(f => f.Name == name);
            if (food == null)
            {
                throw new NullReferenceException($"No food with name:{name}");
            }
            return food;
        }
        public async Task<MealFood>FindMealFoodById(int id)
        {
            var mealfood = await _dbContext.MealFoods
                .Include(mf=>mf.Meal)
                .Include(mf=>mf.Food)
                .FirstOrDefaultAsync(mf => mf.Id == id);
            if (mealfood == null)
            {
                throw new NullReferenceException($"No obj with id:{id}");
            }
            return mealfood;
        }
        public async Task<User> FindUserById(string id)
        {
            var user = await _dbContext.Users
                .Include(u => u.Days)
                .Include(u => u.Gender)
                .Include(u => u.Country)
                .Include(u => u.Posts)
                .Include(u => u.UserBmis)
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
                //.Include(u => u.Days)
                //.Include(u => u.Gender)
                //.Include(u => u.Country)
                //.Include(u => u.Posts)
                //.Include(u => u.UserBmis)
                .FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                throw new NullReferenceException($"No user with username:{username}.");
            }
            return user;
        }
    }
}
