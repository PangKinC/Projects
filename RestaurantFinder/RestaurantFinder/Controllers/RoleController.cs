using System;
using System.Linq;
using System.Web.Mvc;

using RestaurantFinder.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using RestaurantFinder.CustomFilters;

namespace A11_RBS.Controllers
{
    public class RoleController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        /// 
        [AuthLog(Roles = "Administrator")]
        public ActionResult Index()
        {
            var Roles = db.Roles.ToList();
            return View(Roles);
        }

        /// <summary>
        /// Create  a New role
        /// </summary>
        /// <returns></returns>
        [AuthLog(Roles = "Administrator")]
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        /// <summary>
        /// Create a New Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [AuthLog(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            db.Roles.Add(Role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}