using Barbershopmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barbershopmanagement.Controllers
{
    public class OrderHistoryController : Controller
    {
        BarbershopManagementEntities db = new BarbershopManagementEntities();
        // GET: History
        public ActionResult History()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Security");
            }
            var user = (USER)Session["user"];
            List<DONHANG> dsDonHang = db.DONHANGs.Where(m => m.USERID == user.USERID).ToList();
            return View(dsDonHang);
        }

        public ActionResult Delete(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Security");
            }
            var model = db.DONHANGs.Find(id);
            db.DONHANGs.Remove(model);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e) { }
            return RedirectToAction("History");
        }

        public ActionResult Details(int id)
        {
            List<CHITIETDONHANG> chitietDH = db.CHITIETDONHANGs.Where(m => m.DONHANGID == id).ToList();
            return View(chitietDH);
        }
    }
}