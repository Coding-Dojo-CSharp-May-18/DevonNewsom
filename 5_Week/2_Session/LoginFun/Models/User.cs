using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoginFun.Models
{
    public class BaseUser
    {
        public int UserId {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        [UserNameUnique]
        public string Username {get;set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password {get;set;}
    }
    
    public class RegisterUser : BaseUser
    {
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Confirm {get;set;}
    }
    public class LoginUser
    {
        [Required]
        public string Username {get;set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password {get;set;}
    }

    public class UserNameUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // from the FORM
            string username = (string)value;
            string SQL = $"SELECT * FROM users WHERE username = '{username}'";

            string ternary = (true) ? "TRUE" : "FALSE";
            
            List<Dictionary<string, object>> users = new List<Dictionary<string, object>>();

            ValidationResult failed = new ValidationResult("Username is Taken");

            return (users.Count != 0)
                ? failed
                : ValidationResult.Success;

        }
    }
}