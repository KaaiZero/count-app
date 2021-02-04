using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Count.App.Areas.Admin.Services.Interfaces;
using Count.App.Areas.Admin.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Count.App.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IAdminUserService _service;
        public UserController(IAdminUserService service)
        {
            _service = service; 
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var userProfile = new Count.Models.User();

            userProfile.Id = user.Id;
            userProfile.UserName = user.UserName;
            userProfile.FirstName = user.FirstName;
            userProfile.LastName = user.LastName;
            userProfile.Weight = user.Weight;
            userProfile.Height = user.Height;
            userProfile.Gender = user.Gender;
            userProfile.Age = user.Age;
            userProfile.Country = user.Country; 
            userProfile.Days = user.Days;
            userProfile.Email = user.Email;
            userProfile.EmailConfirmed = user.EmailConfirmed;
            userProfile.IsDelete = user.IsDelete;
            userProfile.Posts = user.Posts;
            userProfile.UserBmis = user.UserBmis;

            return View(userProfile); 
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var userEdit = new EditUserBindingModel();
            userEdit.Id = user.Id;
            userEdit.UserName = user.UserName;
            userEdit.FirstName = user.FirstName;
            userEdit.LastName = user.LastName;
            userEdit.Age = user.Age;
            userEdit.Country = user.Country;
            userEdit.Gender = user.Gender;
            userEdit.Height = user.Height;
            userEdit.Weight = user.Weight;

            return View(userEdit); 

        }
        [HttpPost]
        public async Task<IActionResult>EditProfile(EditUserBindingModel model)
        {
            if (ModelState.IsValid)
            {
                await _service.EditUser(model);
                return RedirectToAction("Profile","User");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProfile()
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            await _service.DeleteUser(user.UserName);
            return RedirectToAction("Profile", "User");
        }
    }
}
