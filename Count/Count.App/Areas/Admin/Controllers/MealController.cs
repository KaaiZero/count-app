using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Count.App.Areas.Admin.Services.Interfaces;
using Count.App.Areas.Admin.ViewModels.MealFood;
using Count.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Count.App.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    [Area("Admin")]
    public class MealController : Controller
    {
        private readonly IAdminMealService _service;
        private readonly IAdminFoodService _serviceFood;
        public MealController(IAdminMealService service,IAdminFoodService serviceFood)
        {
            _service = service;
            _serviceFood = serviceFood;
        }
        //all MealsOfDay are in DayController
        [HttpGet]
        public async Task<IActionResult> CreateMeal(int id)
        {
            var day = await _service.FindDay(id);
            var meal = new Meal();
            meal.DayId = day.Id;
            return View(meal);

        }
        [HttpPost]
        public async Task<IActionResult> CreateMeal(Meal model)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateMeal(model);
                return RedirectToAction("AllMealsOfDay", "Day",new { id=model.DayId});
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> EditMeal(int id)
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var meal = await _service.FindMeal(id);
            var toEdit = new Meal();
            toEdit.AllCalories = meal.AllCalories;
            toEdit.CourceTitle = meal.CourceTitle;
            toEdit.Id = meal.Id;
            toEdit.DayId = meal.DayId;
            return View(toEdit);
        }
        [HttpPost]
        public async Task<IActionResult>EditMeal(Meal model)
        {
            if (ModelState.IsValid)
            {
                await _service.EditMeal(model);
                return RedirectToAction("AllMealsOfDay", "Day", new { id = model.DayId });
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var meal = await _service.FindMeal(id);
            var toDelete = new Meal();
            toDelete.IsDeleted = meal.IsDeleted;
            toDelete.Id = meal.Id;
            toDelete.CourceTitle = meal.CourceTitle;
            toDelete.AllCalories = meal.AllCalories;
            toDelete.DayId = meal.DayId;

            return View(toDelete);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMeal(Meal model)
        {
            if (ModelState.IsValid)
            {
                await _service.DeleteMeal(model);
                return RedirectToAction("AllMealsOfDay", "Day", new { id = model.DayId });
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddFoodToMeal(int id)//---id of the meal
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var meal = await _service.FindMeal(id);
            //CreateMealFoodVM newMF = new CreateMealFoodVM();//to make a list of foods for multiple
            MealFood newMF = new MealFood();
            newMF.MealId = meal.Id;

            IEnumerable<Food> list = await _serviceFood.AllFoods();
            
            ViewBag.Food = new SelectList(list, "Name", "Name", newMF.FoodId);
            //newMF.Foods = list; 

            //ViewBag.FoodList = new MultiSelectList(list, "Name","Name", newMF.Foods);
            return View(newMF);

        }
        [HttpPost]
        public async Task<IActionResult>AddFoodToMeal(/*CreateMealFoodVM*/MealFood model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var food = await _service.FindFoodByName(model.Food.Name);
            model.FoodId = food.Id;
            await _service.CreateMealFood(model);
            //if (model.Foods.Count == 0)
            //{
            //    return RedirectToAction("AllFoodsOfMeal", "Meal", new { id = model.MealId });
            //}
            //else
            //{
            //    foreach (Food food in model.Foods)
            //    {
            //        var mf = new MealFood();
            //        mf.FoodId = food.Id;
            //        mf.MealId = model.MealId;
            //        await _service.CreateMealFood(mf);
            //    }
            //}
            return RedirectToAction("AllFoodsOfMeal","Meal",new {id=model.MealId });
        }
        public async Task<IActionResult> RemoveFoodFromMeal(int id)
        {
            var mealfood = await _service.FindMealFoodById(id);
            await _service.RemoveFoodFromMeal(id);
            return RedirectToAction("AllFoodsOfMeal", "Meal", new { id = mealfood.MealId });
        }
        [HttpGet]
        public async Task<IActionResult> AllFoodsOfMeal(int id)
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var list = new AllFoodsOfMealVM();
            list.AlFofM = await _service.AllFoodsOfMeal(id);
            return View(list);
        }

    }
}
