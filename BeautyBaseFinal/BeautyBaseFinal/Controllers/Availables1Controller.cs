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
    public class Availables1Controller : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Availables1
        public IQueryable<Available> GetAvailables()
        {
            return db.Availables;
        }

        // GET: api/Availables1/5
        [ResponseType(typeof(Available))]
        public IHttpActionResult GetAvailable(int id)
        {
            Available available = db.Availables.Find(id);
            if (available == null)
            {
                return NotFound();
            }

            return Ok(available);
        }

        // PUT: api/Availables1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAvailable(int id, Available available)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != available.Id)
            {
                return BadRequest();
            }

            db.Entry(available).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvailableExists(id))
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

        // POST: api/Availables1
        [ResponseType(typeof(Available))]
        public IHttpActionResult PostAvailable(Available available)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Availables.Add(available);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = available.Id }, available);
        }

        

        // DELETE: api/Availables1/5
        [ResponseType(typeof(Available))]
        public IHttpActionResult DeleteAvailable(int id)
        {
            Available available = db.Availables.Find(id);
            if (available == null)
            {
                return NotFound();
            }

            db.Availables.Remove(available);
            db.SaveChanges();

            return Ok(available);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AvailableExists(int id)
        {
            return db.Availables.Count(e => e.Id == id) > 0;
        }
    }
}