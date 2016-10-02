using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantLocator.Models
{
    public class SortLocations : Restaurant
    {
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
                var joinAddress = new List<string>(4);

                if (!string.IsNullOrEmpty(this.Address)) { joinAddress.Add(this.Address); }
                if (!string.IsNullOrEmpty(this.City)) { joinAddress.Add(this.City); }
                if (!string.IsNullOrEmpty(this.Country)) { joinAddress.Add(this.Country); }
                if (!string.IsNullOrEmpty(this.Postcode)) { joinAddress.Add(this.Postcode); }

                return string.Join(", ", joinAddress.ToArray());
            }
        }

    }
}