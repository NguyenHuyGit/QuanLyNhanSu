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
using System.Security.Cryptography;
using System.Text;

namespace QLNV4_60135761.Controllers
{
    public class TaiKhoans_60135761Controller : Controller
    {
        private QLNV4_60135761Entities db = new QLNV4_60135761Entities();

        // GET: TaiKhoans_60135761
        public ActionResult Index(string search, int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.SoTK = db.TaiKhoans.OrderByDescending(x => x.NgayTao).Where(x => x.Email.Contains(search) || search == null).ToList().Count();
            return View(db.TaiKhoans.OrderByDescending(x => x.NgayTao).Where(x => x.Email.Contains(search) || search == null).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: TaiKhoans_60135761/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // GET: TaiKhoans_60135761/Create
        public ActionResult Create()
        {
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV");
            return View();
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        // POST: TaiKhoans_60135761/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTK,Email,Password,NgayTao,Quyen,KichHoat,MaNV")] TaiKhoan taiKhoan, string password)
        {

            var kiemtra = db.TaiKhoans.Where(x => x.Email == taiKhoan.Email).ToList();
            if (kiemtra.Count() > 0)
            {
                ViewBag.error = "Email đã tồn tại. Vui lòng nhập email khác.";
                ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", taiKhoan.MaNV);
                return View("Create");
            }

            var f_password = GetMD5(password);

            if (ModelState.IsValid)
            {
                taiKhoan.Password = f_password;
                taiKhoan.NgayTao = DateTime.Now;
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", taiKhoan.MaNV);
            return View(taiKhoan);
        }

        // GET: TaiKhoans_60135761/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", taiKhoan.MaNV);
            return View(taiKhoan);
        }

        // POST: TaiKhoans_60135761/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTK,Email,Password,NgayTao,Quyen,KichHoat,MaNV")] TaiKhoan taiKhoan,string password)
        {

            var f_password = GetMD5(password);
            if (ModelState.IsValid)
            {
                taiKhoan.Password = f_password;
                taiKhoan.NgayTao = DateTime.Now;
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", taiKhoan.MaNV);
            return View(taiKhoan);
        }

        // GET: TaiKhoans_60135761/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: TaiKhoans_60135761/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            db.TaiKhoans.Remove(taiKhoan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(string email = "", string password = "", string quyen = "")
        {
            if (ModelState.IsValid)
            {
                string quanly = "Quản lý";
                string nhanvien = "Nhân viên";
                var f_password = GetMD5(password);

                if (quanly == quyen)
                {
                    quanly = quyen;
                    var tkquanly = db.TaiKhoans.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password) && s.Quyen.Equals(quanly)).ToList();
                    if (tkquanly.Count() > 0)
                    {
                        Session["DangNhap"] = quanly;
                        Session["QuanLy"] = 1;
                        if (Session["Admin"] != null)
                        {
                            Session.Remove("Admin");
                        }    
                        return RedirectToAction("../TongQuan/Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Tài khoản không hợp lệ !");
                    }

                }

                if (nhanvien == quyen)
                {
                    nhanvien = quyen;
                    var tknhanvien = db.TaiKhoans.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password) && s.Quyen.Equals(nhanvien)).ToList();
                    TaiKhoan nv = db.TaiKhoans.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password) && s.Quyen.Equals(nhanvien)).FirstOrDefault();
                    if (tknhanvien.Count() > 0)
                    {
                        Session["DangNhap"] = nhanvien;
                        Session["MaNhanVien"] = nv.MaNV;
                        return RedirectToAction("../NhanViens_60135761/Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Tài khoản không hợp lệ !");
                    }
                }

            }


            return View("DangNhap");
        }

        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("../TaiKhoans_60135761/DangNhap");
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
