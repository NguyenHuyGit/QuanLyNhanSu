using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLNV4_60135761.Models;
using QLNV4_60135761.ViewModel;

namespace QLNV4_60135761.Controllers
{
    public class ThongKeController : Controller
    {
        private QLNV4_60135761Entities _context = new QLNV4_60135761Entities();
        // GET: ThongKe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetGender()
        {

            bool male = true;
            bool female = false;
            int maleCount = _context.NhanViens.Where(x => x.GioiTinh == male && x.TrangThai == "Đang làm việc").Count();
            int femaleCount = _context.NhanViens.Where(x => x.GioiTinh == female && x.TrangThai == "Đang làm việc").Count();
            Ratio obj = new Ratio();
            obj.MaleCount = maleCount;
            obj.FemaleCount = femaleCount;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTrinhDo()
        {
            int cdcount = _context.NhanViens.Where(x => x.MaTD == "TD01" && x.TrangThai == "Đang làm việc").Count();
            int dhcount = _context.NhanViens.Where(x => x.MaTD == "TD02" && x.TrangThai == "Đang làm việc").Count();
            int tscount = _context.NhanViens.Where(x => x.MaTD == "TD03" && x.TrangThai == "Đang làm việc").Count();
            int thscount = _context.NhanViens.Where(x => x.MaTD == "TD04" && x.TrangThai == "Đang làm việc").Count();
            TrinhDoRetio obj = new TrinhDoRetio();
            obj.CaoDang = cdcount;
            obj.Daihoc = dhcount;
            obj.TienSi = tscount;
            obj.ThacSi = thscount;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BienDongNhanSu()
        {
            int lamviec = _context.NhanViens.Where(x => x.TrangThai == "Đang làm việc").Count();
            int nghiviec = _context.NhanViens.Where(x => x.TrangThai == "Nghỉ việc").Count();
            BienDongNV obj = new BienDongNV();
            obj.LamViec = lamviec;
            obj.NghiViec = nghiviec;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBoPhan()
        {
            
            List<NhanVien> listnv = new List<NhanVien>();
            listnv = _context.NhanViens.Where(x => x.TrangThai == "Đang làm việc").ToList();

            int hanhchinh = 0;
            int ketoan = 0;
            int it = 0;
            int nhansu = 0;
            int vanhanh = 0;

            List<ViTriCongViec> listvt = new List<ViTriCongViec>();
            listvt = _context.ViTriCongViecs.ToList();
           
            foreach(var i in listnv)
            {

                foreach(var vt in listvt)
                {
                    if(vt.MaVT == i.MaVT)
                    {
                        BoPhan bp = new BoPhan();
                        bp.MaBP = vt.MaBP;
                        if (bp.MaBP == "BP01")
                        {
                            ketoan++;
                        }
                        else if (bp.MaBP == "BP02")
                        {
                            nhansu++;
                        }
                        else if (bp.MaBP == "BP03")
                        {
                            hanhchinh++;
                        }
                        else if (bp.MaBP == "BP04")
                        {
                            it++;
                        }
                        else
                        {
                            vanhanh++;
                        }
                    }
                }                
            }

            BoPhanCount obj = new BoPhanCount();
            obj.HanhChinh = hanhchinh;
            obj.KeToan = ketoan;
            obj.NhanSu = nhansu;
            obj.IT = it;
            obj.VanHanh = vanhanh;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HieuSuat()
        {
            int nam = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            var query = _context.CTPhieuDanhGias.Include("PhieuDanhGia")
                .Where(x => x.PhieuDanhGia.Nam == nam && x.PhieuDanhGia.KyDanhGia == month)
                .GroupBy(x => x.MaPhieu)
                .Select(g => new { name = g.Key, sum = g.Sum(x => x.Diem) }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}