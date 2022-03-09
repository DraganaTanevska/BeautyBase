using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeautyBaseFinal.Models;

namespace BeautyBaseFinal.Controllers
{
    [Authorize]
    public class UnavailablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles ="Admin")]
        // GET: Unavailables
        public ActionResult Index()
        {
            return View(db.Unavailables.ToList());
        }

        // GET: Unavailables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unavailable unavailable = db.Unavailables.Find(id);
            if (unavailable == null)
            {
                return HttpNotFound();
            }
            return View(unavailable);
        }

        // GET: Unavailables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Unavailables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,selectedTime,UserName,ActionTime,Description")] Unavailable unavailable)
        {
            if (ModelState.IsValid)
            {
                db.Unavailables.Add(unavailable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(unavailable);
        }

        // GET: Unavailables/Edit/5
        [Authorize(Roles ="Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unavailable unavailable = db.Unavailables.Find(id);
            if (unavailable == null)
            {
                return HttpNotFound();
            }
            return View(unavailable);
        }

        // POST: Unavailables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description")] Unavailable unavailable)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(unavailable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(unavailable);
        }

        // GET: Unavailables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unavailable unavailable = db.Unavailables.Find(id);
            if (unavailable == null)
            {
                return HttpNotFound();
            }
            return View(unavailable);
        }
        // POST: Unavailables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Unavailable unavailable = db.Unavailables.Find(id);
            db.Unavailables.Remove(unavailable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult UserAppointments()
        {

            return View(db.Unavailables.ToList());
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
