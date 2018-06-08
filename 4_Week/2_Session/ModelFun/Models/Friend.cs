using System;
using System.ComponentModel.DataAnnotations;

namespace ModelFun.Models
{
    public class Friend
    {
        [Required(ErrorMessage="CUSTOM THING EHERE")]
        [MinLength(3)]
        [MaxLength(20)]
        public string NickName {get;set;}
        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB {get;set;}
        [Required]
        [Range(0, 20, ErrorMessage="NUM HANDS must be normal")]
        public int NumHands {get;set;}
        
    }
    public class WeirdModelThing
    {
        public Friend BestFriend {get;set;}
        public Friend NewFriend {get;set;}
        public Friend[] MyFriends {get;set;}
        public bool HasPizza {get;set;}
    }
}