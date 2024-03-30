using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopQuanAo.Models;
namespace ShopQuanAo.Controllers
{
    public class QuanAoController : Controller
    {
        // GET: QuanAo
        DataClasses1DataContext db = new DataClasses1DataContext();

        public ActionResult QuanAoPartial()
        {
            return View(db.QuanAos.ToList());
        }
        public ActionResult QuanAoTheoLoai(int MaLoai)
        {
            return View(db.QuanAos.Where(s => s.MaLoai == MaLoai).ToList());
        }
        public ActionResult XemChiTiet(int mqa)
        {
            List<BinhLuan> BLS = db.BinhLuans.Where(s => s.MaQA == mqa).ToList();
            List<TraLoiBinhLuan> TLBLs = db.TraLoiBinhLuans.Where(s => s.BinhLuan.MaQA == mqa).ToList();
            ViewBag.bls = BLS;
            ViewBag.tlbls = TLBLs;
            return View(db.QuanAos.Single(s => s.MaQA == mqa));
        }
        public ActionResult DanhGia(int MaQA)
        {
            return View(db.QuanAos.Single(s => s.MaQA == MaQA));
        }
        public ActionResult BinhLuan(int maQA, string url,string NoiDung)
        {
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            if (kh != null)
            {
                BinhLuan moi = new BinhLuan();
                moi.MaBL = db.BinhLuans.Count() + 1;
                moi.MaKH = kh.MaKH;
                moi.NoiDung = NoiDung;
                moi.MaQA = maQA;
                moi.NgayDang = DateTime.Now;
                db.BinhLuans.InsertOnSubmit(moi);
                db.SubmitChanges();
                return RedirectToAction("XemChiTiet", "QuanAo", new { mqa = maQA });
            }
            else
            {
                Session["url"] = url;
                return RedirectToAction("DangNhap", "NguoiDung");
            }
        }
        public ActionResult TraLoiBinhLuan(int maBL, int maQA, string url, string NoiDung)
        {
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            if (kh != null)
            {
                TraLoiBinhLuan moi = new TraLoiBinhLuan();
                moi.MaTLBL = db.TraLoiBinhLuans.Count() + 1;
                moi.MaBL = maBL;
                moi.MaKH = kh.MaKH;
                moi.NoiDung = NoiDung;
                moi.NgayDang = DateTime.Now;
                db.TraLoiBinhLuans.InsertOnSubmit(moi);
                db.SubmitChanges();
                return RedirectToAction("XemChiTiet", "QuanAo", new { mqa = maQA });
            }
            else
            {
                Session["url"] = url;
                return RedirectToAction("DangNhap", "NguoiDung");
            }
        }
        
    }
}