using Barbershopmanagement.Models;
using BarbershopManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barbershopmanagement.Areas.Admin.Controllers
{
    public class QLLichHenController : Controller
    {
        BarbershopManagementEntities db = new BarbershopManagementEntities();
        Helpers.Authorization auth = new Helpers.Authorization();
        // GET: Admin/QLLichHen
        public ActionResult Index()
        {
            List<DONHANG> dsDonHang = db.DONHANGs.ToList();
            return View(dsDonHang);
            if (Session["user"] != null)
            {
                USER user = (USER)Session["user"];
                if (auth.isAdmin(user.ROLE) == true)
                {
                    List<DONHANG> dsDonhang = db.DONHANGs.ToList();
                    return View(dsDonhang);
                }
                else
                {
                    return RedirectToAction("Index", "HomePage");
                }
            }
            return Redirect("/Security/Login");
        }

        public ActionResult Details(int id)
        {
            List<CHITIETDONHANG> chitietDH = db.CHITIETDONHANGs.Where(m => m.DONHANGID == id).ToList();
            return View(chitietDH);
        }

        public ActionResult Confirm(int id)
        {
            var comfirmModel = db.DONHANGs.Find(id);
            comfirmModel.TINHTRANGID = 2;
            try
            {
                db.SaveChanges();
            }catch (Exception ex) { }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id, string lydo)
        {
            var model = db.DONHANGs.Find(id);
            EmailService emailService = new EmailService();
            emailService.SendEmail(model.USER.EMAIL, "HỦY ĐƠN HÀNG", "Lý do: " + lydo);
            TempData["deleteDonHang"] = true;
            db.DONHANGs.Remove(model);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e) { }
            return RedirectToAction("Index");
        }

        public ActionResult Done(int id)
        {
            var comfirmModel = db.DONHANGs.Find(id);
            comfirmModel.TINHTRANGID = 3;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
    }
}