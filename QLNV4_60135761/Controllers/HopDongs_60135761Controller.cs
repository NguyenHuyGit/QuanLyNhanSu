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
    public class HopDongs_60135761Controller : Controller
    {
        private QLNV4_60135761Entities db = new QLNV4_60135761Entities();

        // GET: HopDongs_60135761
        public ActionResult Index(int? page, string search)
        {
            if (page == null)
                page = 1;
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            ViewBag.SoHD = db.HopDongs.OrderByDescending(x => x.NgayKy).Where(x => x.SoHD.Contains(search) || search == null).ToList().Count();
            return View(db.HopDongs.OrderByDescending(x => x.NgayKy).Where(x => x.SoHD.Contains(search) || search == null).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: HopDongs_60135761/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HopDong hopDong = db.HopDongs.Find(id);
            if (hopDong == null)
            {
                return HttpNotFound();
            }
            return View(hopDong);
        }

        // GET: HopDongs_60135761/Create
        public ActionResult Create()
        {
            ViewBag.MaLuong = new SelectList(db.LuongNhanViens, "MaLuong", "MaLuong");
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV");
            return View();
        }

        // POST: HopDongs_60135761/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoHD,NgayKy,NgayBatDau,NgayKetThuc,NguoiDaiDien,TinhTrang,GhiChu,MaNV,MaLuong")] HopDong hopDong)
        {
            if (hopDong.NgayKetThuc <= hopDong.NgayBatDau || hopDong.NgayKetThuc <= hopDong.NgayKy)
            {
                ViewBag.error = "Ngày kết thúc phải lớn hơn ngày bắt đầu (hoặc ngày ký)!";
                ViewBag.MaLuong = new SelectList(db.LuongNhanViens, "MaLuong", "MaLuong", hopDong.MaLuong);
                ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", hopDong.MaNV);
                return View("Create");
            }

            var sohd = Request.Form.GetValues("SoHD");
            var check = db.HopDongs.Find(sohd);
            if (check != null)
            {
                ViewBag.sohderror = "Đã tồn tại số hợp đồng này!";
                ViewBag.MaLuong = new SelectList(db.LuongNhanViens, "MaLuong", "MaLuong", hopDong.MaLuong);
                ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", hopDong.MaNV);
                return View("Create");
            }

            if (ModelState.IsValid)
            {
                db.HopDongs.Add(hopDong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLuong = new SelectList(db.LuongNhanViens, "MaLuong", "MaLuong", hopDong.MaLuong);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", hopDong.MaNV);
            return View(hopDong);
        }

        // GET: HopDongs_60135761/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HopDong hopDong = db.HopDongs.Find(id);
            if (hopDong == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLuong = new SelectList(db.LuongNhanViens, "MaLuong", "MaLuong", hopDong.MaLuong);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", hopDong.MaNV);
            return View(hopDong);
        }

        // POST: HopDongs_60135761/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoHD,NgayKy,NgayBatDau,NgayKetThuc,NguoiDaiDien,TinhTrang,GhiChu,MaNV,MaLuong")] HopDong hopDong)
        {
            if (hopDong.NgayKetThuc <= hopDong.NgayBatDau)
            {
                ViewBag.error = "Ngày kết thúc phải lớn hơn ngày bắt đầu !";
                ViewBag.MaLuong = new SelectList(db.LuongNhanViens, "MaLuong", "MaLuong", hopDong.MaLuong);
                ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", hopDong.MaNV);
                return View("Edit");
            }

            if (ModelState.IsValid)
            {
                db.Entry(hopDong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLuong = new SelectList(db.LuongNhanViens, "MaLuong", "MaLuong", hopDong.MaLuong);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", hopDong.MaNV);
            return View(hopDong);
        }

        // GET: HopDongs_60135761/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HopDong hopDong = db.HopDongs.Find(id);
            if (hopDong == null)
            {
                return HttpNotFound();
            }
            return View(hopDong);
        }

        // POST: HopDongs_60135761/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HopDong hopDong = db.HopDongs.Find(id);
            db.HopDongs.Remove(hopDong);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult TimKiemHopDong(string sohd = "", string nguoidaidien = "", string tinhtrang = "", string manv = "")
        {
            ViewBag.sohd = sohd;
            ViewBag.nguoidaidien = nguoidaidien;
            ViewBag.tinhtrang = tinhtrang;
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV");
            var hopDongs = db.HopDongs.SqlQuery("HopDong_TimKiem '" + sohd + "',N'" + nguoidaidien + "',N'" + tinhtrang + "','" + manv + "'");
            if (hopDongs.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";

            ViewBag.countHD = hopDongs.Count();
            return View(hopDongs.ToList());
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
