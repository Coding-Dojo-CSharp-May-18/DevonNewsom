using System;
using System.ComponentModel.DataAnnotations;

namespace MoreEFunizes.Models
{
    public class Quote
    {
        [Key]
        public int QuoteId {get;set;}
        [Required]
        [MinLength(10)]
        public string Content {get;set;}
        public string Byline {get;set;}
        public int UserId {get;set;}
        public DateTime CreatedAt {get;set;}
        // fAncy entity framework stuff
        public QuoteUser Creator {get;set;}
        public Quote()
        {
            CreatedAt = DateTime.Now;
        }
    }
}