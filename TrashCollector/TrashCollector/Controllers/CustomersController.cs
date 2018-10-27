using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class CustomersController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            var CAD = db.CustomerAccountDetails.ToList();
            var u = HttpContext.User.Identity.Name;
            var customer = db.Customers.Where(x => x.Email == u).FirstOrDefault();
            bool found = false;
            foreach (var item in CAD)
            {
                if(item.CustomerId == customer.ID)
                {
                    found = true;
                }
            }
            if(found == true)
            {
                var pickups = db.PickUpRequests.Where(x => x.CustomerId == customer.ID).ToList();
                var accountDet = db.CustomerAccountDetails.Where(x => x.CustomerId == customer.ID).FirstOrDefault();
                CustomerAndAccountViewModel viewmodel = new CustomerAndAccountViewModel() { cust = customer, account = accountDet, pickups = pickups };
                return View(viewmodel);
            }
            else
            {
                return RedirectToAction("Initial");
            }
        }

        public ActionResult Initial()
        {
            CustomerAccountDetails account = new CustomerAccountDetails();
            return View(account);
        }

        [HttpPost]
        public ActionResult Initial(CustomerAccountDetails account)
        {
            CustomerAccountDetails newEntry = new CustomerAccountDetails();
            var u = HttpContext.User.Identity.Name;
            var cust = db.Customers.Where(x => x.Email == u).FirstOrDefault();
            newEntry.CurrentlySuspended = false;
            newEntry.CustomerId = cust.ID;
            newEntry.MoneyOwed = 0;
            newEntry.WeeklyPickUpDay = account.WeeklyPickUpDay;
            db.CustomerAccountDetails.Add(newEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Email,Phone,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Email,Phone,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
