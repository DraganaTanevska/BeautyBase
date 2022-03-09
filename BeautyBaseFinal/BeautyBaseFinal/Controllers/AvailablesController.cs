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
    public class AvailablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Availables
        public ActionResult Index()
        {
            List<Available> availables = db.Availables.ToList();
            foreach (Available available in availables)
            {
                string[] selectedDate = available.Date.ToString().Split(' ')[0].Split('.');
                int selectedDay = Int32.Parse(selectedDate[0]);
                int selectedMonth = Int32.Parse(selectedDate[1]);
                int selectedYear = Int32.Parse(selectedDate[2]);
                int selectedHour = Int32.Parse(available.selectedTime.Split(':')[0]);
                int selectedMin = Int32.Parse(available.selectedTime.Split(':')[1]);
                string[] realDate1 = DateTime.Now.ToString("g").Split(' ');
                string[] realDate = realDate1[0].Split('.');
                int realDay = Int32.Parse(realDate[0]);
                int realMonth = Int32.Parse(realDate[1]);
                int realYear = Int32.Parse(realDate[2]);
                int realHour = Int32.Parse(realDate1[1].Split(':')[0]);
                int realMin = Int32.Parse(realDate1[1].Split(':')[1]);
                //dozvoluva prikaz samo na idni termini
                if (selectedYear < realYear || selectedYear == realYear && (selectedMonth < realMonth || selectedMonth == realMonth && (selectedDay < realDay || selectedDay == realDay && selectedHour < realHour|| (selectedHour==realHour&&selectedMin<realMin))))
                {

                    db.Availables.Remove(available);
                    db.SaveChanges();
                }

            }
            return View(db.Availables.ToList());
        }
    

        // GET: Availables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Available available = db.Availables.Find(id);
            if (available == null)
            {
                return HttpNotFound();
            }
            return View(available);
        }

        // GET: Availables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Availables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,selectedTime,Description")] Available available)
        {
            if (ModelState.IsValid)
            {
                string[] selectedDate = available.Date.ToString().Split(' ')[0].Split('.');
                int selectedDay = Int32.Parse(selectedDate[0]);
                int selectedMonth = Int32.Parse(selectedDate[1]);
                int selectedYear = Int32.Parse(selectedDate[2]);
                int selectedHour = Int32.Parse(available.selectedTime.Split(':')[0]);
                int selectedMin = Int32.Parse(available.selectedTime.Split(':')[1]);
                string[] realDate1 = DateTime.Now.ToString("g").Split(' ');
                string[] realDate = realDate1[0].Split('.');
                int realDay = Int32.Parse(realDate[0]);
                int realMonth = Int32.Parse(realDate[1]);
                int realYear = Int32.Parse(realDate[2]);
                int realHour = Int32.Parse(realDate1[1].Split(':')[0]);
                int realMin = Int32.Parse(realDate1[1].Split(':')[1]);
                //dozvoluva vnes samo na idni termini
                if (selectedYear < realYear || selectedYear == realYear && (selectedMonth < realMonth || selectedMonth == realMonth && selectedDay < realDay || (selectedDay==realDay&&selectedHour<realHour || (selectedHour==realHour&&selectedMin<realMin))))
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    db.Availables.Add(available);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(available);
        }

        // GET: Availables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Available available = db.Availables.Find(id);
            if (available == null)
            {
                return HttpNotFound();
            }
            return View(available);
        }

        // POST: Availables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,selectedTime,Description")] Available available)
        {
            if (ModelState.IsValid)
            {
                db.Entry(available).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(available);
        }

        // GET: Availables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Available available = db.Availables.Find(id);
            if (available == null)
            {
                return HttpNotFound();
            }
            return View(available);
        }

        // POST: Availables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Available available = db.Availables.Find(id);
            db.Availables.Remove(available);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Reserve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Available available = db.Availables.Find(id);
            if (available == null)
            {
                return HttpNotFound();
            }
            return View(available);
        }

        // POST: Availables/Delete/5
        [HttpPost, ActionName("Reserve")]
        [ValidateAntiForgeryToken]
        public ActionResult ReserveConfirmed(int id)
        {
            Available available = db.Availables.Find(id);
            Unavailable unavailable = new Unavailable();
            unavailable.Date = available.Date;
            unavailable.selectedTime = available.selectedTime;
            unavailable.UserName = User.Identity.Name;
            unavailable.Description = available.Description;
            unavailable.ActionTime = DateTime.Now.ToString("g");
            db.Unavailables.Add(unavailable);
            db.Availables.Remove(available);
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
