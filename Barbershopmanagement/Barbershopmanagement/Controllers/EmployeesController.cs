using Barbershopmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Barbershopmanagement.App_Start;

namespace BarbershopManagement.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        BarbershopManagementEntities db = new BarbershopManagementEntities();
        public ActionResult Employees()
        {
            List<NHANVIEN> dsNhanVien = db.NHANVIENs.ToList();
            return View(dsNhanVien);
        }

        public ActionResult Details(int id)
        {
            NHANVIEN model = db.NHANVIENs.Find(id);
            return View(model);
        }


        [Logged]
        [HttpPost]
        public ActionResult Comment(string comment, int id)
        {

            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Security");
            }
            USER user = (USER)Session["user"];
            var dongHang = db.DONHANGs.Where(m => m.NHANVIENID == id && m.USERID == user.USERID && m.TINHTRANGID == 3).FirstOrDefault();
            if (dongHang != null)
            {
                COMMENT_NHANVIEN cm = new COMMENT_NHANVIEN();
                cm.USERID = user.USERID;
                cm.NHANVIENID = id;
                cm.NOIDUNG = comment;
                db.COMMENT_NHANVIEN.Add(cm);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e) { }
                return RedirectToAction("Details", new { id });
            }
            else
            {
                TempData["comment_employee"] = "Bạn chưa sử dụng dịch vụ từ nhân viên này.";
                return RedirectToAction("Details", new { id });
            }

        }
    }
}