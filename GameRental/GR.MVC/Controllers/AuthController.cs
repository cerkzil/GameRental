﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using GR.Domains;
using GR.MVC.Models;
using System.Threading.Tasks;

namespace GR.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SingInViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AuthError = "Something went wrong";
                return View("~/Views/Auth/Login.cshtml", model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser newUser = new AppUser()
                {
                    Email = model.Email,
                    UserName = model.Email
                };
                var result = await _userManager.CreateAsync(newUser, model.ConfirmPassword);

                if (result.Succeeded)
                {
                    if (EmailCheckForAdmin(model.Email))
                    {
                        await _userManager.AddToRoleAsync(newUser, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(newUser, "Member");
                    }
                    return RedirectToAction("Login"); // Redirect to logging
                }
            }
            return View("~/Views/Auth/Index.cshtml", model); //Redirect to logging
        }

        private bool EmailCheckForAdmin(string email)
        {
            return email.EndsWith(_configuration["Admin"]);
        }
    }
}