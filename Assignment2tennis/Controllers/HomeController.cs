using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assignment2tennis.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment2tennis.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        public HomeController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            await GivememberRole();
            return View();
        }

        [Authorize]
        [HttpGet]
        // giving a member role to the new user 
        public async Task<IActionResult> GivememberRole()
        {
            var member = await GetCurrentUserAsync();
            var memberId = member?.Id;
            string username = member?.Email;
            if ((User.IsInRole("Admin")) || (User.IsInRole("Coach")) || (User.IsInRole("Member")))
            {

            }
            else
            {
                var _userManager = HttpContext.RequestServices
                   .GetRequiredService<UserManager<IdentityUser>>();
                var signInManager = HttpContext.RequestServices
                    .GetRequiredService<SignInManager<IdentityUser>>();

                await _userManager.AddToRoleAsync(member, "Member");
                await signInManager.RefreshSignInAsync(member);

            }

            return RedirectToAction("index", "home");

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            var users = _userManager.Users;

            return View("about", users);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
