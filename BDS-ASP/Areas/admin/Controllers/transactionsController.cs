using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BDS_ASP.Models;

namespace BDS_ASP.Areas.admin.Controllers
{
    public class transactionsController : Controller
    {
        private BDSEntities db = new BDSEntities();

        // GET: admin/transactions
        public ActionResult Index()
        {
            var transactions = db.transactions.Include(t => t.customer).Include(t => t.employee).Include(t => t.property);
            return View(transactions.ToList());
        }

        // GET: admin/transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = db.transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: admin/transactions/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "customer_name");
            ViewBag.employee_id = new SelectList(db.employees, "employee_id", "employee_name");
            ViewBag.property_id = new SelectList(db.properties, "property_id", "property_address");
            return View();
        }

        // POST: admin/transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "transaction_id,transaction_date,transaction_location,transaction_prices,employee_id,customer_id,property_id")] transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "customer_name", transaction.customer_id);
            ViewBag.employee_id = new SelectList(db.employees, "employee_id", "employee_name", transaction.employee_id);
            ViewBag.property_id = new SelectList(db.properties, "property_id", "property_address", transaction.property_id);
            return View(transaction);
        }

        // GET: admin/transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = db.transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "customer_name", transaction.customer_id);
            ViewBag.employee_id = new SelectList(db.employees, "employee_id", "employee_name", transaction.employee_id);
            ViewBag.property_id = new SelectList(db.properties, "property_id", "property_address", transaction.property_id);
            return View(transaction);
        }

        // POST: admin/transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "transaction_id,transaction_date,transaction_location,transaction_prices,employee_id,customer_id,property_id")] transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "customer_name", transaction.customer_id);
            ViewBag.employee_id = new SelectList(db.employees, "employee_id", "employee_name", transaction.employee_id);
            ViewBag.property_id = new SelectList(db.properties, "property_id", "property_address", transaction.property_id);
            return View(transaction);
        }

        // GET: admin/transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = db.transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: admin/transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            transaction transaction = db.transactions.Find(id);
            db.transactions.Remove(transaction);
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
