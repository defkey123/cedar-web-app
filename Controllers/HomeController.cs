using System;
using System.IO;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment _environment;

        public HomeController(CedarContext context, IHostingEnvironment IHostingEnvironment)
        {
            dbContext = context;
            _environment = IHostingEnvironment;
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

        [HttpGet("foods")]
        public IActionResult Foods()
        {
            int? userId = HttpContext.Session.GetInt32("logged_in_id");
            if (userId is null) return RedirectToAction("LoginReg");

            FoodDashboard dash = new FoodDashboard();
            dash.CurrentUser = dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
            dash.Foods = dbContext.Foods.ToList();

            return View(dash);
        }

        [HttpGet("foods/new")]
        public IActionResult NewFood()
        {
            int? userId = HttpContext.Session.GetInt32("logged_in_id");
            if (userId is null) return RedirectToAction("LoginReg");

            return View();
        }

        [HttpPost("foods/new")]
        public IActionResult NewFood(FoodItem foodSubmission)
        {
            int? userId = HttpContext.Session.GetInt32("logged_in_id");
            if (userId is null) return RedirectToAction("LoginReg");

            if (ModelState.IsValid)
            {
                var foodInDb = dbContext.Foods.FirstOrDefault(u => u.Name == foodSubmission.Name);

                if (foodInDb is null)
                {

                    //path in db to img name
                    string PathDB = string.Empty;

                    //image to ImgUrl in db
                    var newFileName = string.Empty;

                    if (HttpContext.Request.Form.Files != null)
                    {
                        var fileName = string.Empty;

                        var files = HttpContext.Request.Form.Files;

                        foreach (var file in files)
                        {
                            if (file.Length > 0)
                            {
                                //Getting FileName
                                fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                                //Assigning Unique Filename (Guid)
                                var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                                //Getting file Extension
                                var FileExtension = Path.GetExtension(fileName);

                                // concating  FileName + FileExtension
                                newFileName = myUniqueFileName + FileExtension;

                                // Combines two strings into a path.
                                fileName = Path.Combine(_environment.WebRootPath, "user-upload") + $@"\{newFileName}";

                                // if you want to store path of folder in database
                                PathDB = "user-upload/" + newFileName;

                                using (FileStream fs = System.IO.File.Create(fileName))
                                {
                                    file.CopyTo(fs);
                                    fs.Flush();
                                }
                            }
                        }
                    }

                    foodSubmission.ImageUrl = PathDB;
                    dbContext.Foods.Add(foodSubmission);
                    dbContext.SaveChanges();

                    return RedirectToAction("Foods");
                }
                else
                {
                    ModelState.AddModelError("Name", "A food already exists with this name!");
                    return View("NewFood");
                }

            }
            return View("NewFood");
        }

    }
}
