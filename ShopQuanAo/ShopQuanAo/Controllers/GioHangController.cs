using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopQuanAo.Models;
namespace ShopQuanAo.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        DataClasses1DataContext db = new DataClasses1DataContext();

        public ActionResult GioHang()
        {
            List<GioHang> GH = LayGioHang();
            ViewBag.tsl = GH.Sum(g => g.Sl);
            ViewBag.tt = GH.Sum(g => g.ThanhTien);
            return View(GH);
        }
        public List<GioHang> LayGioHang()
        {
            List<GioHang> GH = Session["GioHang"] as List<GioHang>;
            if (GH == null)
            {
                GH = new List<GioHang>();
                Session["GioHang"] = GH;
            }
            return GH;
        }
        public ActionResult ThemQAVaoGioHang(int mqa, string url)
        {
            List<GioHang> GH = LayGioHang();
            GioHang moi = GH.Find(g => g.MaSP == mqa);
            if (moi == null)
            {
                QuanAo sp = db.QuanAos.Single(s => s.MaQA == mqa);
                int gia = Convert.ToInt32(sp.GiaBan.Value);
                GH.Add(new GioHang(mqa, sp.TenQA, sp.Anh, 1, gia, gia));
            }
            else
            {
                moi.ThanhTien += moi.Gia;
                moi.Sl++;
            }
            Session["TongGH"] = GH.Sum(g => g.Sl);
            return Redirect(url);
        }

        public ActionResult XoaGioHang(int masp)
        {
            //Lấy Giỏ Hàng
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra xem quần áo cần xóa có trong giỏ hàng không?
            GioHang sp = lstGioHang.Single(s => s.MaSP == masp);

            //Nếu có thì tiến hành xóa
            if(sp != null)
            {
                lstGioHang.RemoveAll(s => s.MaSP == masp);
                    return RedirectToAction("GioHang","GioHang");
            }
            //Nếu giỏ hàng rổng
            if(lstGioHang.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("GioHang","GioHang");
        }
        public ActionResult XoaGioHang_All()
        {
            //Lấy giỏ hàng ra
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult CapNhatGioHang( FormCollection f)
        {
            //Lây giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            for(int i =0;i<lstGioHang.Count();i++)
            {
                lstGioHang[i].Sl = int.Parse(f["Sl" + i.ToString()]);
                lstGioHang[i].ThanhTien = lstGioHang[i].Gia * lstGioHang[i].Sl;
            }
            ViewBag.tsl = lstGioHang.Sum(g => g.Sl);
            ViewBag.tt = lstGioHang.Sum(g => g.ThanhTien);
            return RedirectToAction("GioHang", "GioHang");
        }
        [HttpGet]
        public ActionResult DatHang(string url)
        {
            Session["url"] = url;
            if (Session["TaiKhoan"] == null )
            {               
                return RedirectToAction("DangNhap", "NguoiDung");
            }           
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.tsl = lstGioHang.Sum(g => g.Sl);
            ViewBag.tt = lstGioHang.Sum(g => g.ThanhTien);
            return View(lstGioHang);
        }
        public ActionResult DatHang(FormCollection f)
        {

            //thêm đơn hàng
            DonHang ddh = new DonHang();
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            List<GioHang> gh = LayGioHang();
            ddh.MaDonHang = db.DonHangs.Count() + 1;
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", f["NgayGiao"]);
            ddh.TinhTrangGiaoHang = 0;
            ddh.DaThanhToan = "Chua";
            db.DonHangs.InsertOnSubmit(ddh);
            db.SubmitChanges();
            //thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                ChiTietDonHang ctDH = new ChiTietDonHang();
                ctDH.MaDonHang = ddh.MaDonHang;
                ctDH.MaQA = item.MaSP;
                ctDH.SoLuong = item.Sl;
                ctDH.DonGia = (decimal)item.Gia;
                db.ChiTietDonHangs.InsertOnSubmit(ctDH);
            }
            db.SubmitChanges();
            Session.Remove("GioHang"); 
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }

    }
}