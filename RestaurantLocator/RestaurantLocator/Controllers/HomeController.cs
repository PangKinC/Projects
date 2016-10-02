using RestaurantLocator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RestaurantLocator.Controllers
{
    public class HomeController : Controller
    {
        private RestaurantDBEntities db = new RestaurantDBEntities();

        // GET: Home
        public ActionResult Index()
        {
            RestaurantView resView = new RestaurantView();
            resView.allRes = (from r in db.Restaurants select r).ToList();
            resView.allRate = (from t in db.Rates select t).ToList(); 

            return View(resView);
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string address)
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
            Rate rate = db.Rates.Find(id);
            if (rate != null) { db.Rates.Remove(rate); }

            Restaurant res = db.Restaurants.Find(id);
            db.Restaurants.Remove(res);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RCReview(int id)
        {
            /*var rate = db.Rates.Where(r => r.Restaurant_ID == id);
            R
            return View(rate);*/       
            Rate rate = new Rate();
            Restaurant res = db.Restaurants.Find(id);
            RestaurantView rv = new RestaurantView();
            rv.rate = rate;
            rv.res = res;
            return View(rv);
        }

        [HttpPost]
        public ActionResult RCReview(RestaurantView rv)
        {
            if (ModelState.IsValid)
            {
                Rate rate = new Rate();
                rate.Review = rv.rate.Review;
                rate.Restaurant_ID = rv.res.ID;

                db.Rates.Add(rate);
                db.SaveChanges();
                return View(rv);
            }
            else { return View(rv); }
        }

        public ViewResult ReviewList(int id)
        {
            /*SpecificReview reviews = new SpecificReview { checkReview = db.Rates.Where(r => r.Restaurant_ID == id).OrderBy(p => p.ID) };

            return PartialView(reviews);*/

            //return PartialView(db.Rates.ToList());

           return View(db.Rates.Where
                (r => r.Restaurant_ID == id)
                .OrderByDescending(p => p.ID).
                ToList());
        }

        public ActionResult DeleteReview(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            Rate rate = db.Rates.Find(id);
            if (rate == null) { return HttpNotFound(); }
            return View(rate);
        }

        [HttpPost, ActionName("DeleteReview")]
        public ActionResult DeleteReviewC(int? id)
        {
            Rate rate = db.Rates.Find(id);
            db.Rates.Remove(rate);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(Account acct)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Form is not valid; please review and try again.";
                return View("Login");
            }

            bool validEmail = db.Accounts.Any(e => e.Email_Address == acct.Email_Address);
            bool validPassword = db.Accounts.Any(e => e.Password == acct.Password);

            if ((validEmail == true) && (validPassword == true))
            {
                FormsAuthentication.RedirectFromLoginPage(acct.Email_Address, true);
            }

            ViewBag.Error = "Credentials invalid. Please try again.";
            return View("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}