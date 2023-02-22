using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLNV4_60135761.Models;
using ClosedXML.Excel;
using System.IO;
using PagedList.Mvc;
using PagedList;

namespace QLNV4_60135761.Controllers
{

    public class NhanViens_60135761Controller : Controller
    {
        private QLNV4_60135761Entities db = new QLNV4_60135761Entities();
        string LayMaNV()
        {
            var maMax = db.NhanViens.ToList().Select(n => n.MaNV).Max();
            int maNV = int.Parse(maMax.Substring(2)) + 1;
            string NV = String.Concat("00", maNV.ToString());
            return "NV" + NV.Substring(maNV.ToString().Length - 1);
        }
        // GET: NhanViens_60135761
        public ActionResult Index(string search, int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.SoNV = db.NhanViens.OrderBy(x=>x.HoTenNV).Where(x => x.HoTenNV.Contains(search) || search == null).ToList().Count();

            return View(db.NhanViens.OrderBy(x => x.HoTenNV).Where(x => x.HoTenNV.Contains(search) || search == null).ToList().ToPagedList(pageNumber, pageSize));
        }
       
        public string TenNguoiQuanLy(string manvql)
        {
            NhanVien nvql = db.NhanViens.Find(manvql);
            return nvql.HoTenNV;            
        }

        // GET: NhanViens_60135761/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: NhanViens_60135761/Create
        public ActionResult Create()
        {
            ViewBag.MaCV = new SelectList(db.ChucVus, "MaCV", "TenChucVu");
            ViewBag.MaLoaiNV = new SelectList(db.LoaiNhanViens, "MaLoaiNV", "TenLoaiNhanVien");
            ViewBag.MaNVQL = new SelectList(db.NhanViens, "MaNV", "HoTenNV");
            ViewBag.MaQT = new SelectList(db.QuocTiches, "MaQT", "TenQuocTich");
            ViewBag.MaTD = new SelectList(db.TrinhDoDaoTaos, "MaTD", "TenTrinhDo");
            ViewBag.MaVT = new SelectList(db.ViTriCongViecs, "MaVT", "TenViTriCongViec");
            ViewBag.MaNV = LayMaNV();
            return View();
        }

        // POST: NhanViens_60135761/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,HoTenNV,NgaySinh,GioiTinh,AnhNV,DiaChi,SDT,CMND,TrangThai,MaNVQL,MaVT,MaTD,MaQT,MaCV,MaLoaiNV")] NhanVien nhanVien)
        {
            var imgNV = Request.Files["Avatar"];

            string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);

            var path = Server.MapPath("/Public/Images/" + postedFileName);
            imgNV.SaveAs(path);


            if (ModelState.IsValid)
            {
                nhanVien.MaNV = LayMaNV();
                nhanVien.AnhNV = postedFileName;
                db.NhanViens.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCV = new SelectList(db.ChucVus, "MaCV", "TenChucVu", nhanVien.MaCV);
            ViewBag.MaLoaiNV = new SelectList(db.LoaiNhanViens, "MaLoaiNV", "TenLoaiNhanVien", nhanVien.MaLoaiNV);
            ViewBag.MaNVQL = new SelectList(db.NhanViens, "MaNV", "HoTenNV", nhanVien.MaNVQL);
            ViewBag.MaQT = new SelectList(db.QuocTiches, "MaQT", "TenQuocTich", nhanVien.MaQT);
            ViewBag.MaTD = new SelectList(db.TrinhDoDaoTaos, "MaTD", "TenTrinhDo", nhanVien.MaTD);
            ViewBag.MaVT = new SelectList(db.ViTriCongViecs, "MaVT", "TenViTriCongViec", nhanVien.MaVT);
            return View(nhanVien);
        }

        // GET: NhanViens_60135761/Edit/5
        public ActionResult Edit(string id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCV = new SelectList(db.ChucVus, "MaCV", "TenChucVu", nhanVien.MaCV);
            ViewBag.MaLoaiNV = new SelectList(db.LoaiNhanViens, "MaLoaiNV", "TenLoaiNhanVien", nhanVien.MaLoaiNV);
            ViewBag.MaNVQL = new SelectList(db.NhanViens, "MaNV", "HoTenNV", nhanVien.MaNVQL);
            ViewBag.MaQT = new SelectList(db.QuocTiches, "MaQT", "TenQuocTich", nhanVien.MaQT);
            ViewBag.MaTD = new SelectList(db.TrinhDoDaoTaos, "MaTD", "TenTrinhDo", nhanVien.MaTD);
            ViewBag.MaVT = new SelectList(db.ViTriCongViecs, "MaVT", "TenViTriCongViec", nhanVien.MaVT);
            return View(nhanVien);
        }

