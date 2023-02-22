using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLNV4_60135761.Models;

namespace QLNV4_60135761.Controllers
{
    public class TongQuanController : Controller
    {
        private QLNV4_60135761Entities db = new QLNV4_60135761Entities();
        // GET: TongQuan
        public List<NhanVien> SinhNhat()
        {
            List<NhanVien> listsinhnhat = db.NhanViens.OrderBy(x => x.NgaySinh).Where(x => x.NgaySinh.Month == DateTime.Now.Month && x.TrangThai=="Đang làm việc").ToList();
            return listsinhnhat;
        }

        // list hợp đồng hết hạn trong tháng
        public List<HopDong> HopDongHetHan()
        {

            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            List<HopDong> listhopdong = db.HopDongs.OrderBy(x => x.NgayKetThuc).Where(x => x.NgayKetThuc.Month == month && x.NgayKetThuc.Year == year && x.TinhTrang == "Còn hạn").ToList();
            return listhopdong;
        }

        // trả về họ tên nhân viên
        public string TenNhanVien(string manv)
        {
            NhanVien nv = db.NhanViens.Find(manv);
            return nv.HoTenNV;
        }
        public ActionResult Index()
        {
            ViewBag.listsinhnhat = SinhNhat();
            ViewBag.listhopdonghethan = HopDongHetHan();
            ViewBag.SoNV = db.NhanViens.Where(x => x.TrangThai == "Đang làm việc").Count();
            ViewBag.SoVT = db.ViTriCongViecs.Count();
            ViewBag.SoBP = db.BoPhans.Count();
            ViewBag.SoHD = db.HopDongs.Where(x => x.TinhTrang == "Còn hạn").Count();
            return View();
        }
    }
}