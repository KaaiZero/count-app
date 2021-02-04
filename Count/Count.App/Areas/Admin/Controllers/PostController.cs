using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Count.App.Areas.Admin.Services.Interfaces;
using Count.App.Areas.Admin.ViewModels;
using Count.App.Areas.Admin.ViewModels.Post;
using Count.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Count.App.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IAdminPostService _service; 
        public PostController(IAdminPostService service)
        {
            _service = service; 
        }


        [HttpGet]
        public async Task< IActionResult> Feed()
        {
            var getAllPosts = await _service.AllPosts();
            var allPosts = new ViewModels.AllPostsViewModel
            {
                Posts = getAllPosts
            };

            return View(allPosts);
        }
        
        [HttpGet]
        public async Task<IActionResult> CreatePost()
        {
            var newPost = new Post();
            var author = await _service.FindUserByUsername(User.Identity.Name);
            newPost.AuthorId = author.Id;
            return View(newPost);
        }
        [HttpPost]
        public async Task<IActionResult>CreatePost(Post model)
        {
            model.PostedOn = DateTime.Now;
            string[] contentSplit = model.Content.Split('.','!','?');
            string summary = contentSplit[0]+"." + contentSplit[1]+ "......";
            model.Summary = summary; 
            if (ModelState.IsValid)
            {
                await _service.CreatePost(model);
                //----Here wanted to redirect to edit because there the user will be able to upload a file but the id of the post is not yet created
                return RedirectToAction("Feed","Post");
            }
            return View(); 

        }

        [HttpGet]
        public async Task<IActionResult> EditPost(int id)
        {
            var findPost = await _service.FindPost(id);
            var authorcheck = await _service.FindUserByUsername(User.Identity.Name);
            if (findPost.AuthorId == authorcheck.Id)
            {
                var post = new EditPostBindingModel();
                post.Id = findPost.Id;
                post.Title = findPost.Title;
                post.Content = findPost.Content;
                post.Summary = findPost.Summary;
                post.AuthorId = findPost.AuthorId;
                post.Author = findPost.Author; 

                return View(post);
            }
            ViewData["NotTheAuthor"] = "You can not edit this post, because you are not the author!";
            return View(); 
           
        }
        [HttpPost]
        public async Task<IActionResult> EditPost(EditPostBindingModel model)
        {
            string[] contentSplit = model.Content.Split('.', '!', '?');
            string summary = contentSplit[0] + "." + contentSplit[1] + "......";
            model.Summary = summary;

            if (ModelState.IsValid)
            {
                await _service.EditPost(model);
                return RedirectToAction("Feed","Post");
            }
            return View(); 
        }

        [HttpGet]
        public async Task<IActionResult> DetailsPost(int id)
        {
            var post = await _service.FindPost(id);
            
            return View(post);
        }
        [HttpGet]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _service.FindPost(id);
            var authorcheck = await _service.FindUserByUsername(User.Identity.Name);
            var postToDelete = new DeletePostBindingModel();
            postToDelete.Id = post.Id;
            postToDelete.Title = post.Title;
            postToDelete.PostedOn = post.PostedOn;
            postToDelete.Files = post.Files;
            postToDelete.Author = post.Author;
            postToDelete.AuthorId = post.AuthorId;
            postToDelete.Content = post.Content;
            postToDelete.CoverPhoto = post.CoverPhoto;
            postToDelete.Summary = post.Summary;
            if (post.AuthorId == authorcheck.Id)
            {
                return View(postToDelete); 
            }
            ViewData["NotTheAuthor"] = "You can not delete this post, because you are not the author!";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>DeletePost(DeletePostBindingModel model)
        {
            if (ModelState.IsValid)
            {
                await _service.DeletePost(model);
                return RedirectToAction("Feed", "Post");
            }
            return View(); 
        }
    }
}
