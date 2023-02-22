using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLNV4_60135761.Models;
using PagedList.Mvc;
using PagedList;

namespace QLNV4_60135761.Controllers
{
    public class ViTriCongViecs_60135761Controller : Controller
    {
        private QLNV4_60135761Entities db = new QLNV4_60135761Entities();
        string LayMaViTri()
        {
            var maMax = db.ViTriCongViecs.ToList().Select(n => n.MaVT).Max();
            int maVT = int.Parse(maMax.Substring(2)) + 1;
            string ViTri = String.Concat("0", maVT.ToString());
            return "VT" + ViTri.Substring(maVT.ToString().Length - 1);
        }
        // GET: ViTriCongViecs_60135761
        public ActionResult Index(string search, int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            ViewBag.SoVT = db.ViTriCongViecs.Where(x => x.TenViTriCongViec.Contains(search) || search == null).ToList().Count();

            return View(db.ViTriCongViecs.Where(x => x.TenViTriCongViec.Contains(search) || search == null).ToList().ToPagedList(pageNumber, pageSize));
        }

        public string TenViTri(string mavt)
        {
            ViTriCongViec vt = db.ViTriCongViecs.Find(mavt);
            return vt.TenViTriCongViec;
        }
        // GET: ViTriCongViecs_60135761/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViTriCongViec viTriCongViec = db.ViTriCongViecs.Find(id);
            if (viTriCongViec == null)
            {
                return HttpNotFound();
            }
            return View(viTriCongViec);
        }

        // GET: ViTriCongViecs_60135761/Create
        public ActionResult Create()
        {
            ViewBag.MaVT = LayMaViTri();
            ViewBag.MaBP = new SelectList(db.BoPhans, "MaBP", "TenBoPhan");
            return View();
        }

        // POST: ViTriCongViecs_60135761/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaVT,TenViTriCongViec,MaBP")] ViTriCongViec viTriCongViec)
        {
            if (ModelState.IsValid)
            {
                viTriCongViec.MaVT = LayMaViTri();
                db.ViTriCongViecs.Add(viTriCongViec);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaBP = new SelectList(db.BoPhans, "MaBP", "TenBoPhan", viTriCongViec.MaBP);
            return View(viTriCongViec);
        }

        // GET: ViTriCongViecs_60135761/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViTriCongViec viTriCongViec = db.ViTriCongViecs.Find(id);
            if (viTriCongViec == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBP = new SelectList(db.BoPhans, "MaBP", "TenBoPhan", viTriCongViec.MaBP);
            return View(viTriCongViec);
        }

        // POST: ViTriCongViecs_60135761/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaVT,TenViTriCongViec,MaBP")] ViTriCongViec viTriCongViec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viTriCongViec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaBP = new SelectList(db.BoPhans, "MaBP", "TenBoPhan", viTriCongViec.MaBP);
            return View(viTriCongViec);
        }

        // GET: ViTriCongViecs_60135761/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViTriCongViec viTriCongViec = db.ViTriCongViecs.Find(id);
            if (viTriCongViec == null)
            {
                return HttpNotFound();
            }
            return View(viTriCongViec);
        }

        // POST: ViTriCongViecs_60135761/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ViTriCongViec viTriCongViec = db.ViTriCongViecs.Find(id);
            db.ViTriCongViecs.Remove(viTriCongViec);
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
