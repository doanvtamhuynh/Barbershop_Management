using Barbershopmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barbershopmanagement.Areas.Admin.Controllers
{
    public class ThongKeDoanhThuController : Controller
    {
        Barbershopmanagement.Helpers.Authorization auth = new Barbershopmanagement.Helpers.Authorization();
        BarbershopManagementEntities db = new BarbershopManagementEntities();
        // GET: Admin/ThongKeDoanhThu
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                USER user = (USER)Session["user"];
                if (auth.isAdmin(user.ROLE) == true)
                {
                    var thoigian = new DateTime();
                    thoigian = DateTime.Now;
                    var yyyy = thoigian.Year.ToString();
                    var mm = thoigian.Month.ToString();
                    if (mm.Length == 1)
                    {
                        mm = "0" + mm;
                    }

                    var dd = thoigian.Day.ToString();
                    if (dd.Length == 1)
                    {
                        dd = "0" + dd;
                    }
                    string selectDay = yyyy + "-" + mm + "-" + dd;
                    List<spDonHangTheoNgay_Result> dsDHTN = db.spDonHangTheoNgay(selectDay).ToList();
                    ViewBag.selectDay = selectDay;
                    return View(dsDHTN);
                }
                else
                {
                    return RedirectToAction("Index", "HomePage");
                }
            }
            return Redirect("/Security/Login");
        }

        public ActionResult TimKiemDonHangTheoNgay(DateTime datetime)
        {
            if (Session["user"] != null)
            {
                USER user = (USER)Session["user"];
                if (auth.isAdmin(user.ROLE) == true)
                {
                    var yyyy = datetime.Year.ToString();
                    var mm = datetime.Month.ToString();
                    if (mm.Length == 1)
                    {
                        mm = "0" + mm;
                    }

                    var dd = datetime.Day.ToString();
                    if (dd.Length == 1)
                    {
                        dd = "0" + dd;
                    }
                    string selectDay = yyyy + "-" + mm + "-" + dd;
                    List<spDonHangTheoNgay_Result> dsDHTN = db.spDonHangTheoNgay(selectDay).ToList();
                    ViewBag.selectDay = datetime;
                    return View(dsDHTN);
                }
                else
                {
                    return RedirectToAction("Index", "HomePage");
                }
            }
            return Redirect("/Security/Login");
             
        }

        public ActionResult TimKiemDonHangTheoThang(DateTime month)
        {
            if (Session["user"] != null)
            {
                USER user = (USER)Session["user"];
                if (auth.isAdmin(user.ROLE) == true)
                {
                    var yyyy = month.Year.ToString();
                    var mm = month.Month.ToString();
                    if (mm.Length == 1)
                    {
                        mm = "0" + mm;
                    }
                    List<spDonHangTheoThang_Result> dsDHTT = db.spDonHangTheoThang(mm, yyyy).ToList();
                    ViewBag.selectMonth = month;
                    return View(dsDHTT);
                }
                else
                {
                    return RedirectToAction("Index", "HomePage");
                }
            }
            return Redirect("/Security/Login");
            
        }

        public ActionResult TimKiemDonHangTheoNam(int year)
        {
            if (Session["user"] != null)
            {
                USER user = (USER)Session["user"];
                if (auth.isAdmin(user.ROLE) == true)
                {
                    var yyyy = year.ToString();
                    List<spDonHangTheoNam_Result> dsDHTN = db.spDonHangTheoNam(yyyy).ToList();
                    ViewBag.selectYear = year;
                    return View(dsDHTN);
                }
                else
                {
                    return RedirectToAction("Index", "HomePage");
                }
            }
            return Redirect("/Security/Login");
            
        }
    }
}