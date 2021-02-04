using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Services.Inderfaces
{
    public interface IAccountService
    {
        public Task<User> FindUserByUsername(string username);  
    }
}
