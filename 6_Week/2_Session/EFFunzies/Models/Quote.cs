using System;
using System.ComponentModel.DataAnnotations;

namespace EFFunzies.Models
{
    public class Quote
    {
        [Key]
        public int QuoteId {get;set;}
        [Required]
        [MinLength(10)]
        public string Content {get;set;}
        public string Byline {get;set;}
        public DateTime CreatedAt {get;set;}
    }
}