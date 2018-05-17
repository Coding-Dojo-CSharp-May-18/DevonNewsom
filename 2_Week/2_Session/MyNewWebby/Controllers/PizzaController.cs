using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyNewWebby.Models;

namespace MyNewWebby.Controllers
{
    public class PizzaController : Controller
    {
        // take in input (as web request)
        // localhost:5000/pizza
        [Route("")]
        public ViewResult Index()
        {
            return View();
        }
        [Route("create")]
        public ViewResult Pizza()
        {

            return View();
        }
        // @app.route("/")
        [Route("show/{toppings}")]
        public IActionResult PizzaWithToppings(string toppings)
        {
            if(toppings == "pep")
                return View("Pepperoni");

            return RedirectToAction("Index");
        }
        [Route("delete/api")]
        public JsonResult PizzaApi()
        {
            Pizza myPizza = new Pizza()
            {
                Name = "BigTyme Pizza!",
                Toppings = new string[]
                {
                    "Pep", "Extra-Cheese", "Jalapenos"
                }
            };
            return Json(myPizza);
        }

    }
}