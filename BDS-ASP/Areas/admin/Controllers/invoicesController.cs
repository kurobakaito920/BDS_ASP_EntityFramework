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
    public class invoicesController : Controller
    {
        private BDSEntities db = new BDSEntities();

        // GET: admin/invoices
        public ActionResult Index()
        {
            var invoices = db.invoices.Include(i => i.customer).Include(i => i.transaction);
            return View(invoices.ToList());
        }

        // GET: admin/invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            invoice invoice = db.invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: admin/invoices/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "customer_name");
            ViewBag.transaction_id = new SelectList(db.transactions, "transaction_id", "transaction_date");
            return View();
        }

        // POST: admin/invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "invoice_id,export_invoice,total_prices,transaction_id,customer_id")] invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "customer_name", invoice.customer_id);
            ViewBag.transaction_id = new SelectList(db.transactions, "transaction_id", "transaction_date", invoice.transaction_id);
            return View(invoice);
        }

        // GET: admin/invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            invoice invoice = db.invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "customer_name", invoice.customer_id);
            ViewBag.transaction_id = new SelectList(db.transactions, "transaction_id", "transaction_date", invoice.transaction_id);
            return View(invoice);
        }

        // POST: admin/invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "invoice_id,export_invoice,total_prices,transaction_id,customer_id")] invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "customer_name", invoice.customer_id);
            ViewBag.transaction_id = new SelectList(db.transactions, "transaction_id", "transaction_date", invoice.transaction_id);
            return View(invoice);
        }

        // GET: admin/invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            invoice invoice = db.invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: admin/invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            invoice invoice = db.invoices.Find(id);
            db.invoices.Remove(invoice);
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
