using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoreEFunizes.Models;

namespace MoreEFunizes.Controllers
{
    [Route("quotes")]
    public class QuotesController : Controller
    {
        private QuoteContext _dbContext;
        public QuotesController(QuoteContext context)
        {
            _dbContext = context;
        }
        [HttpGet("")]
        // View will use List<Quotes> as model!
        public IActionResult Index()
        {
            List<Quote> model = _dbContext.Quotes
                .OrderByDescending(q => q.CreatedAt)
                .Take(5)    
                .ToList();

            return View(model);
        }
        // Create a new quotes
        [HttpPost("create")]
        public IActionResult Create(Quote newQuote)
        {
            // if valid
            if(ModelState.IsValid)
            {
                _dbContext.Quotes.Add(newQuote);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }
    }
}
