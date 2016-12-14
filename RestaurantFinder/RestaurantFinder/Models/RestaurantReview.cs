using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantFinder.Models
{
    public class RestaurantReview
    {
        public RestaurantReview() { }
        public int ID { get; set; }
        public int RestaurantID { get; set; }
        public string Reviewer { get; set; }
        public string Review { get; set; }

        [ForeignKey("RestaurantID")]
        public virtual RestaurantDetail Restaurants { get; set; }
    }
}