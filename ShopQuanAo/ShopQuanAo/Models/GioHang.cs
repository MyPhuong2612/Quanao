using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopQuanAo.Models
{
    public class GioHang
    {
        int maSP;
        string tenSP;
        string anh;
        int sl;
        int gia;
        int thanhTien;

        public GioHang(int maSP, string tenSP, string anh, int sl, int gia, int thanhTien)
        {
            this.MaSP = maSP;
            this.TenSP = tenSP;
            this.Anh = anh;
            this.Sl = sl;
            this.Gia = gia;
            this.ThanhTien = thanhTien;
        }
        public int MaSP
        {
            get { return maSP; }
            set { maSP = value; }
        }
        public string TenSP
        {
            get { return tenSP; }
            set { tenSP = value; }
        }
        public string Anh
        {
            get { return anh; }
            set { anh = value; }
        }
        public int Sl
        {
            get { return sl; }
            set { sl = value; }
        }
        public int Gia
        {
            get { return gia; }
            set { gia = value; }
        }
        public int ThanhTien
        {
            get { return thanhTien; }
            set { thanhTien = value; }
        }
    }

}