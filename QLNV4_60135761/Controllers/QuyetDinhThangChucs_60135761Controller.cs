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
    public class QuyetDinhThangChucs_60135761Controller : Controller
    {
        private QLNV4_60135761Entities db = new QLNV4_60135761Entities();
        string LaySoQuyetDinh()
        {
            var maMax = db.QuyetDinhThangChucs.ToList().Select(n => n.SoQD).Max();
            if (maMax == null)
            {
                return "QD01";
            }
            int soQD = int.Parse(maMax.Substring(2)) + 1;
            string QuyetDinh = String.Concat("0", soQD.ToString());
            return "QD" + QuyetDinh.Substring(soQD.ToString().Length - 1);
        }

        // GET: QuyetDinhThangChucs_60135761
        public ActionResult Index()
        {
            var quyetDinhThangChucs = db.QuyetDinhThangChucs.OrderByDescending(x=>x.NgayTao).Include(q => q.NhanVien);
            return View(quyetDinhThangChucs.ToList());
        }

        // GET: QuyetDinhThangChucs_60135761/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyetDinhThangChuc quyetDinhThangChuc = db.QuyetDinhThangChucs.Find(id);
            if (quyetDinhThangChuc == null)
            {
                return HttpNotFound();
            }
            return View(quyetDinhThangChuc);
        }

        // GET: QuyetDinhThangChucs_60135761/Create
        public ActionResult Create()
        {
            ViewBag.SoQD = LaySoQuyetDinh();
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV");
            return View();
        }

        // POST: QuyetDinhThangChucs_60135761/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoQD,NoiDungQuyetDinh,NgayTao,MaNV")] QuyetDinhThangChuc quyetDinhThangChuc)
        {
            if (ModelState.IsValid)
            {
                quyetDinhThangChuc.SoQD = LaySoQuyetDinh();
                quyetDinhThangChuc.NgayTao = DateTime.Now;
                db.QuyetDinhThangChucs.Add(quyetDinhThangChuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", quyetDinhThangChuc.MaNV);
            return View(quyetDinhThangChuc);
        }

        // GET: QuyetDinhThangChucs_60135761/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyetDinhThangChuc quyetDinhThangChuc = db.QuyetDinhThangChucs.Find(id);
            if (quyetDinhThangChuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", quyetDinhThangChuc.MaNV);
            return View(quyetDinhThangChuc);
        }

        // POST: QuyetDinhThangChucs_60135761/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoQD,NoiDungQuyetDinh,NgayTao,MaNV")] QuyetDinhThangChuc quyetDinhThangChuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quyetDinhThangChuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", quyetDinhThangChuc.MaNV);
            return View(quyetDinhThangChuc);
        }

        // GET: QuyetDinhThangChucs_60135761/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyetDinhThangChuc quyetDinhThangChuc = db.QuyetDinhThangChucs.Find(id);
            if (quyetDinhThangChuc == null)
            {
                return HttpNotFound();
            }
            return View(quyetDinhThangChuc);
        }

        // POST: QuyetDinhThangChucs_60135761/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            QuyetDinhThangChuc quyetDinhThangChuc = db.QuyetDinhThangChucs.Find(id);
            db.QuyetDinhThangChucs.Remove(quyetDinhThangChuc);
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
