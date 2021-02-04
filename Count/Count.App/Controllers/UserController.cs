using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Count.App.Models.User;
using Count.App.Services.Inderfaces;
using Microsoft.AspNetCore.Mvc;

namespace Count.App.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
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
            var userEdit = new EditUserrBindingModel();
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
        public async Task<IActionResult> EditProfile(EditUserrBindingModel model)
        {
            if (ModelState.IsValid)
            {
                await _service.EditUser(model);
                return RedirectToAction("Profile", "User");
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
