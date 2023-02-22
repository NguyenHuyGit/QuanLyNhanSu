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
    public class DeXuatTangLuongs_60135761Controller : Controller
    {
        private QLNV4_60135761Entities db = new QLNV4_60135761Entities();
        string LayMaDeXuat()
        {
            var maMax = db.DeXuatTangLuongs.ToList().Select(n => n.MaDeXuat).Max();
            if (maMax == null)
            {
                return "DX01";
            }
            int maDX = int.Parse(maMax.Substring(2)) + 1;
            string DeXuat = String.Concat("0", maDX.ToString());
            return "DX" + DeXuat.Substring(maDX.ToString().Length - 1);
        }
        // GET: DeXuatTangLuongs_60135761
        public ActionResult Index()
        {
            var deXuatTangLuongs = db.DeXuatTangLuongs.Include(d => d.NhanVien);
            return View(deXuatTangLuongs.ToList());
        }

        // GET: DeXuatTangLuongs_60135761/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeXuatTangLuong deXuatTangLuong = db.DeXuatTangLuongs.Find(id);
            if (deXuatTangLuong == null)
            {
                return HttpNotFound();
            }
            return View(deXuatTangLuong);
        }

        // GET: DeXuatTangLuongs_60135761/Create
        public ActionResult Create()
        {
            ViewBag.MaDeXuat = LayMaDeXuat();
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV");
            return View();
        }

        // POST: DeXuatTangLuongs_60135761/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDeXuat,NgayDeXuat,LiDoDeXuat,LuongMongMuon,TrangThai,MaNV")] DeXuatTangLuong deXuatTangLuong)
        {
            if (ModelState.IsValid)
            {
                deXuatTangLuong.MaDeXuat = LayMaDeXuat();
                deXuatTangLuong.NgayDeXuat = DateTime.Now;
                db.DeXuatTangLuongs.Add(deXuatTangLuong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", deXuatTangLuong.MaNV);
            return View(deXuatTangLuong);
        }

        // GET: DeXuatTangLuongs_60135761/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeXuatTangLuong deXuatTangLuong = db.DeXuatTangLuongs.Find(id);
            if (deXuatTangLuong == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", deXuatTangLuong.MaNV);
            return View(deXuatTangLuong);
        }

        // POST: DeXuatTangLuongs_60135761/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDeXuat,NgayDeXuat,LiDoDeXuat,LuongMongMuon,TrangThai,MaNV")] DeXuatTangLuong deXuatTangLuong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deXuatTangLuong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", deXuatTangLuong.MaNV);
            return View(deXuatTangLuong);
        }

        // GET: DeXuatTangLuongs_60135761/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeXuatTangLuong deXuatTangLuong = db.DeXuatTangLuongs.Find(id);
            if (deXuatTangLuong == null)
            {
                return HttpNotFound();
            }
            return View(deXuatTangLuong);
        }

        // POST: DeXuatTangLuongs_60135761/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DeXuatTangLuong deXuatTangLuong = db.DeXuatTangLuongs.Find(id);
            db.DeXuatTangLuongs.Remove(deXuatTangLuong);
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
