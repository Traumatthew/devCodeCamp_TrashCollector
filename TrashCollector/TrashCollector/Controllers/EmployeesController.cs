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
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private bool CheckSuspensions(int CustId)
        {
            var suspensions = db.Suspensions.Where(x => x.CustomerId == CustId).ToList();
            bool suspended = false;
            foreach(var item in suspensions)
            {
                if(DateTime.Today < item.EndDate && DateTime.Today > item.StartDate)
                {
                    suspended = true;
                }
            }
            return suspended;
        }


        public ActionResult Index()
        {
            var employee = db.Employees.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            TempData["employee"] = employee;
            var customers = db.Customers.Where(x => x.Zip == employee.Zip).ToList();
            var custAcc = db.CustomerAccountDetails.Where(x => x.WeeklyPickUpDay == DateTime.Today.DayOfWeek.ToString()).ToList();
            List<Customer> customersL = db.Customers.ToList();
            PickUpsViewModel view = new PickUpsViewModel();
            List<Customer> results = new List<Customer>();
            List<WeeklyPickups> pickupResults = new List<WeeklyPickups>();
            foreach (var account in custAcc)
            {
                foreach (var cust in customers)
                {
                    if (cust.ID == account.CustomerId)
                    {
                        bool check = CheckSuspensions(cust.ID);
                        if(check == false)
                        {
                            CreateWeekly(cust.ID);
                            var today = DateTime.Today.ToShortDateString();
                            pickupResults.Add(db.WeeklyPickups.Where(x => x.CustomerId == cust.ID).Where(x => x.Date == today).FirstOrDefault());
                            results.Add(cust);
                        }
                    }
                }
            }
            var special = db.PickUpRequests.Where(x => x.Date == DateTime.Today).ToList();
            view.weeklypickups = pickupResults;
            view.standardPickups = results;
            view.specialPickups = special;
            view.customers = customersL;
            return View(view);
        }

        public void CreateWeekly(int id)
        {
            
            WeeklyPickups wp = new WeeklyPickups();
            wp.CustomerId = id;
            wp.Date = DateTime.Today.ToShortDateString();
            wp.Complete = false;
            if (db.WeeklyPickups.Where(x => x.CustomerId == id).Where(x => x.Date == wp.Date).FirstOrDefault() == null)
            {
                db.WeeklyPickups.Add(wp);
                db.SaveChanges();
            }
        }

     

        public ActionResult PickupDetail (int id)
        {
            var pickup = db.PickUpRequests.Where(x => x.PickUpId == id).FirstOrDefault();        
            return View(pickup);
        }

        [HttpPost]
        public ActionResult PickupDetail(int id, FormCollection form)
        {
            var pickup = db.PickUpRequests.Where(x => x.PickUpId == id).FirstOrDefault();
            var cust = db.CustomerAccountDetails.Where(x => x.CustomerId == pickup.CustomerId).FirstOrDefault();
            pickup.complete = true;
            cust.MoneyOwed += pickup.Fee;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CustomerAccounts()
        {
            Employee employee = (Employee)TempData["employee"];
            var accounts = db.CustomerAccountDetails.ToList();
            var customers = db.Customers.Where(x => x.Zip == employee.Zip).ToList();
            List<CustomerAccountDetails> filteredAccounts = new List<CustomerAccountDetails>();
            foreach(var cust in customers)
            {
                foreach(var acc in accounts)
                {
                    if(acc.CustomerId == cust.ID)
                    {
                        filteredAccounts.Add(acc);
                    }
                }
            }
            GeoLocations Geo = new GeoLocations();
            CustomerAndAccountViewModel view = new CustomerAndAccountViewModel() { accounts = filteredAccounts, customers = customers, geo = Geo };
            EmployeeCustomerAccountsViewModel finalView = new EmployeeCustomerAccountsViewModel() { CustViewModel = view, emp = employee };
            TempData["employee"] = employee;
            return View(finalView);
        }

        public ActionResult ConfirmWeeklyPickup(int id)
        {
            var customer = db.Customers.Where(x => x.ID == id).FirstOrDefault();
            var today = DateTime.Today.ToShortDateString();
            var pickup = db.WeeklyPickups.Where(x => x.CustomerId == id).Where(x => x.Date == today).FirstOrDefault();
            PickUpsViewModel pick = new PickUpsViewModel() { cust = customer, weeklypickup = pickup};
            return View(pick);
        }

        [HttpPost]
        public ActionResult ConfirmWeeklyPickup(int id, FormCollection form)
        {
            var today = DateTime.Today.ToShortDateString();
            var custAccount = db.CustomerAccountDetails.Where(x => x.CustomerId == id).FirstOrDefault();
            var pickup = db.WeeklyPickups.Where(x => x.CustomerId == id).Where(x => x.Date == today).FirstOrDefault();
            pickup.Complete = true;
            custAccount.MoneyOwed += 10;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult CustomerAccounts(string[] days, EmployeeCustomerAccountsViewModel finalView)
        {
            
            var accounts = db.CustomerAccountDetails.ToList();
            var customers = db.Customers.ToList();
            List<CustomerAccountDetails> accResults = new List<CustomerAccountDetails>();
            List<Customer> custResults = new List<Customer>();
            foreach(var day in days)
            {
                foreach(var acc in accounts)
                {
                    if(acc.WeeklyPickUpDay == day)
                    {
                        accResults.Add(acc);
                    }
                }
            }
            foreach(var cust in customers)
            {
                foreach(var acc in accResults)
                {
                    if(acc.CustomerId == cust.ID)
                    {
                        custResults.Add(cust);
                    }
                }
            }
            Employee employee = (Employee)TempData["employee"];
            CustomerAndAccountViewModel view = new CustomerAndAccountViewModel() { customers = custResults, accounts = accResults, };
            finalView.CustViewModel = view;
            finalView.emp = employee;
            TempData["employee"] = employee;
            return View(finalView);
        }
        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Email,Phone,Address")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Email,Phone,Address")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
