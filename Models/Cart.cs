using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CedarWebApp.Models
{
    public class Cart
    {
        [Key]
        public int CartId {get;set;}

        [Required]
        public int UserId {get;set;}

        public List<FoodJoined> FoodItemsJoined { get; set; }
    }
}