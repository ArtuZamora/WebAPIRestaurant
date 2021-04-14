using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIRestaurant.Models
{
    public class DishCE
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Cooking { get; set; }
        public string Area { get; set; }
        public string ImageURL { get; set; }
        public CategoryCE Category { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Tags { get; set; }
    }
}