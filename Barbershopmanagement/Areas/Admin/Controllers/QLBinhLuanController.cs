using Barbershopmanagement.App_Start;
using Barbershopmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Barbershopmanagement.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class QLBinhLuanController : Controller
    {
        BarbershopManagementEntities db = new BarbershopManagementEntities();
        // GET: Admin/QLBinhLuan
        public ActionResult BLDichVu()
        {
            List<COMMENT_SERVICES> dsBLDV = db.COMMENT_SERVICES.ToList();
            return View(dsBLDV);
        }

        public ActionResult TimBLDichVu(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("BLDichVu");
            }
            List<COMMENT_SERVICES> dsBLDV = db.COMMENT_SERVICES.Where(m => m.DICHVUID == id).ToList();
            return View(dsBLDV);
        }

        public ActionResult XoaBLDichVu(int id)
        {
            var model = db.COMMENT_SERVICES.Find(id);
            try
            {
                db.COMMENT_SERVICES.Remove(model);
                db.SaveChanges();
            }
            catch (Exception ex) { }
            return RedirectToAction("BLDichVu");
        }

        public ActionResult BLNhanVien()
        {
            List<COMMENT_NHANVIEN> dsBLNV = db.COMMENT_NHANVIEN.ToList();
            return View(dsBLNV);
        }

        public ActionResult TimBLNhanVien(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("BLNhanVien");
            }
            List<COMMENT_NHANVIEN> dsBLNV = db.COMMENT_NHANVIEN.Where(m => m.NHANVIENID == id).ToList();
            return View(dsBLNV);
        }

        public ActionResult XoaBLNhanVien(int id)
        {
            var model = db.COMMENT_NHANVIEN.Find(id);
            try
            {
                db.COMMENT_NHANVIEN.Remove(model);
                db.SaveChanges();
            }
            catch (Exception ex) { }
            return RedirectToAction("BLNhanVien");
        }
    }
}