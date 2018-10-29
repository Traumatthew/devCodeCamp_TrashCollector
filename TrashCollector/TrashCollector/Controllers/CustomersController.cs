using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
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
        public Customer GetCustomer()
        {
            var customer = db.Customers.Where(x => x.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            return customer;
        }

        public CustomerAccountDetails GetCustomerAccount()
        {
            var cust = GetCustomer();
            var customerAccount = db.CustomerAccountDetails.Where(x => x.CustomerId == cust.ID).FirstOrDefault();
            return customerAccount;
        }

        // GET: Customers
        public ActionResult Index()
        {
            var CAD = db.CustomerAccountDetails.ToList();
            var customer = GetCustomer();
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
                var susp = db.Suspensions.Where(x => x.CustomerId == customer.ID).ToList();
                var pickups = db.PickUpRequests.Where(x => x.CustomerId == customer.ID).ToList();
                var accountDet = db.CustomerAccountDetails.Where(x => x.CustomerId == customer.ID).FirstOrDefault();
                CustomerAndAccountViewModel viewmodel = new CustomerAndAccountViewModel() { cust = customer, account = accountDet, pickups = pickups, suspensions = susp };
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
            var cust = GetCustomer();
            newEntry.CurrentlySuspended = false;
            newEntry.CustomerId = cust.ID;
            newEntry.MoneyOwed = 0;
            newEntry.WeeklyPickUpDay = account.WeeklyPickUpDay;
            db.CustomerAccountDetails.Add(newEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateSpecialPickup()
        {
            PickUpRequests pickup = new PickUpRequests();
            ViewBag.TimeMessage = "";
            return View(pickup);
        }

        [HttpPost]
        public ActionResult CreateSpecialPickup(FormCollection form)
        {
           
            string date = form["Date"];
       
            
            DateTime dvalue;
          
            if(DateTime.TryParse(date, out(dvalue)) == true)
            {
                dvalue = DateTime.Parse(date);
            }
            else
            {
                ViewBag.faileddate = "Date format incorrect: " + form["Date"];
                return View();
            }

            TempData["pickup"] = form;
            return RedirectToAction("ConfirmPickUpRequest", new { Form = form});
        }


        public ActionResult ConfirmPickUpRequest()
        {
            FormCollection Form = (FormCollection)TempData["pickup"];
            int fee = 100;   
            PickUpRequests pickup = new PickUpRequests();
            string date = Form["Date"];
            DateTime dvalue = DateTime.Parse(date);
            var cust = GetCustomer();
            int pId = Int32.Parse(Form["PickUpId"]);
            pickup.PickUpId = pId;
            pickup.CustomerId = cust.ID;
            pickup.Place = Form["Place"];
            pickup.Date = dvalue;
            pickup.Time = Form["Times"];
            pickup.Fee = fee;
            pickup.notes = Form["Notes"];
            pickup.complete = false;
            TempData["pickup"] = pickup;
            return View(pickup);
        }

        [HttpPost]
        public ActionResult ConfirmPickUpRequest(PickUpRequests p)
        {
            PickUpRequests pickup = (PickUpRequests)TempData["pickup"];
            db.PickUpRequests.Add(pickup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeletePickup(int id)
        {
            var pickup = db.PickUpRequests.Where(x => x.PickUpId == id).FirstOrDefault();
            return View(pickup);
        }

        [HttpPost]
        public ActionResult DeletePickup(int id, FormCollection form)
        {
            var pickup = db.PickUpRequests.Where(x => x.PickUpId == id).FirstOrDefault();
            db.PickUpRequests.Remove(pickup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditProfile()
        {
            var customer = GetCustomer();
            var acc = GetCustomerAccount();
            CustomerAndAccountViewModel vm = new CustomerAndAccountViewModel() { cust = customer, account = acc };
            return View(vm);
        }

        [HttpPost]
        public ActionResult EditProfile(FormCollection form)
        {
            var cust = GetCustomer();
            var acc = GetCustomerAccount();
            cust.FirstName = form["cust.FirstName"];
            cust.LastName = form["cust.LastName"];
            cust.Phone = form["cust.Phone"];
            cust.State = form["cust.State"];
            cust.Street = form["cust.Street"];
            cust.Zip = form["cust.Zip"];
            acc.WeeklyPickUpDay = form["account.WeeklyPickUpDay"];
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
