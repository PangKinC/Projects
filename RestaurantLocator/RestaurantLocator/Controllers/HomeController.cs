using RestaurantLocator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    }

}