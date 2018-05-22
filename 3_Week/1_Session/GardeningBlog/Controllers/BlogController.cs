using System;
using Microsoft.AspNetCore.Mvc;

namespace GardeningBlog.Controllers
{
    public class BlogController : Controller
    {
        // :5000/
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        // :5000/create
        [HttpPost("create")]
        public IActionResult NewBlog(string title, string content)
        {
            // do something cool with new blog post

            ViewBag.NewTitle = title;
            ViewBag.NewContent = content;
            ViewBag.CreatedAt = DateTime.Now;
            ViewBag.Things = new string[]
            {
                "thing 1", "thing 2"
            };

            // => /Views/Shared/NewBlog.cshtml
            // => /Views/Blog/NewBlog.cshtml
            return View();
        }
        [HttpGet("blog")]
        public IActionResult NewBlog()
        {
            return View();
        }
    }
}