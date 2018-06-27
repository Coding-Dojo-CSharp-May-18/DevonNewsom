using Microsoft.EntityFrameworkCore;

namespace EFFunzies.Models
{
    public class QuoteContext : DbContext
    {
        public QuoteContext(DbContextOptions options) : base(options){}
        public DbSet<Quote> Quotes {get;set;}
    }
}