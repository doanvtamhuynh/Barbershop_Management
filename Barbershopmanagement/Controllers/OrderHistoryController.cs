using Barbershopmanagement.App_Start;
using Barbershopmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Barbershopmanagement.Controllers
{
    [Logged]
    public class OrderHistoryController : Controller
    {
        BarbershopManagementEntities db = new BarbershopManagementEntities();
        // GET: History
        public ActionResult History()
        {
            var user = (USER)Session["user"];
            List<DONHANG> dsDonHang = db.DONHANGs.Where(m => m.USERID == user.USERID).OrderBy(m => m.TINHTRANGID).ToList();
            return View(dsDonHang);
        }

        public ActionResult Delete(int id)
        {
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