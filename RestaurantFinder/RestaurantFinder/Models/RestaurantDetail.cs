using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantFinder.Models
{
    public class RestaurantDetail
    {
        public RestaurantDetail()
        {
            Reviews = new List<RestaurantReview>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Cuisine { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string PhoneNumber { get; set; }
        [Range(0, 1000)]
        [DataType(DataType.Currency)]
        public decimal? PriceRange { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.0000000}", ApplyFormatInEditMode = true)]
        public decimal Latitude { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.0000000}", ApplyFormatInEditMode = true)]
        public decimal Longitude { get; set; }

        public virtual ICollection<RestaurantReview> Reviews { get; set; }

    }
}