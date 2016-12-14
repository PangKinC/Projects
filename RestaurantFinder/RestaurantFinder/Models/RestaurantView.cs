using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantFinder.Models
{
    public class RestaurantView
    {
        public RestaurantDetail res { get; set; }
        public RestaurantReview rev { get; set; }
        public IList<RestaurantDetail> allRes { get; set; }
        public IList<RestaurantReview> allRev { get;  set;}
    }
}