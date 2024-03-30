using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopQuanAo.Models;
namespace ShopQuanAo.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        DataClasses1DataContext db = new DataClasses1DataContext();
        public ActionResult DangKy()
        {
            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("QuanAoPartial", "QuanAo");
        }
        [HttpPost]
        public ActionResult DangKy(KhachHang kh, FormCollection f)
        {
            //gán các giá trị người dùng nhập từ form f cho các biến
            var hoten = f["HoTenKH"];
            var tendn = f["TenDN"];
            var matkhau = f["MatKhau"];
            var remakhau = f["ReMatKhau"];
            var dienthoai = f["DienThoai"];
            var ngaysinh = String.Format("{0:MM/DD/YYYY}", f["NgaySinh"]);
            var email = f["Email"];
            var diachi = f["DiaChi"];

            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ Tên không được bỏ trống";

            }
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Tên đăng nhập không được bỏ trống";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Vui lòng nhập mật khẩu";
            }
            if (String.IsNullOrEmpty(remakhau))
            {
                ViewData["Loi4"] = "Vui lòng nhập mật khẩu";
            }
            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi5"] = "Vui lòng nhập số điện thoại";
            }
            if (String.IsNullOrEmpty(diachi))
            {
                ViewData["Loi6"] = "Vui lòng nhập địa chỉ";
            }
            if (!String.IsNullOrEmpty(hoten) && !String.IsNullOrEmpty(tendn) && !String.IsNullOrEmpty(matkhau) && !String.IsNullOrEmpty(remakhau) && !String.IsNullOrEmpty(dienthoai))
            {
                //Gán giá trị cho đối tượng kh
                kh.MaKH = (db.KhachHangs.Count() + 1);
                kh.HoTen = hoten;
                kh.NgaySinh = DateTime.Parse(ngaysinh);
                kh.TaiKhoan = tendn;
                kh.MatKhau = matkhau;
                kh.Email = email;
                kh.DiaChi = diachi;
                //Ghi xuống csdl
                db.KhachHangs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("DangNhap", "NguoiDung");

            }

            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f, string url)
        {
            //khai báo các biến nhận giá trị từ form f
            var tendn = f["TenDN"];
            var matkhau = f["Matkhau"];
            ViewBag.ten = tendn;
            ViewBag.mk = matkhau;
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Tên đăng nhập không bỏ trống";                 
                return View();

            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu";
                return View();
            }
            if (!String.IsNullOrEmpty(tendn) && !String.IsNullOrEmpty(matkhau))
            {
                KhachHang kh = db.KhachHangs.SingleOrDefault(c => c.TaiKhoan == tendn && c.MatKhau == matkhau);
                if (kh != null)
                {
                    Session["taikhoan"] = kh;
                }
                else
                {
                    ViewBag.ten = null;
                    ViewBag.mk = null;
                    ViewBag.TB = "Sai tên đăng nhập hoặc mật khẩu, vui lòng kiểm tra lại!";
                    return View();
                }

            }
            if (Session["url"] != null)
                return Redirect(Session["url"].ToString());
            else
                return RedirectToAction("QuanAoPartial", "QuanAo");
        }
        
    }
}