        // POST: NhanViens_60135761/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,HoTenNV,NgaySinh,GioiTinh,AnhNV,DiaChi,SDT,CMND,TrangThai,MaNVQL,MaVT,MaTD,MaQT,MaCV,MaLoaiNV")] NhanVien nhanVien)
        {
            var imgNV = Request.Files["Avatar"];
            try
            {
                string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);
                var path = Server.MapPath("/Public/Images/" + postedFileName);
                imgNV.SaveAs(path);
            }
            catch
            {

            }
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCV = new SelectList(db.ChucVus, "MaCV", "TenChucVu", nhanVien.MaCV);
            ViewBag.MaLoaiNV = new SelectList(db.LoaiNhanViens, "MaLoaiNV", "TenLoaiNhanVien", nhanVien.MaLoaiNV);
            ViewBag.MaNVQL = new SelectList(db.NhanViens, "MaNV", "HoTenNV", nhanVien.MaNVQL);
            ViewBag.MaQT = new SelectList(db.QuocTiches, "MaQT", "TenQuocTich", nhanVien.MaQT);
            ViewBag.MaTD = new SelectList(db.TrinhDoDaoTaos, "MaTD", "TenTrinhDo", nhanVien.MaTD);
            ViewBag.MaVT = new SelectList(db.ViTriCongViecs, "MaVT", "TenViTriCongViec", nhanVien.MaVT);
            return View(nhanVien);
        }

        // GET: NhanViens_60135761/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanViens_60135761/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            db.NhanViens.Remove(nhanVien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public FileResult ExportToExcel()
        {
            DataTable dt = new DataTable("Grid");

            dt.Columns.AddRange(new DataColumn[] { new DataColumn("STT"),
                                                    new DataColumn("Mã nhân viên"),
                                                     new DataColumn("Họ và tên"),
                                                     new DataColumn("Giới tính"),
                                                     new DataColumn("Địa chỉ"),
                                                     new DataColumn("SĐT"),
                                                     new DataColumn("CMND"),
                                                     new DataColumn("Trạng thái"),
                                                     new DataColumn("Người quản lý"),
                                                     new DataColumn("Chức vụ"),                                                   
                                                     new DataColumn("Quốc tịch"),
                                                     new DataColumn("Trình độ"),
                                                     new DataColumn("Vị trí công việc")});

            var nvList = from nv in db.NhanViens select nv;

            int i = 1;
            foreach (var item in nvList)
            {

                dt.Rows.Add(i, item.MaNV, item.HoTenNV, item.GioiTinh == true ? "Nam" : "Nữ", item.DiaChi,
                    item.SDT, item.CMND, item.TrangThai, TenNguoiQuanLy(item.MaNVQL),
                    item.ChucVu.TenChucVu, item.QuocTich.TenQuocTich, item.TrinhDoDaoTao.TenTrinhDo, item.ViTriCongViec.TenViTriCongViec);
                i++;
            }

            using (XLWorkbook wb = new XLWorkbook())
            {

                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachNhanVienINFOdation.xlsx");
                }
            }
        }

        public ActionResult TimKiemNV(string hoten = "", string gtinh = "", string diachi = "", string maCV = "", string maloaiNV = "", string maQT = "", string maTD = "", string maVT = "")
        {
            if (gtinh != "1" && gtinh != "0")
                gtinh = null;
            ViewBag.hoTen = hoten;
            ViewBag.gtinh = gtinh;
            ViewBag.diachi = diachi;
            ViewBag.MaCV = new SelectList(db.ChucVus, "MaCV", "TenChucVu");                      
            ViewBag.MaLoaiNV = new SelectList(db.LoaiNhanViens, "MaLoaiNV", "TenLoaiNhanVien");
            ViewBag.MaQT = new SelectList(db.QuocTiches, "MaQT", "TenQuocTich");
            ViewBag.MaTD = new SelectList(db.TrinhDoDaoTaos, "MaTD", "TenTrinhDo");
            ViewBag.MaVT = new SelectList(db.ViTriCongViecs, "MaVT", "TenViTriCongViec");
            var nhanViens = db.NhanViens.SqlQuery("NhanVien_TimKiem N'" + hoten + "','" + gtinh + "',N'" + diachi + "','" + maCV + "','" + maloaiNV + "','" + maQT + "','" + maTD + "','" + maVT + "'");
            if (nhanViens.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";

            ViewBag.Count = nhanViens.Count();

            return View(nhanViens.ToList());

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
