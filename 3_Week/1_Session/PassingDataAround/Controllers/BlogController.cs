using Microsoft.AspNetCore.Mvc;
using PassingDataAround.Models;

namespace PassingDataAround.Controllers
{
    public class BlogController : Controller
    {
        [HttpGet("")]
        public ViewResult Index()
        {
            // Your View lives: /Views/Blog/Index.cstml
            return View();
        }
        [HttpPost("create")]
        public IActionResult Create(Blog newBlog)
        {
            // lets pass this data to a view!!
            // View will be /Views/Blog/Results.cshtml
            ViewBag.NewTitle = newBlog.Title;
            ViewBag.NewContent = newBlog.Content;

            ViewBag.NewBlog = newBlog;



            // ViewData["NewTitle"]
            // ViewData["NewContent"]


            return View("Results");
        }
    }
}