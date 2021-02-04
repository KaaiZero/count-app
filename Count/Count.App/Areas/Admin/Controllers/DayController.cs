using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Count.App.Areas.Admin.Services.Interfaces;
using Count.App.Areas.Admin.ViewModels.Day;
using Count.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Count.App.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    [Area("Admin")]
    public class DayController : Controller
    {
        private readonly IAdminDayService _service;
        public DayController(IAdminDayService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> AllUserDays()
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var list = new AllDaysOfUserViewModel
            { 
                AllDaysOfUser= await _service.AllDaysOfUser(user.UserName) 
            };
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> AllMealsOfDay(int id)//----day id
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var day = await _service.FindDay(id);
            var list = new AllMealsOfDayViewModel
            {
                AllMealsOfDay = await _service.AllMealsOfDay(day.Id)
            };
            return View(list); 
        }
        [HttpGet]
        public async Task<IActionResult> CreateDay()
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var newDay = new Day();
            newDay.Date = DateTime.Now.Date;
            newDay.UserId = user.Id;
            
            return View(newDay);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDay(Day model)
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            List<Day> list = await _service.AllDaysOfUser(user.UserName);
            foreach (Day day in list)
            {
                if (day.Date.Date == model.Date)
                {
                    if (!day.IsDeleted)
                    {
                        ViewData["DayExists"] = "This day already exists!";
                        return RedirectToAction("AllUserDays", "Day");
                    }
                }
            }
            if (ModelState.IsValid)
            {
                await _service.CreateDay(model);
                return RedirectToAction("AllUserDays", "Day");
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> DeleteDay(int id)
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var day = await _service.FindDay(id);
            var toDelete = new Day();
            toDelete.UserId = day.UserId;
            toDelete.IsComplete = day.IsComplete;
            toDelete.IsDeleted = day.IsDeleted;
            toDelete.Meals = day.Meals;
            toDelete.Id = day.Id;
            toDelete.Date = day.Date;
            toDelete.AllDayCalories = day.AllDayCalories;

            return View(toDelete);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDay(Day model)
        {
            if (ModelState.IsValid)
            {
                await _service.DeleteDay(model);
                return RedirectToAction("AllUserDays","Day");
            }
            return View(model);
        }
        
    }
}
