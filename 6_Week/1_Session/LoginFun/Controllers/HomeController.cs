using System.Collections.Generic;
using System.Linq;
using LoginFun.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginFun.Controllers
{
    enum HowImDoin
    {
        Happy,
        Sad,
        Grumpy
    }
    public class HomeController : Controller
    {
        HowImDoin myFeelingsNow = HowImDoin.Happy;

        private UserFactory _userFactory;

        public HomeController(UserFactory userService)
        {
            _userFactory = userService;
        }

       
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
       [HttpPost("Registration")]
       public IActionResult Registration(RegisterUser user)
       {
           if(ModelState.IsValid)
           {
                if(_userFactory.UsernameExists(user.Username))
                    ModelState.AddModelError("Username", "Username is in use!!");

             

                // HASH IT
                PasswordHasher<RegisterUser> hasher = new PasswordHasher<RegisterUser>();
                string hashed = hasher.HashPassword(user, user.Password);

                // Update user object with hash
                user.Password = hashed;

                // ask our dapper factory to create user
                _userFactory.CreateUser(user);
            }
                // LOG EM IN
            if(ModelState.IsValid)
            {

                TempData["message"] = "Now you may log in!";
               
                return RedirectToAction("Index");
            }
            return View("Index");

           
       }
        [HttpPost("Login")]
        public IActionResult Login(LoginUser user)
        {
            if(ModelState.IsValid)
            {
               if(!_userFactory.UsernameExists(user.Username))
                    ModelState.AddModelError("Username", "Username is not in DB");
                // DB Checks

                else
                {
                    string HashedPW = _userFactory.HashedPWFromUsername(user.Username);
                    
                    PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();

                    if(hasher.VerifyHashedPassword(user, HashedPW, user.Password) == PasswordVerificationResult.Failed)
                        ModelState.AddModelError("UserName", "Username is not in DB");
                }
            }

            if(ModelState.IsValid)
            {
                // result of some query
                int userId = 1;

                HttpContext.Session.SetInt32("id", userId);
                return RedirectToAction("Profile", new {userid = userId});   
            }
                
            return View("Index");
        }

        [HttpGet("user")]
        public IActionResult Profile(int userid)
        {
            // List<Dictionary<string, object>> result = DbConnector.Query($"SELECT * FROM users WHERE UserId = '{userid}'");

            // if(result.Count < 1)
                // return Json("WHOOPS");

            // var userResult = result.First();

            // theUser = dapperThing.GetUserWithId(userid);

            BaseUser theUser = new BaseUser()
            {
                Name = "Dummy",
                Username = "Artorias",
                UserId = 12
            };

            return View(theUser);
        }
    }
}