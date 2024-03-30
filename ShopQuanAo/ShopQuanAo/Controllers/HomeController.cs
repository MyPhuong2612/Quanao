using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopQuanAo.Models;
namespace ShopQuanAo.Controllers
{
    public class HomeController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        
        public ActionResult Index()
        {
            return View(db.QuanAos.ToList());
        }
        public ActionResult AoPartial()
        {
            return View(db.Loais.Where(l => l.Note.Equals("Áo")).Take(5).ToList().OrderBy(l => l.TenLoai).ToList());
        }
        public ActionResult QuanPartial()
        {
            return View(db.Loais.Where(l => l.Note.Equals("Quần")).OrderByDescending(l => l.TenLoai).Take(5).ToList().OrderBy(l => l.TenLoai).ToList());
        }
        [HttpPost]
        public ActionResult Search(string txt_Search)
        {
            return View(db.QuanAos.Where(s => s.TenQA.Contains(txt_Search)).ToList());
        }
        public ActionResult INDEX2()
        {
            return View();
        }
    }
}
