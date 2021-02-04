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
    public class AdminBmiService:IAdminBmiService
    {
        private readonly CountDbContext _dbContext;
        public AdminBmiService(CountDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateBmi(BmiUser model)
        {
            var newBmi = new BmiUser();
            newBmi.Date = model.Date;
            newBmi.Bmi = model.Bmi;
            newBmi.Weight = model.Weight;
            newBmi.Height = model.Height;
            newBmi.CalculatedBmi = model.CalculatedBmi;
            newBmi.UserId = model.UserId;

            await _dbContext.BmisUsers.AddAsync(newBmi);
            await _dbContext.SaveChangesAsync(); 
        }

        public async Task EditBmi(BmiUser model)
        {
            var bmi = await FindBmi(model.Id);
            bmi.Date = model.Date;
            bmi.Id = model.Id;
            bmi.UserId = model.UserId;
            bmi.Weight = model.Weight;
            bmi.Height = model.Height;
            bmi.Bmi = model.Bmi;
            bmi.CalculatedBmi = model.CalculatedBmi;

            _dbContext.BmisUsers.Update(bmi);
            await _dbContext.SaveChangesAsync(); 

        }
        public async Task DeleteBmi(BmiUser model)
        {
            var bmi = await FindBmi(model.Id);
            bmi.IsDeleted = true;
            _dbContext.BmisUsers.Update(bmi);
            await _dbContext.SaveChangesAsync(); 
        }

        public async Task<BmiUser> FindBmi(int id)
        {
            var bmiUser = await _dbContext.BmisUsers
                .Include(bu=>bu.User)
                .FirstOrDefaultAsync(bu => bu.Id == id);
            if (bmiUser == null)
            {
                throw new NullReferenceException($"No BMI with id:{id}.");
            }
            return bmiUser; 
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
                .Include(u=>u.UserBmis)
                .Include(u=>u.Posts)
                .Include(u=>u.Days)
                .FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                throw new NullReferenceException($"No user with username:{username}.");
            }
            return user;

        }

        public async Task<List<BmiUser>> AllUserUserBmis(string username)
        {
            var user = await FindUserByUsername(username);
            List<BmiUser> list = await _dbContext.BmisUsers
                .Where(bu => bu.UserId == user.Id)
                .Include(bu=>bu.User)
                .ToListAsync();
            return list;

        }
    }
}
