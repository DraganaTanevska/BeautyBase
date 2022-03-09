using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BeautyBaseFinal.Models;

namespace BeautyBaseFinal.Controllers
{
    public class Unavailables1Controller : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Unavailables1
        public IQueryable<Unavailable> GetUnavailables()
        {
            return db.Unavailables;
        }

        // GET: api/Unavailables1/5
        [ResponseType(typeof(Unavailable))]
        public IHttpActionResult GetUnavailable(int id)
        {
            Unavailable unavailable = db.Unavailables.Find(id);
            if (unavailable == null)
            {
                return NotFound();
            }

            return Ok(unavailable);
        }

        // PUT: api/Unavailables1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUnavailable(int id, Unavailable unavailable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != unavailable.Id)
            {
                return BadRequest();
            }

            db.Entry(unavailable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnavailableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Unavailables1
        [ResponseType(typeof(Unavailable))]
        public IHttpActionResult PostUnavailable(Unavailable unavailable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Unavailables.Add(unavailable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = unavailable.Id }, unavailable);
        }

        // DELETE: api/Unavailables1/5
        [ResponseType(typeof(Unavailable))]
        public IHttpActionResult DeleteUnavailable(int id)
        {
            Unavailable unavailable = db.Unavailables.Find(id);
            if (unavailable == null)
            {
                return NotFound();
            }

            Available available = new Available();
            available.Date = unavailable.Date;
            available.selectedTime = unavailable.selectedTime;
            db.Availables.Add(available);
            db.Unavailables.Remove(unavailable);
            db.SaveChanges();
            return Ok(unavailable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UnavailableExists(int id)
        {
            return db.Unavailables.Count(e => e.Id == id) > 0;
        }
    }
}