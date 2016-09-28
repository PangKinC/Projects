using RestaurantLocator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RestaurantLocator.Controllers
{
    public class HomeController : Controller
    {
        private RestaurantDBEntities db = new RestaurantDBEntities();

        // GET: Home
        public ActionResult Index()
        {
            var restaurants = from m in db.Restaurants
                              select m;

            return View(restaurants);
        }

        [HttpPost]
        public ActionResult Index(string address)
        {
            var restaurants = from m in db.Restaurants
                              select m;

            if (string.IsNullOrEmpty(address)) { return View(restaurants); }

            var matchFound = GoogleMapsAPIHelpersCS.GetGeocodingSearchResults(address);
            var matchCount = matchFound.Elements("result").Count();

            if (matchCount == 0) {
                ViewData["NoMatch"] = true;
                return View(restaurants);
            }
            else if (matchCount == 1) {
                return RedirectToAction("AddressResult", new { Address = matchFound.Element("result").Element("formatted_address").Value });
            }
            else {
                var matchList = from match in matchFound.Elements("result")
                                let formatted_address = match.Element("formatted_address")
                                .Value
                                select formatted_address;
                ViewData["MatchResults"] = matchList;

                return View(restaurants);
            }

        }

        public ActionResult AddressResult(string address)
        {
            if (string.IsNullOrEmpty(address)) { return RedirectToAction("Index"); }

            var matchFound = GoogleMapsAPIHelpersCS.GetGeocodingSearchResults(address);
            var ltde = Convert.ToDecimal(matchFound.Element("result").Element("geometry").Element("location").Element("lat").Value, NumberFormatInfo.InvariantInfo);
            var lgte = Convert.ToDecimal(matchFound.Element("result").Element("geometry").Element("location").Element("lng").Value, NumberFormatInfo.InvariantInfo);

            var nearbyRes = from r in db.Restaurants
                            where Math.Abs(r.Latitude - ltde) < 0.25M &&
                            Math.Abs(r.Longitude - lgte) < 0.25M
                            //select r;
                            select new SortLocations()
                            {
                                ID = r.ID,
                                Name = r.Name,
                                Cuisine = r.Cuisine,
                                Address = r.Address,
                                City = r.City,
                                Country = r.Country,
                                Postcode = r.Postcode,
                                Phone_Number = r.Phone_Number,
                                Price_Range = r.Price_Range,              
                                Latitude = r.Latitude,
                                Longitude = r.Longitude,
                                AddressLatitude = ltde,
                                AddressLongitude = lgte
                            };

            var sortedRes = nearbyRes.ToList().OrderBy(r => r.AddressDistance).ToList();
            return View(sortedRes);
        }

        public ActionResult CreateData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateData(Restaurant res)
        {
            if (ModelState.IsValid) {
                db.Restaurants.Add(res);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else { return View(res); }
        }
        public ActionResult ReadData(int? id)
        {
            Restaurant res = db.Restaurants.Find(id);
            return View(res);
        }

        public ActionResult UpdateData(int? id)
        {
            Restaurant res = db.Restaurants.Find(id);
            return View(res);
        }

        [HttpPost]
        public ActionResult UpdateData(Restaurant res)
        {
            if (ModelState.IsValid) {
                db.Entry(res).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else { return View(res); }
        }

        public ActionResult DeleteData(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            Restaurant res = db.Restaurants.Find(id);
            if (res == null) { return HttpNotFound(); }
            return View(res);
        }

        [HttpPost, ActionName("DeleteData")]
        public ActionResult DeleteConfirmation(int? id)
        {
            Restaurant res = db.Restaurants.Find(id);
            db.Restaurants.Remove(res);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateReview(int? id)
        {
            var rate = db.Rates.Where(r => r.Restaurant_ID == id);
            Restaurant res = db.Restaurants.Find(id);
            return View(res);
        }

    }

}