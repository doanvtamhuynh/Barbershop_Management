﻿using Barbershopmanagement.App_Start;
using Barbershopmanagement.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Barbershopmanagement.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class HomeController : Controller
    {
        BarbershopManagementEntities db = new BarbershopManagementEntities();
        public ActionResult Index()
        {
            DateTime datetime = DateTime.Now;
            var dsDonHang = db.DONHANGs.Where(m => m.THOIGIAN.Value.Day == datetime.Day
                                                 && m.THOIGIAN.Value.Month == datetime.Month
                                                 && m.THOIGIAN.Value.Year == datetime.Year
                                                 && m.TINHTRANGID == 3
                                                 ).ToList();
            double doanhThu = 0;
            foreach (var item in dsDonHang)
            {
                var chiTietDH = db.CHITIETDONHANGs.Where(m => m.DONHANGID == item.DONHANGID).ToList();
                foreach (var i in chiTietDH)
                {
                    doanhThu += i.DICHVU.GIADICHVU;
                }
            }
            var dsDonHangMoi = db.DONHANGs.Where(m => m.THOIGIAN.Value.Day >= datetime.Day
                                                 && m.THOIGIAN.Value.Month >= datetime.Month
                                                 && m.THOIGIAN.Value.Year >= datetime.Year
                                                 && m.TINHTRANGID == 1
                                                 ).Count();

            ViewBag.ThongKeKH = db.USERS.Count(m => m.ROLE == "khachhang");
            ViewBag.ThongKeDonHangChuaHT = db.DONHANGs.Where(m => m.THOIGIAN.Value.Day >= datetime.Day
                                                 && m.THOIGIAN.Value.Month >= datetime.Month
                                                 && m.THOIGIAN.Value.Year >= datetime.Year
                                                 && m.TINHTRANGID == 2
                                                 ).Count();
            ViewBag.DoanhThu = doanhThu;
            ViewBag.ThongKeDonHang = dsDonHangMoi;
            return View();
        }

    }
}