using Barbershopmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarbershopManagement.Controllers
{
    public class CartController : Controller
    {
        BarbershopManagementEntities db = new BarbershopManagementEntities();
        // GET: Cart
        public ActionResult Cart()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Login", "Security");
            }
            int userID = (int)Session["userid"];
            var gioHang = db.GIOHANGs.Where(m => m.USERID == userID).FirstOrDefault();
            if (gioHang == null)
            {
                GIOHANG newGioHang = new GIOHANG();
                newGioHang.USERID = userID;
                db.GIOHANGs.Add(newGioHang);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex) { }
            }
            List<CHITIETGIOHANG> chiTietGioHang = db.CHITIETGIOHANGs.Where(m => m.GIOHANGID == gioHang.GIOHANGID).ToList();
            return View(chiTietGioHang);
        }

        [HttpPost]
        public ActionResult RemoveItem(int id)
        {
            int userID = (int)Session["userid"];
            var gioHang = db.GIOHANGs.Where(m => m.USERID == userID).FirstOrDefault();
            var item = db.CHITIETGIOHANGs.Where(m => m.GIOHANGID == gioHang.GIOHANGID && m.DICHVUID == id);
            db.CHITIETGIOHANGs.RemoveRange(item);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex) { }
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public ActionResult Order(int nhanvienid, DateTime datetime)
        {
            int userID = (int)Session["userid"];
            GIOHANG gioHang = db.GIOHANGs.Where(m => m.USERID == userID).SingleOrDefault();
            List<CHITIETGIOHANG> chitietGH = db.CHITIETGIOHANGs.Where(m => m.GIOHANGID == gioHang.GIOHANGID).ToList();
            if (chitietGH.Count == 0)
            {
                return RedirectToAction("Cart");
            }

            DONHANG donHang = new DONHANG();
            donHang.USERID = userID;
            donHang.NHANVIENID = nhanvienid;
            donHang.THOIGIAN = datetime;
            donHang.TINHTRANGID = 1;
            db.DONHANGs.Add(donHang);


            foreach (var item in chitietGH)
            {
                CHITIETDONHANG chitietDH = new CHITIETDONHANG();
                chitietDH.DONHANGID = donHang.DONHANGID;
                chitietDH.DICHVUID = item.DICHVUID;
                db.CHITIETDONHANGs.Add(chitietDH);
                db.CHITIETGIOHANGs.Remove(item);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex) { }
            return RedirectToAction("History", "History");
        }
    }
}