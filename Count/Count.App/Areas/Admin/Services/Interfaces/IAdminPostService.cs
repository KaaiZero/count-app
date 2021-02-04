using Count.App.Areas.Admin.ViewModels.Post;
using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Areas.Admin.Services.Interfaces
{
    public interface IAdminPostService
    {
        public Task<List<Post>> AllPosts();
        public Task<Post> FindPost(int id);
        public Task CreatePost(Post model);
        public Task EditPost(EditPostBindingModel model);
        public Task DeletePost(DeletePostBindingModel id);

        public Task<User> FindUserByUsername(string username);
        public Task<User> FindUserById(string id);


    }
}
