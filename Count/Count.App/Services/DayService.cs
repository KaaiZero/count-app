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
    public class DayService: IDayService
    {
        private readonly CountDbContext _dbContext;
        public DayService(CountDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateDay(Day model)
        {
            var day = new Day();
            day.AllDayCalories = model.AllDayCalories;

            day.Date = model.Date;
            day.IsComplete = model.IsComplete;
            day.UserId = model.UserId;
            day.User = model.User;
            day.Meals = model.Meals;

            _dbContext.Days.Add(day);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDay(Day model)
        {
            var day = await FindDay(model.Id);
            day.IsDeleted = true;

            _dbContext.Update(day);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditDay(Day model)
        {
            var day = await FindDay(model.Id);
            day.AllDayCalories = model.AllDayCalories;
            day.IsComplete = model.IsComplete;
            day.Meals = model.Meals;
            day.User = model.User;
            day.UserId = model.UserId;
        }
        public async Task<List<Day>> AllDaysOfUser(string username)
        {
            List<Day> list = await _dbContext.Days
                .Include(d => d.Meals)
                .Include(d => d.User)
                .Where(d => d.User.UserName == username)
                .ToListAsync();
            return list;
        }
        public async Task<List<Meal>> AllMealsOfDay(int id)
        {
            List<Meal> list = await _dbContext.Meals
                .Include(m => m.Day)
                .Include(m => m.Foods)
                .Where(m => m.DayId == id)
                .ToListAsync();
            return list;
        }

        public async Task<Day> FindDay(int id)
        {
            var day = await _dbContext.Days
                .Include(d => d.Meals)
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (day == null)
            {
                throw new NullReferenceException($"No day with id:{id}.");
            }
            return day;
        }

        public async Task<Meal> FindMeal(int id)
        {
            var meal = await _dbContext.Meals
                .Include(m => m.Day)
                .Include(m => m.Foods)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                throw new NullReferenceException($"No meal with id:{id}.");
            }
            return meal;
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
                //.Include(u=>u.Days)
                //.Include(u=>u.Gender)
                //.Include(u=>u.Country)
                //.Include(u=>u.Posts)
                //.Include(u=>u.UserBmis)
                .FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                throw new NullReferenceException($"No user with username:{username}.");
            }
            return user;
        }






    }
}
