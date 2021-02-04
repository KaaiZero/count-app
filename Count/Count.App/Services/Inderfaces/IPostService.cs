using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Services.Inderfaces
{
    public interface IPostService
    {
        public Task<List<Post>> AllPosts();
        public Task<Post> FindPost(int id);
        public Task CreatePost(Post model);
        public Task EditPost(Models.Post.EditPostBindingModel model);
        public Task DeletePost(Models.Post.DeletePostBindingModel model);

        public Task<User> FindUserByUsername(string username);
        public Task<User> FindUserById(string id);
    }
}
