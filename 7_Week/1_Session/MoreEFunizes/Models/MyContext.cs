using Microsoft.EntityFrameworkCore;

namespace MoreEFunizes.Models
{
    public class QuoteContext : DbContext
    {
        public QuoteContext(DbContextOptions options) : base(options){}
        public DbSet<QuoteUser> Users {get;set;}
        public DbSet<Quote> Quotes {get;set;}
    }
}