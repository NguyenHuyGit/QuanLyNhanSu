using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNV4_60135761.ViewModel
{
    public class TieuChi
    {
        public int MaTieuChi { get; set; }
        public string TenTieuChi { get; set; }
        public int Diem { get; set; }

        public TieuChi()
        {

        }

        public TieuChi(int matc, string ten, int diem)
        {
            this.MaTieuChi = matc;
            this.TenTieuChi = ten;
            this.Diem = diem;
        }
    }
}