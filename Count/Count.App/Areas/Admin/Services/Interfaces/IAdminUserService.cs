using Count.App.Areas.Admin.ViewModels.User;
using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Areas.Admin.Services.Interfaces
{
    public interface IAdminUserService
    {
        public Task EditUser(EditUserBindingModel model);
        public Task DeleteUser(string username); 
        public Task<User> FindUserById(string id);
        public Task<User> FindUserByUsername(string username);
        public Task<List<BmiUser>> UserBmis(string id);
        public Task<List<Post>> UserPosts(string id);
        public Task<List<Day>> UserDays(string id);



    }
}
