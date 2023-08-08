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
    public class propertiesController : Controller
    {
        private BDSEntities db = new BDSEntities();

        // GET: admin/properties
        public ActionResult Index()
        {
            var properties = db.properties.Include(p => p.subCategory);
            return View(properties.ToList());
        }

        // GET: admin/properties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            property property = db.properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: admin/properties/Create
        public ActionResult Create()
        {
            ViewBag.subCategory_id = new SelectList(db.subCategories, "subCategory_id", "subCategory_name");
            return View();
        }

        // POST: admin/properties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "property_id,property_address,area,priceToSell,rentCost,descriptions,subCategory_id")] property property)
        {
            if (ModelState.IsValid)
            {
                db.properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.subCategory_id = new SelectList(db.subCategories, "subCategory_id", "subCategory_name", property.subCategory_id);
            return View(property);
        }

        // GET: admin/properties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            property property = db.properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            ViewBag.subCategory_id = new SelectList(db.subCategories, "subCategory_id", "subCategory_name", property.subCategory_id);
            return View(property);
        }

        // POST: admin/properties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "property_id,property_address,area,priceToSell,rentCost,descriptions,subCategory_id")] property property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.subCategory_id = new SelectList(db.subCategories, "subCategory_id", "subCategory_name", property.subCategory_id);
            return View(property);
        }

        // GET: admin/properties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            property property = db.properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: admin/properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            property property = db.properties.Find(id);
            db.properties.Remove(property);
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
