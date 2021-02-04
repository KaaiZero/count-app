using Count.App.Services.Inderfaces;
using Count.Data;
using Count.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Services
{
    public class PostService :IPostService
    {
        private readonly CountDbContext _dbContext;
        public PostService(CountDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreatePost(Post model)
        {
            var post = new Post();
            post.AuthorId = model.AuthorId;
            post.Content = model.Content;
            post.PostedOn = model.PostedOn;
            post.Summary = model.Summary;
            post.Title = model.Title;

            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePost(Models.Post.DeletePostBindingModel model)
        {
            var post = await FindPost(model.Id);
            post.IsDelete = true;

            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditPost(Models.Post.EditPostBindingModel model)
        {
            var post = await FindPost(model.Id);

            post.Title = model.Title;
            post.Summary = model.Summary;
            post.Content = model.Content;
            post.AuthorId = model.AuthorId;
            int count = 0;
            if (model.Files != null)
            {
                foreach (var file in model.Files)
                {
                    if (file != null)
                    {
                        if (file.Length > 0)
                        {
                            count++; //---The first uploaded photo when count=1 will be the cover photo so thats why i have this count

                            var fileName = Path.GetFileName(file.FileName);
                            var fileExtension = Path.GetExtension(fileName);
                            var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                            var fileToUpload = new Count.Models.File();
                            fileToUpload.PostId = model.Id;
                            if (count == 1)
                            {
                                fileToUpload.IsCoverPhoto = true;
                            }

                            using (var target = new MemoryStream())
                            {
                                file.CopyTo(target);
                                fileToUpload.FileUpload = target.ToArray();
                            }
                            await _dbContext.Files.AddAsync(fileToUpload);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }
            }

            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync(); 
        }
        public async Task<Post> FindPost(int id)
        {
            var post = await _dbContext.Posts
                .Include(p => p.Files)
                .Include(p => p.CoverPhoto)
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.Id == id);
            return post;
        }

        public async Task<List<Post>> AllPosts()
        {
            var list = await _dbContext.Posts
                .Include(p => p.Author)
                .Include(p => p.Files)
                .Include(p => p.CoverPhoto)
                .ToListAsync();
            return list;
        }
        public async Task<User> FindUserByUsername(string username)
        {
            var user = await _dbContext.Users
                .Include(u=>u.Posts)
                .Include(u=>u.UserBmis)
                .Include(u=>u.Days)
                .FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                throw new NullReferenceException($"No user with username: {username}.");
            }
            return user;
        }
        public async Task<User> FindUserById(string id)
        {
            var user = await _dbContext.Users
                .Include(u => u.Posts)
                .Include(u => u.UserBmis)
                .Include(u => u.Days)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new NullReferenceException($"No user with id: { id}.");
            }
            return user;
        }

    }
}
