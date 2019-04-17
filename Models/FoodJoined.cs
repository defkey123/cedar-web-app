namespace CedarWebApp.Models
{
    public class FoodJoined
    {
        public int FoodJoinedId { get; set; }

        public int FoodId { get; set; }
        public FoodItem Food { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}