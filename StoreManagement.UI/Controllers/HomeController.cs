using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.BAL.Interfaces;
using StoreManagement.Common.DTOs;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StoreManagement.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthentication _authService;

        public HomeController(IAuthentication authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "LandingPage");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _authService.ValidateUser(username, password);
            if (user != null)
            {
                if (!user.is_approved)
                {
                    ViewBag.Error = "Your account is pending approval by the admin.";
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.Role, user.role),
                    new Claim("FullName", user.full_name)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "LandingPage");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(User model)
        {
            // Set default role as User
            model.role = "User"; 
            var result = await _authService.RegisterUser(model);
            if (result)
            {
                ViewBag.Success = "Registration successful! Please wait for admin approval.";
                return View("Login");
            }

            ViewBag.Error = "Registration failed. Please try again.";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
