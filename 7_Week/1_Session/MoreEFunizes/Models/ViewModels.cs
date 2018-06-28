using System.Collections.Generic;

namespace MoreEFunizes.Models
{
    public class QuoteDash
    {
        public IEnumerable<Quote> RecentQuotes {get;set;}
        public QuoteUser TopUser {get;set;}
    }
}