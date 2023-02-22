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
    public partial class HopDong
    {
        [Required(ErrorMessage = "Nhập số hợp đồng.")]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "Số hợp đồng gồm 9 kí tự là ngày giờ và năm hiện tại, ví dụ: I20210101.")]
        public string SoHD { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Nhập ngày ký.")]
        public System.DateTime NgayKy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Nhập ngày bắt đầu.")]
        public System.DateTime NgayBatDau { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Nhập ngày kết thúc.")]
        public System.DateTime NgayKetThuc { get; set; }
        [Required(ErrorMessage = "Nhập tên người đại diện công ty ký hợp đồng.")]
        public string NguoiDaiDien { get; set; }
        public string TinhTrang { get; set; }
        public string GhiChu { get; set; }
        public string MaNV { get; set; }
        public string MaLuong { get; set; }
    
        public virtual LuongNhanVien LuongNhanVien { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
