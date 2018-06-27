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
        [HttpPost("login")]
        public IActionResult Login(LogReg model)
        {
            QuoteUser user = model.LogUser;
            QuoteUser queriedUser = _dbContext.Users.SingleOrDefault(u => u.Username == user.Username);
            if(queriedUser == null)
            {
                ModelState.AddModelError("Username", "Invalid Username/Password");
                return View("Index");
            }
            if(ModelState.IsValid)
            {
                PasswordHasher<QuoteUser> hasher = new PasswordHasher<QuoteUser>();
                if(hasher.VerifyHashedPassword(user, queriedUser.Password, user.Password)
                    == PasswordVerificationResult.Failed)
                {
                    ModelState.AddModelError("Username", "Invalid Username/Password");
                    return View("Index");
                }
            }
            HttpContext.Session.SetInt32("id", queriedUser.UserId);
            return RedirectToAction("Index", "Quotes");
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
