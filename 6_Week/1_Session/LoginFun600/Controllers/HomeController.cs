using System.Collections.Generic;
using System.Linq;
using LoginFun.Models;
using LoginFun.Factories;
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
        private UserFactory _userFactory;


        public HomeController()
        {
            _userFactory = new UserFactory();
        }
        HowImDoin myFeelingsNow = HowImDoin.Happy;

        
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
                if(_userFactory.UsernameExists(user.Username))
                    ModelState.AddModelError("Username", "Username exists");


                // HASH IT
                PasswordHasher<RegisterUser> hasher = new PasswordHasher<RegisterUser>();
                string hashed = hasher.HashPassword(user, user.Password);

                user.Password = hashed;
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
        public IActionResult Login(LogregViewModel model)
        {
            LoginUser user = model.Login;
            string SQL = "SELECT UserId, Password FROM users WHERE Username = '{}'";
            var result = DbConnector.Query(SQL);

            if(ModelState.IsValid)
            {
                // ! => NOT
                if(!_userFactory.UsernameExists(user.Username))
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
            BaseUser user = _userFactory.GetUserById(userid);
            return View(user);
        } 
    }
}