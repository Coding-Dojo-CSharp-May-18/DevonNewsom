using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreEFunizes.Models;

namespace MoreEFunizes.Controllers
{
    delegate bool IsCoolest(QuoteUser guy);

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
            List<Quote> recentQuotes = _dbContext.Quotes
                .Include(q => q.Creator)
                .OrderByDescending(q => q.CreatedAt)
                .Take(5)    
                .ToList();

            int topQuoteCount = _dbContext.Users.Max(us => us.SelectedQuotes.Count());

            QuoteUser userWithMostQuotes = _dbContext.Users
                //           => user 
                .FirstOrDefault(u => u.SelectedQuotes.Count() == topQuoteCount);

            QuoteDash model = new QuoteDash()
            {
                RecentQuotes = recentQuotes,
                TopUser = userWithMostQuotes
            };
            return View(model);
        }
        // Create a new quotes
        [HttpPost("create")]
        public IActionResult Create(Quote newQuote)
        {
            // if valid
            if(ModelState.IsValid)
            {
                // get UserID on this thing!!!

                _dbContext.Quotes.Add(newQuote);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }
    }
}
