using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Areas.Admin.Services.Interfaces
{
    public interface IAdminBmiService
    {
        public Task<User> FindUserById(string id);
        public Task<User> FindUserByUsername(string username);
        public Task<BmiUser> FindBmi(int id);
        public Task<List<BmiUser>> AllUserUserBmis(string username); 
        public Task CreateBmi(BmiUser model);
        public Task EditBmi(BmiUser model);
        public Task DeleteBmi(BmiUser model);

    }
}
