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
    public class LoaiNhanViens_60135761Controller : Controller
    {
        private QLNV4_60135761Entities db = new QLNV4_60135761Entities();
        string LayMaLNV()
        {
            var maMax = db.LoaiNhanViens.ToList().Select(n => n.MaLoaiNV).Max();
            int maLNV = int.Parse(maMax.Substring(3, 2)) + 1;
            string LNV = String.Concat("0", maLNV.ToString());
            return "LNV" + LNV.Substring(maLNV.ToString().Length - 1);
        }

        // GET: LoaiNhanViens_60135761
        public ActionResult Index()
        {
            return View(db.LoaiNhanViens.ToList());
        }

        // GET: LoaiNhanViens_60135761/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiNhanVien loaiNhanVien = db.LoaiNhanViens.Find(id);
            if (loaiNhanVien == null)
            {
                return HttpNotFound();
            }
            return View(loaiNhanVien);
        }

        // GET: LoaiNhanViens_60135761/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiNV = LayMaLNV();
            return View();
        }

        // POST: LoaiNhanViens_60135761/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiNV,TenLoaiNhanVien")] LoaiNhanVien loaiNhanVien)
        {
            if (ModelState.IsValid)
            {
                loaiNhanVien.MaLoaiNV = LayMaLNV();
                db.LoaiNhanViens.Add(loaiNhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiNhanVien);
        }

        // GET: LoaiNhanViens_60135761/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiNhanVien loaiNhanVien = db.LoaiNhanViens.Find(id);
            if (loaiNhanVien == null)
            {
                return HttpNotFound();
            }
            return View(loaiNhanVien);
        }

        // POST: LoaiNhanViens_60135761/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiNV,TenLoaiNhanVien")] LoaiNhanVien loaiNhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiNhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiNhanVien);
        }

        // GET: LoaiNhanViens_60135761/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiNhanVien loaiNhanVien = db.LoaiNhanViens.Find(id);
            if (loaiNhanVien == null)
            {
                return HttpNotFound();
            }
            return View(loaiNhanVien);
        }

        // POST: LoaiNhanViens_60135761/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LoaiNhanVien loaiNhanVien = db.LoaiNhanViens.Find(id);
            db.LoaiNhanViens.Remove(loaiNhanVien);
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
