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

    public partial class DeXuatTangLuong
    {
        public string MaDeXuat { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime NgayDeXuat { get; set; }
        [Required(ErrorMessage = "Nhập lí do đề xuất.")]
        public string LiDoDeXuat { get; set; }
        [Required(ErrorMessage = "Nhập mức lương mong muốn.")]
        [Range(155, Int32.MaxValue, ErrorMessage = "Mức lương ít nhất là {1} USD hoặc 3,500,000 VND.")]
        [DisplayFormat(DataFormatString = "{0:#,##0 đồng}", ApplyFormatInEditMode = false)]
        public int LuongMongMuon { get; set; }
        public string TrangThai { get; set; }
        public string MaNV { get; set; }
    
        public virtual NhanVien NhanVien { get; set; }
    }
}