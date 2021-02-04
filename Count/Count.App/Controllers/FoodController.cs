using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Count.App.Models.Food;
using Count.App.Services.Inderfaces;
using Count.Models;
using Microsoft.AspNetCore.Mvc;

namespace Count.App.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodService _service;
        public FoodController(IFoodService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> AllFoodss()
        {
            var list = new FoodsViewModel
            {
                AllFoods = await _service.AllFoods()
            };
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> CreateFood()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("AllFoods", "Food");
            }
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var food = new Food();
            food.UserId = user.Id;

            return View(food);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFood(Food model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            List<Food> list = await _service.AllFoods();
            var check = list.FirstOrDefault(l => l.Name == model.Name);
            if (check != null)
            {
                ModelState.AddModelError("FoodAlreadyExists", "This food is already in the database!");
                return View(model);
            }
            await _service.CreateFood(model);
            return RedirectToAction("AllFoods", "Food");

        }
        [HttpGet]
        public async Task<IActionResult> EditFood(int id)
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var food = await _service.FindFood(id);
            food.UserId = user.Id;
            return View(food);
        }
        [HttpPost]
        public async Task<IActionResult> EditFood(Food model)
        {
            if (ModelState.IsValid)
            {
                await _service.EditFood(model);
                return RedirectToAction("AllFoods", "Food");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var food = await _service.FindFood(id);
            return View(food);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteFood(Food model)
        {
            if (ModelState.IsValid)
            {
                await _service.DeleteFood(model);
                return RedirectToAction("AllFoods", "Food");
            }
            return View();
        }
    }
}
