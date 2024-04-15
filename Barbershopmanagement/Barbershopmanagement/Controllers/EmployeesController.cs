using Barbershopmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [HttpPost]
        public ActionResult Comment(string comment, int id)
        {

            if (Session["userid"] == null)
            {
                return RedirectToAction("Login", "Security");
            }
            int userID = (int)Session["userid"];
            var dongHang = db.DONHANGs.Where(m => m.NHANVIENID == id && m.USERID == userID && m.TINHTRANGID == 3).FirstOrDefault();
            if (dongHang != null)
            {
                COMMENT_NHANVIEN cm = new COMMENT_NHANVIEN();
                cm.USERID = userID;
                cm.NHANVIENID = id;
                cm.NOIDUNG = comment;
                db.COMMENT_NHANVIEN.Add(cm);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex) { }
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