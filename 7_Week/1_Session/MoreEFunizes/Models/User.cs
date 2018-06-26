using System.ComponentModel.DataAnnotations;

namespace MoreEFunizes.Models
{
    public class QuoteUser
    {
        [Key]
        public int UserId {get;set;}
        public string Name {get;set;}
        public string Username {get;set;}
        [DataType(DataType.Password)]
        public string Password {get;set;}
        public System.DateTime CreatedAt {get;set;}
    }
    public class RegisterUser : QuoteUser
    {
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Confirm {get;set;}
    }
    public class LogReg
    {
        public RegisterUser RegUser {get;set;}
        public QuoteUser LogUser {get;set;}
    }
}