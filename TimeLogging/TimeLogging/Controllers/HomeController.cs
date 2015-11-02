using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using TimeLogging.Models;
using TimeLogging.DataAccess;

namespace TimeLogging.Controllers
{
    public class HomeController : Controller
    {
        private IIimeLogService _tls;

        public HomeController(IIimeLogService tls)
        {
            _tls = tls;
        }

        //public ActionResult Index()
        //{
        //    List<TimeLogViewModel> entries = _tls.GetFiveLatestEntries();

        //    return View(entries);
        //}

        //// POST: /SubmitTimeLog
        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult SubmitTimeLog(TimeLogViewModel log)
        //{
        //    _tls.SubmitTimeLog(log);

        //    return RedirectToAction("Index", "Home");
        //}

        public ActionResult Index()
        {
            return View(_tls.GetEntriesByDate());
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = _tls.FindLog(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,StartTime,EndTime,Comment,Billable")] Log log)
        {
            if (ModelState.IsValid)
            {
                _tls.AddLog(log);
                return RedirectToAction("Index");
            }

            return View(log);
        }

        //// GET: LogsMvc/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Log log = db.Logs.Find(id);
        //    if (log == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(log);
        //}

        //// POST: LogsMvc/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,UserId,StartTime,EndTime,Comment,Billable")] Log log)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(log).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(log);
        //}

        //// GET: LogsMvc/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Log log = db.Logs.Find(id);
        //    if (log == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(log);
        //}

        //// POST: LogsMvc/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Log log = db.Logs.Find(id);
        //    db.Logs.Remove(log);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}