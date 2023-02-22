using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using QLNV4_60135761.ViewModel;
using QLNV4_60135761.Models;
using PagedList.Mvc;
using PagedList;

namespace QLNV4_60135761.Controllers
{
    public class PhieuDanhGias_60135761Controller : Controller
    {
        private QLNV4_60135761Entities db = new QLNV4_60135761Entities();
        string LayMaPhieu()
        {            
            var maMax = db.PhieuDanhGias.ToList().Select(n => n.MaPhieu).Max();
            int maPhieu = int.Parse(maMax.Substring(1)) + 1;
            string P = String.Concat("0", maPhieu.ToString());
            return "P" + P.Substring(maPhieu.ToString().Length-1);
        }
        // GET: PhieuDanhGias_60135761
        public ActionResult Index(int? page, string search)
        {
            if (page == null)
                page = 1;
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            ViewBag.SoPhieu = db.PhieuDanhGias.Where(x => x.MaPhieu.Contains(search) || search == null).ToList().Count();

            return View(db.PhieuDanhGias.Where(x => x.MaPhieu.Contains(search) || search == null).ToList().ToPagedList(pageNumber, pageSize));           
        }

        public string TenTieuChi(int matc)
        {
            TieuChiYeuCau tieuchi = db.TieuChiYeuCaus.Find(matc);
            return tieuchi.TenTieuChi;
        }
        public string LoaiDanhGia(int matc)
        {
            // tìm ra tiêu chí có cùng matc
            TieuChiYeuCau tieuchi = db.TieuChiYeuCaus.Find(matc);

            // trả về tên loại đánh giá của tiêu chí
            return tieuchi.LoaiDanhGia.TenLoaiDanhGia.ToString();
        }

        public List<TieuChi> DanhSachTieuChi()
        {
            // lấy ra số lượng các tiêu chí yêu cầu
            List<TieuChiYeuCau> listtcyc = new List<TieuChiYeuCau>();
            listtcyc = db.TieuChiYeuCaus.ToList();

            //lưu vào danh sách các tiêu chí để đánh giá
            List<TieuChi> listtc = new List<TieuChi>();
            int i = 1;
            foreach (var tc in listtcyc)
            {
                TieuChi tchi = new TieuChi();
                tchi.MaTieuChi = i;
                tchi.TenTieuChi = TenTieuChi(tchi.MaTieuChi);
                //tchi.Diem = 0;
                listtc.Add(tchi);
                i++;
            }

            return listtc;
        }

        public ActionResult DanhGia()
        {
            List<TieuChi> listtc = DanhSachTieuChi();

            ViewBag.tendanhgia = "Đánh giá hiệu suất nhân sự";
            ViewBag.kydanhgia = DateTime.Now.Month;
            ViewBag.nam = DateTime.Now.Year;            
            return View(listtc);
        }

        [HttpPost]
        public ActionResult LuuPhieu(FormCollection form)
        {

            // lấy thông tin từ form
            string tendanhgia = form["tendanhgia"];
            int kydanhgia = int.Parse(form["kydanhgia"]);
            int nam = int.Parse(form["nam"]);            

            //tạo đối tượng phiếu đánh giá
            PhieuDanhGia phieu = new PhieuDanhGia();
            phieu.MaPhieu = LayMaPhieu();
            phieu.TenDanhGia = tendanhgia;
            phieu.KyDanhGia = kydanhgia;
            phieu.Nam = nam;          
            phieu.Khoa = false; //false = 0
            if (Session["MaNhanVien"] == null)
            {
                return RedirectToAction("../TaiKhoans_60135761/DangNhap");
            }
            phieu.MaNV = Session["MaNhanVien"].ToString();

            // lưu phiếu đánh giá
            db.PhieuDanhGias.Add(phieu);
            db.SaveChanges();

            // lấy ra danh sách các tiêu chí
            List<TieuChi> listtc = DanhSachTieuChi();

            //cập nhật điểm lại cho từng tiêu chí
            var listdiem = form["diem"]; // lưu danh sách điểm đã thay đổi-> dạng string
            var listarr = listdiem.Split(',');// đưa về arr gồm các điểm

            int vt = 0;
            foreach (TieuChi tcdiem in listtc)
            {

                if (int.Parse(listarr[vt]) == 0)
                {
                    listtc[vt].Diem = 0;
                }
                listtc[vt].Diem = int.Parse(listarr[vt]);
                vt++;
            }

            foreach (TieuChi tc in listtc)
            {
                CTPhieuDanhGia ctphieu = new CTPhieuDanhGia();
                ctphieu.MaPhieu = phieu.MaPhieu;
                ctphieu.MaTieuChi = tc.MaTieuChi;
                ctphieu.Diem = tc.Diem;
                db.CTPhieuDanhGias.Add(ctphieu);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult ChiTiet(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDanhGia phieuDanhGia = db.PhieuDanhGias.Find(id);
            if (phieuDanhGia == null)
            {
                return HttpNotFound();
            }

            ViewBag.ListChiTiet = db.CTPhieuDanhGias.Where(m => m.MaPhieu == id).ToList();

            return View(phieuDanhGia);
        }

        public ActionResult CapNhatPhieu(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDanhGia phieuDanhGia = db.PhieuDanhGias.Find(id);
            if (phieuDanhGia == null)
            {
                return HttpNotFound();
            }

            //hiển thị danh sách ct phiếu trùng với id
            ViewBag.ListChiTiet = db.CTPhieuDanhGias.Where(m => m.MaPhieu == id).ToList();

            return View(phieuDanhGia);
        }

        [HttpPost]
        public ActionResult CapNhatPhieu(FormCollection form, PhieuDanhGia phieu)
        {

            // lấy ra list các phiếu
            List<CTPhieuDanhGia> listphieu = new List<CTPhieuDanhGia>();
            listphieu = db.CTPhieuDanhGias.Where(x => x.MaPhieu == phieu.MaPhieu).ToList();

            // lấy ra arr điểm đã cập nhật
            var listdiem = form["diem"]; // lưu danh sách điểm đã thay đổi-> dạng string
            var listarr = listdiem.Split(',');// đưa về arr gồm các điểm

            // cập nhật điểm thay đổi
            int vt = 0;
            foreach (var i in listphieu)
            {
                listphieu[vt].Diem = int.Parse(listarr[vt]);
                vt++;
            }

            foreach (CTPhieuDanhGia ct in listphieu)
            {
                db.Entry(ct).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult XoaPhieu(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDanhGia phieuDanhGia = db.PhieuDanhGias.Find(id);
            if (phieuDanhGia == null)
            {
                return HttpNotFound();
            }
            return View(phieuDanhGia);
        }

        [HttpPost, ActionName("XoaPhieu")]
        [ValidateAntiForgeryToken]
        public ActionResult XacNhanXoa(string id)
        {
            List<CTPhieuDanhGia> listtcphieu = new List<CTPhieuDanhGia>();
            listtcphieu = db.CTPhieuDanhGias.Where(m => m.MaPhieu == id).ToList();

            // truyền vào list ctphieu 
            db.CTPhieuDanhGias.RemoveRange(listtcphieu);
            db.SaveChanges();

            PhieuDanhGia phieuDanhGia = db.PhieuDanhGias.Find(id);
            db.PhieuDanhGias.Remove(phieuDanhGia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}