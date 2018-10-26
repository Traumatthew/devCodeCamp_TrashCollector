using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            if (User.IsInRole(RoleNames.Customer))
            {
                return RedirectToAction("Index", "Customers");
            }
            else if (User.IsInRole(RoleNames.Employee))
            {
                return RedirectToAction("Index", "Employees");
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}