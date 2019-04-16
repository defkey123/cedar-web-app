using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CedarWebApp.Models;

namespace CedarWebApp.Controllers
{
    public class HomeController : Controller
    {
        private CedarContext dbContext;
        public HomeController(CedarContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        [HttpGet("login")]
        public IActionResult LoginReg()
        {
            int? userId = HttpContext.Session.GetInt32("logged_in_id");
            if (userId != null) return RedirectToAction("Dashboard");

            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            // Check initial ModelState
            if (ModelState.IsValid)
            {
                // If a User exists with provided Username
                if (dbContext.Users.Any(u => u.Username == user.Username))
                {
                    // Manually add a ModelState error to the Username field, with provided
                    // error message
                    ModelState.AddModelError("Username", "Username already in use!");

                    return View("LoginReg");
                }
                PasswordHasher<User> hasher = new PasswordHasher<User>();

                user.Password = hasher.HashPassword(user, user.Password);
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("logged_in_id", user.UserId);
                return RedirectToAction("Dashboard");
            }
            return View("LoginReg");
        }

        [HttpPost("confirmlogin")]
        public IActionResult Login(LoginUser userSubmission)
        {
            if (ModelState.IsValid)
            {
                // If inital ModelState is valid, query for a user with provided Username
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Username == userSubmission.LoginUsername);
                // If no user exists with provided Username
                if (userInDb is null)
                {
                    // Add an error to ModelState and return to View!
                    ModelState.AddModelError("LoginUsername", "Invalid Username/Password");
                    return View("LoginReg");
                }

                // Initialize hasher object
                var hasher = new PasswordHasher<LoginUser>();

                // varify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LoginPassword);

                // result can be compared to 0 for failure
                if (result == 0)
                {
                    return View("LoginReg");
                }
                else
                {
                    HttpContext.Session.SetInt32("logged_in_id", userInDb.UserId);
                    return RedirectToAction("Dashboard");
                }
            }
            return View("LoginReg");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Dashboard");
        }

        [HttpGet("home")]
        public IActionResult Dashboard()
        {
            int? userId = HttpContext.Session.GetInt32("logged_in_id");
            if (userId is null) return RedirectToAction("LoginReg");

            User CurrentUser = dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);

            return View(CurrentUser);
        }

        [HttpGet("menus")]
        public IActionResult Menus()
        {
            int? userId = HttpContext.Session.GetInt32("logged_in_id");
            if (userId is null) return RedirectToAction("LoginReg");

            User CurrentUser = dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);

            return View(CurrentUser);
        }

    }
}
