//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPIRestaurant.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dish_Ingredient
    {
        public int ID { get; set; }
        public int ID_Dish { get; set; }
        public int ID_Ingredient { get; set; }
        public string Quantity { get; set; }
    
        public virtual Dish Dish { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}