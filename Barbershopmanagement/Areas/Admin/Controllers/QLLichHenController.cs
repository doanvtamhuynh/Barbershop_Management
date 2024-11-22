using Barbershopmanagement.App_Start;
using Barbershopmanagement.Models;
using BarbershopManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barbershopmanagement.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class QLLichHenController : Controller
    {
        BarbershopManagementEntities db = new BarbershopManagementEntities();
        // GET: Admin/QLLichHen
        public ActionResult Index()
        {
            List<DONHANG> dsDonhang = db.DONHANGs.OrderBy(m => m.TINHTRANGID).ToList();
            return View(dsDonhang);
        }

        public ActionResult TimKiem(string tinhtrang)
        {
            int tt = int.Parse(tinhtrang);
            if (tt == 0)
            {
                return RedirectToAction("Index");
            }
            List<DONHANG> dsDonhang = db.DONHANGs.Where(m => m.TINHTRANGID == tt).ToList();
            return View(dsDonhang);
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
            }
            catch (Exception ex) { }
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