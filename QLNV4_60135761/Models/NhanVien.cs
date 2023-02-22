﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLNV4_60135761.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            this.DeXuatTangLuongs = new HashSet<DeXuatTangLuong>();
            this.HopDongs = new HashSet<HopDong>();
            this.LuongNhanViens = new HashSet<LuongNhanVien>();
            this.NhanVien1 = new HashSet<NhanVien>();
            this.PhieuDanhGias = new HashSet<PhieuDanhGia>();
            this.QuyetDinhThangChucs = new HashSet<QuyetDinhThangChuc>();
            this.TaiKhoans = new HashSet<TaiKhoan>();
        }
    
        public string MaNV { get; set; }
        [Required(ErrorMessage = "Nhập họ tên nhân viên.")]
        public string HoTenNV { get; set; }
        [Required(ErrorMessage = "Nhập ngày sinh.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string AnhNV { get; set; }
        [Required(ErrorMessage = "Nhập địa chỉ.")]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Nhập số điện thoại.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Nhập số điện thoại gồm 10 chữ số.")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "Nhập số chứng minh thư.")]
        [StringLength(20, MinimumLength = 9, ErrorMessage = "Nhập số chứng minh thư gồm 9 chữ số.")]
        public string CMND { get; set; }
        public string TrangThai { get; set; }
        public string MaNVQL { get; set; }
        public string MaVT { get; set; }
        public string MaTD { get; set; }
        public string MaQT { get; set; }
        public string MaCV { get; set; }
        public string MaLoaiNV { get; set; }
    
        public virtual ChucVu ChucVu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeXuatTangLuong> DeXuatTangLuongs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HopDong> HopDongs { get; set; }
        public virtual LoaiNhanVien LoaiNhanVien { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LuongNhanVien> LuongNhanViens { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanVien> NhanVien1 { get; set; }
        public virtual NhanVien NhanVien2 { get; set; }
        public virtual QuocTich QuocTich { get; set; }
        public virtual TrinhDoDaoTao TrinhDoDaoTao { get; set; }
        public virtual ViTriCongViec ViTriCongViec { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDanhGia> PhieuDanhGias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuyetDinhThangChuc> QuyetDinhThangChucs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
