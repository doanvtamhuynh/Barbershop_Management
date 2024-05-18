using Barbershopmanagement.App_Start;
using Barbershopmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barbershopmanagement.Controllers
{
    [StaffAuthorize]
    public class LichLamViecController : Controller
    {
        BarbershopManagementEntities db = new BarbershopManagementEntities();
        // GET: LichLamViec
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Security");
            }
            USER user = (USER)Session["user"];
            var datetime = DateTime.Now;
            List<DONHANG> dsDH = db.DONHANGs.Where(m => m.NHANVIEN.USERID == user.USERID && m.TINHTRANGID != 1
                                                        && m.THOIGIAN.Value.Day >= datetime.Day
                                                        && m.THOIGIAN.Value.Month >= datetime.Month
                                                        && m.THOIGIAN.Value.Year >= datetime.Year
                                                        ).OrderBy(m => m.THOIGIAN).ToList();
            return View(dsDH);
        }

        public ActionResult Details(int id) 
        {
            var model = db.CHITIETDONHANGs.Where(m => m.DONHANGID == id);
            return View(model);
        }

        public ActionResult TimKiemTheoNgay(DateTime datetime) 
        {
            USER user = (USER)Session["user"];
            List<DONHANG> dsDH = db.DONHANGs.Where(m => m.NHANVIEN.USERID == user.USERID && m.TINHTRANGID != 1
                                                        && m.THOIGIAN.Value.Day == datetime.Day
                                                        && m.THOIGIAN.Value.Month == datetime.Month
                                                        && m.THOIGIAN.Value.Year == datetime.Year
                                                        ).OrderBy(m => m.THOIGIAN).ToList();
            ViewBag.selectDay = datetime;
            return View(dsDH);
        }
        
        public ActionResult TimKiemTheoThang(DateTime month) 
        {
            USER user = (USER)Session["user"];
            var datetime = DateTime.Now;
            List<DONHANG> dsDH = db.DONHANGs.Where(m => m.NHANVIEN.USERID == user.USERID && m.TINHTRANGID != 1
                                                        && m.THOIGIAN.Value.Month == month.Month
                                                        && m.THOIGIAN.Value.Year == month.Year
                                                        ).OrderBy(m => m.THOIGIAN).ToList();
            ViewBag.selectDay = month;
            return View(dsDH);
        }

        public ActionResult TimKiemTheoNam(int year) 
        {
            USER user = (USER)Session["user"];
            var datetime = DateTime.Now;
            List<DONHANG> dsDH = db.DONHANGs.Where(m => m.NHANVIEN.USERID == user.USERID && m.TINHTRANGID != 1
                                                        && m.THOIGIAN.Value.Year == year
                                                        ).OrderBy(m => m.THOIGIAN).ToList();
            ViewBag.selectDay = year;
            return View(dsDH);
        }
    }
}