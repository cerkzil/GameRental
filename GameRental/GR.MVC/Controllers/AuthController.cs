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
            _adminUser = new User() { Email = "milk@milk.com", Password = "pass", IsAdmin = true, Id = 1, Platforms = {Platform.PS5, Platform.XBOX} };
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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
