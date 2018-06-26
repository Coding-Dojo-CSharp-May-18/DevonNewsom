using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoreEFunizes.Models;

namespace MoreEFunizes.Controllers
{
    public class HomeController : Controller
    {
        private QuoteContext _dbContext;
        public HomeController(QuoteContext context)
        {
            _dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
          
            return View();
        }
        // Create a new quotes
        [HttpPost("create")]
        public IActionResult Register(LogReg model)
        {
            RegisterUser newUser = model.RegUser;
            // if valid
            if(ModelState.IsValid)
            {
                QuoteUser convertedUser = new QuoteUser()
                {
                    Name = newUser.Name,
                    Username = newUser.Username,
                    Password = newUser.Password
                };
                
                PasswordHasher<QuoteUser> hasher = new PasswordHasher<QuoteUser>();
                convertedUser.Password = hasher.HashPassword(convertedUser, convertedUser.Password);
                HttpContext.Session.SetInt32("id", _dbContext.Users.Add(convertedUser).Entity.UserId);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Quotes");
            }

            return View("Index");
        }
    }
}
