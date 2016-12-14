using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantFinder.Models
{
    public class SortLocations
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Cuisine { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? PriceRange { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public decimal AddressLatitude { get; set; }
        public decimal AddressLongitude { get; set; }

        public decimal AddressDistance
        {
            get
            {
                return Convert.ToDecimal(
                    Math.Sqrt(
                        Math.Pow(Convert.ToDouble(this.Latitude - this.AddressLatitude), 2.0)
                        +
                        Math.Pow(Convert.ToDouble(this.Longitude - this.AddressLongitude), 2.0)
                    ) * 62.1371192
                );
            }
        }

        public string DisplayAddressDistance
        {
            get
            {
                return this.AddressDistance.ToString("0.00 Miles.");
            }
        }

        public string WholeString
        {
            get
            {
                var joinAddress = new List<string>(5);

                if (!string.IsNullOrEmpty(this.Address)) { joinAddress.Add(this.Address); }
                if (!string.IsNullOrEmpty(this.Area)) { joinAddress.Add(this.Area); }
                if (!string.IsNullOrEmpty(this.City)) { joinAddress.Add(this.City); }
                if (!string.IsNullOrEmpty(this.Country)) { joinAddress.Add(this.Country); }
                if (!string.IsNullOrEmpty(this.Postcode)) { joinAddress.Add(this.Postcode); }

                return string.Join(", ", joinAddress.ToArray());
            }
        }

    }
}