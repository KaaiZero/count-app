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
    public class AccountService : IAccountService
    {
        private readonly CountDbContext _dbContext; 
        public AccountService(CountDbContext dbContext)
        {
            _dbContext = dbContext; 
        }


        public async Task<User> FindUserByUsername(string username)
        {
            var user = await _dbContext.Users
                .Include(u=>u.Country)
                .FirstOrDefaultAsync(u=>u.UserName==username);
            return user; 
        }


    }
}
