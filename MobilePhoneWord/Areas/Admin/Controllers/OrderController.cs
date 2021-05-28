using MobilePhoneWord.Areas.Admin.Models;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobilePhoneWord.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        PhoneDbContext db = new PhoneDbContext();
        // GET: Admin/Order
        public ActionResult Index()
        {
            //var lstOrder = (from dh in db.DonHang
            //                join ctdh in db.ChiTietDonHang on dh.MaDH equals ctdh.MaDH
            //                join p in db.Products on ctdh.MaSP equals p.ProductID
            //                join kh in db.KhachHang on dh.MaKH equals kh.KHID
            //                select new OrderViewModel
            //                {
            //                    MaDH = dh.MaDH,
            //                    NgayMua = dh.NgayMua,
            //                    FullName = kh.FullName,
            //                    ProductName = p.ProductName,
            //                    PhoneNum = kh.PhoneNum,
            //                    NVAddress = kh.NVAddress,
            //                    SoLuong = ctdh.SoLuong,
            //                    Price = p.Price,
            //                    ThanhTien = ctdh.SoLuong * p.Price,
            //                    Status = dh.Status
            //                }).ToList();
            //var lstOrder = (from dh in db.DonHang
            //               select dh).ToList();
            return View();
        }
    }
}