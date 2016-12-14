using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RestaurantFinder.CustomFilters;
using RestaurantFinder.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RestaurantFinder.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Home
        [AuthLog(Roles="Administrator")]
        public ActionResult AdminTools(string search, string resCuisine, string resArea, string revName)
        {
            RestaurantView resView = new RestaurantView(); 
            resView.allRes = (from r in db.Restaurants select r).ToList();
            resView.allRev = (from t in db.Reviews select t).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                resView.allRes = resView.allRes.ToList().Where(n => n.Name.Contains(search)).ToList();
                resView.allRev = resView.allRev.ToList().Where(n => n.Restaurants.Name.Contains(search)).ToList();
            }

            var cuisineFilter = new List<string>();
            var cuisineQuery = from c in db.Restaurants
                               orderby c.Cuisine
                               select c.Cuisine;

            cuisineFilter.AddRange(cuisineQuery.Distinct());
            ViewBag.resCuisine = new SelectList(cuisineFilter);

            if (!string.IsNullOrEmpty(resCuisine))
            {
                resView.allRes = resView.allRes.ToList().Where(c => c.Cuisine == resCuisine).ToList();
            }

            var areaFilter = new List<string>();
            var areaQuery = from a in db.Restaurants
                             orderby a.Area
                             select a.Area;

            areaFilter.AddRange(areaQuery.Distinct());
            ViewBag.resArea = new SelectList(areaFilter);

            if (!string.IsNullOrEmpty(resArea))
            {
                resView.allRes = resView.allRes.ToList().Where(a => a.Area == resArea).ToList();
            }

            var nameFilter = new List<string>();
            var nameQuery = from n in db.Reviews
                            orderby n.Restaurants.Name
                            select n.Restaurants.Name;

            nameFilter.AddRange(nameQuery.Distinct());
            ViewBag.revName = new SelectList(nameFilter);

            if (!string.IsNullOrEmpty(revName))
            {
                resView.allRev = resView.allRev.ToList().Where(a => a.Restaurants.Name == revName).ToList();
            }

            return View(resView);
        }

        [AuthLog(Roles="Guest, Administrator")]
        public ActionResult Index()
        {
            return View();
        }

        [AuthLog(Roles = "Guest, Administrator")]
        [HttpPost]
        public ActionResult Index(string address)
        {
            if (string.IsNullOrEmpty(address)) { return View(); }

            var matchFound = GoogleMapsAPIHelpersCS.GetGeocodingSearchResults(address);
            var matchCount = matchFound.Elements("result").Count();

            if (matchCount == 0) {
                ViewData["NoMatch"] = true;
                return View();
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

                return View();
            }

        }

        [AuthLog(Roles = "Guest, Administrator")]
        public ActionResult AddressResult(string address, string resCuisine, string resArea, string search)
        {
            if (string.IsNullOrEmpty(address)) { return RedirectToAction("Index"); }

            var matchFound = GoogleMapsAPIHelpersCS.GetGeocodingSearchResults(address);
            var ltde = Convert.ToDecimal(matchFound.Element("result").Element("geometry").Element("location").Element("lat").Value, NumberFormatInfo.InvariantInfo);
            var lgte = Convert.ToDecimal(matchFound.Element("result").Element("geometry").Element("location").Element("lng").Value, NumberFormatInfo.InvariantInfo);

            var nearbyRes = from r in db.Restaurants
                            where Math.Abs(r.Latitude - ltde) < 0.25M &&
                            Math.Abs(r.Longitude - lgte) < 0.25M
                            select new SortLocations()
                            {
                                ID = r.ID,
                                Name = r.Name,
                                Cuisine = r.Cuisine,
                                Address = r.Address,
                                Area = r.Area,
                                City = r.City,
                                Country = r.Country,
                                Postcode = r.Postcode,
                                PhoneNumber = r.PhoneNumber,
                                PriceRange = r.PriceRange,         
                                Latitude = r.Latitude,
                                Longitude = r.Longitude,
                                AddressLatitude = ltde,
                                AddressLongitude = lgte
                            };

            var sortedRes = nearbyRes.ToList().OrderBy(r => r.AddressDistance).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                sortedRes = nearbyRes.ToList().Where(n => n.Name.Contains(search)).ToList();
            }

            var cuisineFilter = new List<string>();
            var cuisineQuery = from c in db.Restaurants
                               orderby c.Cuisine
                               select c.Cuisine;

            cuisineFilter.AddRange(cuisineQuery.Distinct());
            ViewBag.resCuisine = new SelectList(cuisineFilter);

            if (!string.IsNullOrEmpty(resCuisine)) {
                sortedRes = nearbyRes.ToList().Where(c => c.Cuisine == resCuisine).ToList();
            }

            var areaFilter = new List<string>();
            var areaQuery = from a in db.Restaurants
                            orderby a.Area
                            select a.Area;

            areaFilter.AddRange(areaQuery.Distinct());
            ViewBag.resArea = new SelectList(areaFilter);

            if (!string.IsNullOrEmpty(resArea))
            {
                sortedRes = sortedRes.ToList().Where(a => a.Area == resArea).ToList();
            }


            return View(sortedRes);
        }

        [AuthLog(Roles = "Administrator")]
        public ActionResult CreateData()
        {
            return View();
        }

        [AuthLog(Roles = "Administrator")]
        [HttpPost]
        public ActionResult CreateData(RestaurantDetail res)
        {
            if (ModelState.IsValid) {
                db.Restaurants.Add(res);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else { return View(res); }
        }

        [AuthLog(Roles = "Guest, Administrator")]
        public ActionResult ReadData(int? id)
        {
            RestaurantDetail res = db.Restaurants.Find(id);
            return View(res);
        }

        [AuthLog(Roles = "Administrator")]
        public ActionResult UpdateData(int? id)
        {
            RestaurantDetail res = db.Restaurants.Find(id);
            return View(res);
        }

        [AuthLog(Roles = "Administrator")]
        [HttpPost]
        public ActionResult UpdateData(RestaurantDetail res)
        {
            if (ModelState.IsValid) {
                db.Entry(res).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else { return View(res); }
        }

        [AuthLog(Roles = "Administrator")]
        public ActionResult DeleteData(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            RestaurantDetail res = db.Restaurants.Find(id);
            if (res == null) { return HttpNotFound(); }
            return View(res);
        }

        [AuthLog(Roles = "Administrator")]
        [HttpPost, ActionName("DeleteData")]
        public ActionResult DeleteConfirmation(int? id)
        {
            RestaurantReview rev = db.Reviews.Find(id);
            if (rev != null) { db.Reviews.Remove(rev); }

            RestaurantDetail res = db.Restaurants.Find(id);
            db.Restaurants.Remove(res);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AuthLog(Roles = "Guest, Administrator")]
        public ActionResult RCReview(int id)
        {
            /*var rate = db.Rates.Where(r => r.Restaurant_ID == id);
            R
            return View(rate);*/       
            RestaurantReview rev = new RestaurantReview();
            RestaurantDetail res = db.Restaurants.Find(id);
            RestaurantView rv = new RestaurantView();
            rv.rev = rev;
            rv.res = res;
            return View(rv);
        }

        [AuthLog(Roles = "Guest, Administrator")]
        [HttpPost]
        public ActionResult RCReview(RestaurantView rv)
        {
            if (ModelState.IsValid)
            {
                RestaurantReview rev = new RestaurantReview();
                rev.Review = rv.rev.Review;
                rev.RestaurantID = rv.res.ID;
                rev.Reviewer = User.Identity.Name;

                db.Reviews.Add(rev);
                db.SaveChanges();
                return View(rv);
            }
            else { return View(rv); }
        }

        [AuthLog(Roles = "Guest, Administrator")]
        public ViewResult ReviewList(int id)
        {
            /*SpecificReview reviews = new SpecificReview { checkReview = db.Rates.Where(r => r.Restaurant_ID == id).OrderBy(p => p.ID) };

            return PartialView(reviews);*/

            //return PartialView(db.Rates.ToList());

           return View(db.Reviews.Where
                (r => r.RestaurantID == id)
                .OrderByDescending(p => p.ID).
                ToList());
        }

        [AuthLog(Roles = "Administrator")]
        public ActionResult DeleteReview(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            RestaurantReview rev = db.Reviews.Find(id);
            if (rev == null) { return HttpNotFound(); }
            return View(rev);
        }

        [AuthLog(Roles = "Administrator")]
        [HttpPost, ActionName("DeleteReview")]
        public ActionResult DeleteReviewC(int? id)
        {
            RestaurantReview rev = db.Reviews.Find(id);
            db.Reviews.Remove(rev);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}