//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestaurantLocator.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account
    {
        public int ID { get; set; }
        public string Email_Address { get; set; }
        public string Password { get; set; }
        public bool Is_Admin { get; set; }
    }
}