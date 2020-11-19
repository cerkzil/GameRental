using GR.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GR.Domains;
using GR.Domains.Enum;

namespace GR.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private User _adminUser;
        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;

            _adminUser = new User()
            {
                Id = 1,
                Email = "milk@milk.com",
                Password = "pass",
                IsAdmin = true,
                Platforms = new List<Platform>()
                    {
                    Platform.PS5,
                    Platform.SWITCH,
                    Platform.XBOX
                    }
            };
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AuthError = "Bad login details try again!";
                return View("~/Views/Auth/Login.cshtml");
            }

            //check if it is admin
            if (model.Email == _adminUser.Email && model.Password == _adminUser.Password)
            {
                return RedirectToAction("Index", "Admin");
            }


            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
