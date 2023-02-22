using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLNV4_60135761.Models;


namespace QLNV4_60135761.Controllers
{
    public class BoPhans_60135761Controller : Controller
    {
        private QLNV4_60135761Entities db = new QLNV4_60135761Entities();
        string LayMaBoPhan()
        {
            var maMax = db.BoPhans.ToList().Select(n => n.MaBP).Max();
            int maBP = int.Parse(maMax.Substring(2)) + 1;
            string BoPhan = String.Concat("0", maBP.ToString());
            return "BP" + BoPhan.Substring(maBP.ToString().Length - 1);
        }
        // GET: BoPhans_60135761
        public ActionResult Index()
        {
            return View(db.BoPhans.ToList());
        }

        // GET: BoPhans_60135761/Details/5
        public string LayTenBoPhan(string mavt)
        {
            ViTriCongViec vt = db.ViTriCongViecs.Find(mavt);

            BoPhan bp = new BoPhan();
            bp = db.BoPhans.Find(vt.MaBP);           
            return bp.TenBoPhan;
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoPhan boPhan = db.BoPhans.Find(id);
            if (boPhan == null)
            {
                return HttpNotFound();
            }
            return View(boPhan);
        }

        // GET: BoPhans_60135761/Create
        public ActionResult Create()
        {
            ViewBag.MaBP = LayMaBoPhan();   
            return View();
        }

        // POST: BoPhans_60135761/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBP,TenBoPhan")] BoPhan boPhan)
        {
            if (ModelState.IsValid)
            {
                boPhan.MaBP = LayMaBoPhan();
                db.BoPhans.Add(boPhan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boPhan);
        }

        // GET: BoPhans_60135761/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoPhan boPhan = db.BoPhans.Find(id);
            if (boPhan == null)
            {
                return HttpNotFound();
            }
            return View(boPhan);
        }

        // POST: BoPhans_60135761/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaBP,TenBoPhan")] BoPhan boPhan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boPhan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boPhan);
        }

        // GET: BoPhans_60135761/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoPhan boPhan = db.BoPhans.Find(id);
            if (boPhan == null)
            {
                return HttpNotFound();
            }
            return View(boPhan);
        }

        // POST: BoPhans_60135761/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BoPhan boPhan = db.BoPhans.Find(id);
            db.BoPhans.Remove(boPhan);
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
