using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelFun.Models;

namespace ModelFun.Controllers
{
    public class FriendController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
           
            return View();
        }
       
        [HttpPost("create")]
        public IActionResult Create(Friend newFriend)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("SomewhereCool");
            }
            return View("Index");

        }
        [HttpGet("SOMEWHERECOOL")]
        public string SomewhereCool()
        {
            return "SO COOL HERE GUYS!";
        }
    }
}
