using System.Collections.Generic;
using CedarWebApp.Models;

namespace CedarWebApp.Models
{
    public class FoodDashboard
    {
        public User CurrentUser { get; set; }
        public List<FoodItem> Foods { get; set; }

        // public List<Hobby> TopNovice { get; set; }
        // public List<Hobby> TopIntermediate { get; set; }
        // public List<Hobby> TopExpert { get; set; }
    }
}