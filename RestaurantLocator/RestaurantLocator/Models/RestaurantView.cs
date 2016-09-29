using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantLocator.Models
{
    public class RestaurantView
    {
        public Rate rate { get; set; }
        public Restaurant res { get; set; }
        public IList<Restaurant> allRes { get; set; }
        public IList<Rate> allRate { get;  set;}
    }
}