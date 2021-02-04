using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Count.App.Models.Bmi;
using Count.App.Services.Inderfaces;
using Count.Models;
using Microsoft.AspNetCore.Mvc;

namespace Count.App.Controllers
{
    public class BmiController : Controller
    {
        private readonly IBmiService _service;
        public BmiController(IBmiService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> HealthCheck()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var list = new AllUserBmisViewModel
            {
                AllUserBmis = await _service.AllUserUserBmis(user.UserName)
            };
            return View(list);

        }
        [HttpGet]
        public async Task<IActionResult> CreateBmi()
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var bmi = new BmiUser();
            bmi.UserId = user.Id;

            return View(bmi);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBmi(BmiUser model)
        {
            model.Date = DateTime.Now;
            var calculatedBmi = model.Weight / (model.Height * model.Height);
            calculatedBmi = calculatedBmi * 10000;
            model.CalculatedBmi = Math.Round(calculatedBmi, 2);
            if (calculatedBmi > 25.0)
            {
                model.Bmi = Bmi.Overweight;
            }
            else if (calculatedBmi >= 18.5 && calculatedBmi <= 24.9)
            {
                model.Bmi = Bmi.Normal;
            }
            else
            {
                model.Bmi = Bmi.Underweight;
            }

            if (ModelState.IsValid)
            {
                await _service.CreateBmi(model);
                return RedirectToAction("HealthCheck", "Bmi");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditBmi(int id)
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var bmi = await _service.FindBmi(id);
            if (bmi.UserId != user.Id)
            {
                return RedirectToAction("HealthCheck", "Bmi");
            }
            return View(bmi);

        }
        [HttpPost]
        public async Task<IActionResult> EditBmi(BmiUser model)
        {
            var calculatedBmi = model.Weight / (model.Height * model.Height);
            calculatedBmi = calculatedBmi * 10000;
            model.CalculatedBmi = Math.Round(calculatedBmi, 2); ;
            if (calculatedBmi > 25.0)
            {
                model.Bmi = Bmi.Overweight;
            }
            else if (calculatedBmi >= 18.5 && calculatedBmi <= 24.9)
            {
                model.Bmi = Bmi.Normal;
            }
            else
            {
                model.Bmi = Bmi.Underweight;
            }

            if (ModelState.IsValid)
            {
                await _service.EditBmi(model);
                return RedirectToAction("HealthCheck", "Bmi");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteBmi(int id)
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            var bmi = await _service.FindBmi(id);
            if (bmi.UserId != user.Id)
            {
                return RedirectToAction("HealthCheck", "Bmi");
            }
            return View(bmi);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteBmi(BmiUser model)
        {
            if (ModelState.IsValid)
            {
                await _service.DeleteBmi(model);
                return RedirectToAction("HealthCheck", "Bmi");
            }
            return View();
        }




    }
}
