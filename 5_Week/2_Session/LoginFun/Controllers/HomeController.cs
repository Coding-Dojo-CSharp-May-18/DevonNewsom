using System.Collections.Generic;
using System.Linq;
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


            List<Dictionary<string, object>> users = DbConnector.Query(SQL);
            return users.Count == 0;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
       [HttpPost("Registration")]
       public IActionResult Registration(LogregViewModel model)
       {
           System.Console.WriteLine("\nMADE IT TO REG!!!!!!\n");
           RegisterUser user = model.Registration;
           if(ModelState.IsValid)
           {
               // Is UserName unique?
               
               // query => .Query() => 

             

                // HASH IT
                PasswordHasher<RegisterUser> hasher = new PasswordHasher<RegisterUser>();

                string hashed = hasher.HashPassword(user, user.Password);

                // INSERT INTO users (Name, Username, Password) VALUES (Franz, sLDSkjfLKSJI#ODFOJKDSJFKS, swagBOI);
                string SQL = $@"INSERT into users (Name, Username, Password) VALUES ('{user.Name}', '{user.Username}',  '{hashed}');";
                System.Console.WriteLine($"\n{SQL}");
                DbConnector.Execute(SQL);
                
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
        public IActionResult Login(LogregViewModel model)
        {
            LoginUser user = model.Login;
            string SQL = "SELECT UserId, Password FROM users WHERE Username = '{}'";
            var result = DbConnector.Query(SQL);

            if(ModelState.IsValid)
            {
                if(UserNameUnique(user.Username))
                    ModelState.AddModelError("UserName", "Username is not in DB");
                // DB Checks

                else
                {
                    string HashedPW = (string)result.First()["Password"];
                    // QUERY DB => Dictionary["Password"]
                    
                    

                    PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();

                    if(hasher.VerifyHashedPassword(user, HashedPW, user.Password) == PasswordVerificationResult.Failed)
                        ModelState.AddModelError("UserName", "Username is not in DB");

                }
            }

            if(ModelState.IsValid)
            {
                // result of some query
                int userId = (int)result.First()["UserId"];

                HttpContext.Session.SetInt32("id", userId);
                return RedirectToAction("Profile", new {userid = userId});   
            }
                
            return View("Index");
        }

        [HttpGet("{userid}")]
        public IActionResult Profile(int userid)
        {
            List<Dictionary<string, object>> result = DbConnector.Query($"SELECT * FROM users WHERE UserId = '{userid}'");

            if(result.Count < 1)
                return Json("WHOOPS");

            var userResult = result.First();

            // theUser = dapperThing.GetUserWithId(userid);

            BaseUser theUser = new BaseUser()
            {
                Name = (string)userResult["Name"],
                Username = (string)userResult["Username"],
                UserId = (int)userResult["UserId"]
            };

            return View(theUser);
        }
    }
}