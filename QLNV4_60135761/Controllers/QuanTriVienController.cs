using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLNV4_60135761.Models;

namespace QLNV4_60135761.Controllers
{
    public class QuanTriVienController : Controller
    {
        private QLNV4_60135761Entities db = new QLNV4_60135761Entities();
        // GET: QuanTriVien
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var tkquanly = db.QuanTriViens.Where(s => s.Username.Equals(username) && s.Password.Equals(password)).ToList();

                if (tkquanly.Count() > 0)
                {
                    Session["DangNhap"] = username;
                    Session["Admin"] = 1;
                    return RedirectToAction("../TaiKhoans_60135761/Index");
                }
                else
                {
                    ModelState.AddModelError("Error", "Tài khoản không hợp lệ !");
                }

            }
            return View();
        }

    }
}