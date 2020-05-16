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
using WebApi.Models;

namespace WebApi.Controllers
{
    public class InterestsController : ApiController
    {
        private DBmodel db = new DBmodel();

        // GET: api/Interests
        public IQueryable<Interest> GetInterests()
        {
            return db.Interests;
        }

        // GET: api/Interests/5
        [ResponseType(typeof(Interest))]
        public IHttpActionResult GetInterest(int id)
        {
            Interest interest = db.Interests.Find(id);
            if (interest == null)
            {
                return NotFound();
            }

            return Ok(interest);
        }

        // PUT: api/Interests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInterest(int id, Interest interest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != interest.id)
            {
                return BadRequest();
            }

            db.Entry(interest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterestExists(id))
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

        // POST: api/Interests
        [ResponseType(typeof(Interest))]
        public IHttpActionResult PostInterest(Interest interest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Interests.Add(interest);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (InterestExists(interest.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = interest.id }, interest);
        }

        // DELETE: api/Interests/5
        [ResponseType(typeof(Interest))]
        public IHttpActionResult DeleteInterest(int id)
        {
            Interest interest = db.Interests.Find(id);
            if (interest == null)
            {
                return NotFound();
            }

            db.Interests.Remove(interest);
            db.SaveChanges();

            return Ok(interest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InterestExists(int id)
        {
            return db.Interests.Count(e => e.id == id) > 0;
        }
    }
}