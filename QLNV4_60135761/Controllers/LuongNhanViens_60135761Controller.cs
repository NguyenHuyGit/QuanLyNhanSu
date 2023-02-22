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
    public class LuongNhanViens_60135761Controller : Controller
    {
        private QLNV4_60135761Entities db = new QLNV4_60135761Entities();
        string LayMaLuong()
        {
            var maMax = db.LuongNhanViens.ToList().Select(n => n.MaLuong).Max();
            int maLuong = int.Parse(maMax.Substring(2)) + 1;
            string Luong = String.Concat("0", maLuong.ToString());
            return "ML" + Luong.Substring(maLuong.ToString().Length - 1);
        }

        // GET: LuongNhanViens_60135761
        public ActionResult Index(string search, int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            ViewBag.SoML = db.LuongNhanViens.OrderByDescending(x => x.NgayCapNhat).Where(x => x.MaLuong.Contains(search) || search == null).ToList().Count();
            return View(db.LuongNhanViens.OrderByDescending(x => x.NgayCapNhat).Where(x => x.MaLuong.Contains(search) || search == null).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: LuongNhanViens_60135761/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LuongNhanVien luongNhanVien = db.LuongNhanViens.Find(id);
            if (luongNhanVien == null)
            {
                return HttpNotFound();
            }
            return View(luongNhanVien);
        }

        // GET: LuongNhanViens_60135761/Create
        public ActionResult Create()
        {
            ViewBag.MaLuong = LayMaLuong();
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV");
            return View();
        }

        // POST: LuongNhanViens_60135761/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLuong,LuongHienTai,NgayCapNhat,DonViTien,MaNV")] LuongNhanVien luongNhanVien)
        {
            if (ModelState.IsValid)
            {
                luongNhanVien.MaLuong = LayMaLuong();
                luongNhanVien.NgayCapNhat = DateTime.Now;
                db.LuongNhanViens.Add(luongNhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", luongNhanVien.MaNV);
            return View(luongNhanVien);
        }

        // GET: LuongNhanViens_60135761/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LuongNhanVien luongNhanVien = db.LuongNhanViens.Find(id);
            if (luongNhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", luongNhanVien.MaNV);
            return View(luongNhanVien);
        }

        // POST: LuongNhanViens_60135761/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLuong,LuongHienTai,NgayCapNhat,DonViTien,MaNV")] LuongNhanVien luongNhanVien)
        {
            if (ModelState.IsValid)
            {
                luongNhanVien.NgayCapNhat = DateTime.Now;
                db.Entry(luongNhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", luongNhanVien.MaNV);
            return View(luongNhanVien);
        }

        // GET: LuongNhanViens_60135761/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LuongNhanVien luongNhanVien = db.LuongNhanViens.Find(id);
            if (luongNhanVien == null)
            {
                return HttpNotFound();
            }
            return View(luongNhanVien);
        }

        // POST: LuongNhanViens_60135761/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LuongNhanVien luongNhanVien = db.LuongNhanViens.Find(id);
            db.LuongNhanViens.Remove(luongNhanVien);
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
