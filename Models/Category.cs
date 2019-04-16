using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace CedarWebApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId {get;set;}

        [Required]
        public int UserId {get;set;}
        
        [Required]
        public string Name {get;set;}

        public string Description {get;set;}

        public string ImageUrl {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        public List<CategoryJoined> FoodsJoined { get; set; }
    }    
}