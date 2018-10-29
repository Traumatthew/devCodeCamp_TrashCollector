﻿using System;
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
            var customers = db.Customers.Where(x => x.Zip == employee.Zip).ToList();
            var custAcc = db.CustomerAccountDetails.Where(x => x.WeeklyPickUpDay == DateTime.Today.DayOfWeek.ToString()).ToList();
            List<Customer> customersL = db.Customers.ToList();
            PickUpsViewModel view = new PickUpsViewModel();
            List<Customer> results = new List<Customer>();
            foreach (var account in custAcc)
            {
                foreach (var cust in customers)
                {
                    if (cust.ID == account.CustomerId)
                    {
                        bool check = CheckSuspensions(cust.ID);
                        if(check == false)
                        {
                            results.Add(cust);
                        }
                    }
                }
            }
            var special = db.PickUpRequests.Where(x => x.Date == DateTime.Today).ToList();
            view.standardPickups = results;
            view.specialPickups = special;
            view.customers = customersL;
            return View(view);
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
            var customers = db.Customers.ToList();
            return View(customers);
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
