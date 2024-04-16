using Barbershopmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarbershopManagement.Controllers
{
    public class ServicesController : System.Web.Mvc.Controller
    {
        BarbershopManagementEntities db = new BarbershopManagementEntities();
        //GET Services
        public ActionResult Services(string search)
        {
            List<DICHVU> dsDichVu = new List<DICHVU>();
            if (search == null || search == "")
            {
                dsDichVu = db.DICHVUs.ToList();

            }
            else
            {
                dsDichVu = db.DICHVUs.Where(m => m.TENDICHVU.ToLower().Contains(search.ToLower())).ToList();
            }
            return View(dsDichVu);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            DICHVU model = db.DICHVUs.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddServiceToCart(int id)
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
                catch (Exception e) { }
            }

            var hadItem = db.CHITIETGIOHANGs.Where(m => m.GIOHANGID == gioHang.GIOHANGID && m.DICHVUID == id).FirstOrDefault();

            if (hadItem == null)
            {
                CHITIETGIOHANG item = new CHITIETGIOHANG();
                item.DICHVUID = id;
                item.GIOHANGID = gioHang.GIOHANGID;
                db.CHITIETGIOHANGs.Add(item);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e) { }
                TempData["AddServiceToCart"] = true;
                return RedirectToAction("Services");
            }
            else
            {
                TempData["AddServiceToCart"] = false;
                return RedirectToAction("Details", new { id });
            }
        }

        [HttpPost]
        public ActionResult Comment(string comment, int id)
        {

            if (Session["userid"] == null)
            {
                return RedirectToAction("Login", "Security");
            }
            int userID = (int)Session["userid"];
            var dongHang = db.CHITIETDONHANGs.Where(m => m.DICHVUID == id && m.DONHANG.USERID == userID && m.DONHANG.TINHTRANGID == 3).FirstOrDefault();
            if (dongHang != null)
            {
                COMMENT_SERVICES cm = new COMMENT_SERVICES();
                cm.USERID = userID;
                cm.DICHVUID = id;
                cm.NOIDUNG = comment;
                db.COMMENT_SERVICES.Add(cm);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e) { }
                return RedirectToAction("Details", new { id });
            }
            else
            {
                TempData["comment_service"] = "Bạn cần phải sử dụng dịch vụ để bình luận.";
                return RedirectToAction("Details", new { id });
            }

        }

    }
}