using System.Collections.Generic;
using LoginFun.Models;
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

        public bool UserNameUnique(string un)
        {
            string SQL = $"SELECT * FROM users WHERE username = '{un}'";
            List<Dictionary<string, object>> users = new List<Dictionary<string, object>>();
            return users.Count != 0;
        }
       [HttpPost("Registration")]
       public IActionResult Registration(RegisterUser user)
       {
           if(ModelState.IsValid)
           {
               // Is UserName unique?
               
               // query => .Query() => 

             

                // HASH IT
                PasswordHasher<RegisterUser> hasher = new PasswordHasher<RegisterUser>();

                string hashed = hasher.HashPassword(user, user.Password);
                string SQL = $@"INSERT ... {user.Name} {hashed} {user.UserName};";
                
            }
                // LOG EM IN
            if(ModelState.IsValid)
            {
                string SQL = $@"SELECT LAST_INSERT_ID() AS newUserId";
                
                Dictionary<string, object> newUser = new Dictionary<string, object>();
                HttpContext.Session.SetInt32("id", (int)newUser["id"]);
            }
            return View("LoginView");

           
       }
       [HttpPost("Login")]
       public IActionResult Login(LoginUser user)
        {
            if(ModelState.IsValid)
            {
                if(UserNameUnique(user.UserName))
                    ModelState.AddModelError("UserName", "Username is not in DB");
               // DB Checks

               else
               {
                   // QUERY DB => Dictionary["Password"]
                   string hashedPW = "LOL";
                   PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();

                    if(hasher.VerifyHashedPassword(user, hashedPW, user.Password) == PasswordVerificationResult.Failed)
                        ModelState.AddModelError("UserName", "Username is not in DB");

               }
            }

            if(ModelState.IsValid)
            {
                // result of some query
                int userId = 5;

                HttpContext.Session.SetInt32("id", userId);
                return RedirectToAction("SomewhereCool", "Cool");   
            }
                
            return View("LoginView");
       }
    }
}