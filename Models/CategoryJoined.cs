using System.ComponentModel.DataAnnotations;

namespace CedarWebApp.Models
{
    public class CategoryJoined
    {
        public int CategoryJoinedId { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }

    }
}