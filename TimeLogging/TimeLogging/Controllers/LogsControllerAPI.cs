using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using TimeLogging.Models;

namespace TimeLogging.Controllers
{
    public class LogsController : ApiController
    {
        private TimeLoggingContext db = new TimeLoggingContext();

        // GET: api/Logs
        public IQueryable<Log> GetLogs()
        {
            return db.Logs;
        }

        // GET: api/Logs/5
        [ResponseType(typeof(Log))]
        public IHttpActionResult GetLog(int id)
        {
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return NotFound();
            }

            return Ok(log);
        }

        // PUT: api/Logs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLog(int id, Log log)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != log.Id)
            {
                return BadRequest();
            }

            db.Entry(log).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogExists(id))
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

        // POST: api/Logs
        [ResponseType(typeof(Log))]
        public IHttpActionResult PostLog(Log log)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Logs.Add(log);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LogExists(log.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = log.Id }, log);
        }

        // DELETE: api/Logs/5
        [ResponseType(typeof(Log))]
        public IHttpActionResult DeleteLog(int id)
        {
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return NotFound();
            }

            db.Logs.Remove(log);
            db.SaveChanges();

            return Ok(log);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LogExists(int id)
        {
            return db.Logs.Count(e => e.Id == id) > 0;
        }
    }
}