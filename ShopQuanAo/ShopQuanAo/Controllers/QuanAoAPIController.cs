using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopQuanAo.Models;
namespace ShopQuanAo.Controllers
{
    public class QuanAoAPIController : ApiController
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        [HttpGet]
        public List<QuanAo> GetListQuanAoAPI(string MaLoai)
        {
            return db.QuanAos.Where(Q => Q.MaLoai.Equals(MaLoai)).ToList();

        }
        [HttpGet]
        public QuanAo GetSanPham(int mqa)
        {
            return db.QuanAos.Single(s => s.MaQA == mqa);
        }
    }
}
