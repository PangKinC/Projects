namespace RestaurantFinder.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using RestaurantFinder.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<RestaurantFinder.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RestaurantFinder.Models.ApplicationDbContext context)
        {

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
 
            if (!roleManager.RoleExists("Administrator"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Administrator";
                roleManager.Create(role);
             
                var user = new ApplicationUser();
                user.UserName = "admin001";
                user.Email = "kinchungpang@gmail.com";
                string pwString = "Kuroi21-Yami";

                var createUser = UserManager.Create(user, pwString);

                if (createUser.Succeeded)
                {
                    var addRole = UserManager.AddToRole(user.Id, "Administrator");
                }
            }

            if (!roleManager.RoleExists("Guest"))
            { 
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Guest";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "guest001";
                user.Email = "guest@test.com";
                string pwString = "Password-100";

                var createUser = UserManager.Create(user, pwString);

                if (createUser.Succeeded)
                {
                    var addRole = UserManager.AddToRole(user.Id, "Guest");
                }
            }

            var restaurants = new List<RestaurantDetail> {

               new RestaurantDetail { Name="Fu Lam", Cuisine="Chinese", Address="215 Forest Road", Area="Walthamstow", City="London",
                    Country="United Kingdom", Postcode="E17 6HE", PriceRange=10.00m, PhoneNumber="02085271457", Latitude=51.587862m, Longitude=-0.035292m },

               new RestaurantDetail { Name="MASH", Cuisine="Steahouse", Address="77 Brewer Street", Area="Soho", City="London",
                    Country="United Kingdom", Postcode="W1F 9ZN", PriceRange=55.00m, PhoneNumber="02077342608", Latitude=51.510728m, Longitude=-0.136926m },

               new RestaurantDetail { Name="Burger and Lobster", Cuisine="American", Address="36-38 Dean Street", Area="Soho", City="London",
                    Country="United Kingdom", Postcode="W1D 4PS", PriceRange=22.50m, PhoneNumber="02074324800", Latitude=51.513510m, Longitude=-0.132071m },

               new RestaurantDetail { Name="Flat Iron", Cuisine="Steakhouse", Address="17 Beak Street", Area="Soho", City="London",
                    Country="United Kingdom", Postcode="W1F 9RW", PriceRange=17.50m, PhoneNumber="Not Available", Latitude=51.512118m, Longitude=-0.138426m },

               new RestaurantDetail { Name="Shoryu", Cuisine="Japanese", Address="9 Regent Street", Area="Mayfair", City="London",
                    Country="United Kingdom", Postcode="SW1Y 4LR", PriceRange=17.50m, PhoneNumber="Not Available", Latitude=51.508335m, Longitude=-0.134041m },

               new RestaurantDetail { Name="Goodmans", Cuisine="Steakhouse", Address="24-26 Maddox Street", Area="Mayfair", City="London",
                    Country="United Kingdom", Postcode="W1S 1QH", PriceRange=35.00m, PhoneNumber="02074993776", Latitude=51.513115m, Longitude=-0.142320m },

               new RestaurantDetail { Name="Hawksmoor", Cuisine="Steakhouse", Address="157A Commercial Street", Area="Spitalfields", City="London",
                    Country="United Kingdom", Postcode="E1 6BJ", PriceRange=45.00m, PhoneNumber="02074264850", Latitude=51.521150m, Longitude=-0.075657m },

               new RestaurantDetail { Name="Foxlow", Cuisine="British", Address="71-73 Church Street", Area="Stoke Newtington", City="London",
                    Country="United Kingdom", Postcode="N16 0AS", PriceRange=27.50m, PhoneNumber="02074816377", Latitude=51.562061m, Longitude=-0.078100m },

               new RestaurantDetail { Name="Anatolia", Cuisine="Turkish", Address="53 Mare Street", Area="Hackney", City="London",
                    Country="United Kingdom", Postcode="E8 3NS", PriceRange=20.00m, PhoneNumber="02089862223", Latitude=51.542780m, Longitude=-0.055636m },

               new RestaurantDetail { Name="Le Relais de Venise", Cuisine="French", Address="18-20 Mackenzie Walk", Area="Canary Wharf", City="London",
                    Country="United Kingdom", Postcode="E14 4PH", PriceRange=30.00m, PhoneNumber="02034753331", Latitude=51.504513m, Longitude=-0.022078m },

               new RestaurantDetail { Name="The White Swan", Cuisine="British", Address="108 Fetter Lane", Area="Holborn", City="London",
                    Country="United Kingdom", Postcode="EC4A 1ES", PriceRange=25.00m, PhoneNumber="02072429696", Latitude=51.516196m, Longitude=-0.109434m },

               new RestaurantDetail { Name="The Laughing Gravy", Cuisine="French", Address="154 Blackfriars Road", Area="Southwark", City="London",
                    Country="United Kingdom", Postcode="SW1Y 4LR", PriceRange=37.50m, PhoneNumber="02079981707", Latitude=51.501094m, Longitude=-0.104223m },

               new RestaurantDetail { Name="Vintage Salt", Cuisine="Seafood", Address="189 Upper Street", Area="Islington", City="London",
                    Country="United Kingdom", Postcode="N1 1RQ", PriceRange=27.50m, PhoneNumber="02074993776", Latitude=51.542608m, Longitude=-0.103350m },

               new RestaurantDetail { Name="Five Guys", Cuisine="Burger", Address="71 Upper Street", Area="Islington", City="London",
                    Country="United Kingdom", Postcode="N1 0NY", PriceRange=15.00m, PhoneNumber="02072267577", Latitude=51.535991m, Longitude=-0.104014m },

               new RestaurantDetail { Name="Roka", Cuisine="Japanese", Address="First Floor, 4 Park Pavillion, 40 Canada Square", Area="Canary Wharf", City="London",
                    Country="United Kingdom", Postcode="E14 5FW", PriceRange=30.00m, PhoneNumber="02076365228", Latitude=51.504872m, Longitude=-0.018878m },

               new RestaurantDetail { Name="Bistrotheque", Cuisine="British", Address="23-27 Wadeson Street", Area="Bethnal Green", City="London",
                    Country="United Kingdom", Postcode="E2 9DR", PriceRange=25.00m, PhoneNumber="02089837900", Latitude=51.534170m, Longitude=-0.055945m },

               new RestaurantDetail { Name="Ippudo", Cuisine="Japanese", Address="Mall Level -1, 1 Crossrail Place", Area="Canary Wharf", City="London",
                    Country="United Kingdom", Postcode="E14 5AR", PriceRange=30.00m, PhoneNumber="02033269485", Latitude=51.504954m, Longitude=-0.019450m },
            };

            restaurants.ForEach(r => context.Restaurants.AddOrUpdate(a => a.Address, r));
            context.SaveChanges();
        }
    }
}
