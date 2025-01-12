using Barbershopmanagement.App_Start;
using Barbershopmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarbershopManagement.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class QLDichVuController : Controller
    {

        // GET: Admin/QLDichVu
        BarbershopManagementEntities db = new BarbershopManagementEntities();
        public ActionResult Index()
        {
            List<DICHVU> dsDichVu = db.DICHVUs.ToList();
            return View(dsDichVu);

        }

        public ActionResult ThemDichVu()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemDichVu(DICHVU model, HttpPostedFileBase fileImages)
        {
            if (model.TENDICHVU != null && model.TENDICHVU.Length > 0 && model.GIADICHVU > 0 && model.THOIGIANTHUCHIEN > 0 && fileImages != null && fileImages.ContentLength > 0)
            {
                DICHVU dv = new DICHVU();
                string rootFolder = Server.MapPath("/Content/images/serviceimg/");
                string pathImg = rootFolder + fileImages.FileName;
                fileImages.SaveAs(pathImg);
                dv.URLHINHANH = fileImages.FileName;
                dv.TENDICHVU = model.TENDICHVU;
                dv.THOIGIANTHUCHIEN = model.THOIGIANTHUCHIEN;
                dv.GIADICHVU = model.GIADICHVU;
                dv.MOTA = model.MOTA;
                db.DICHVUs.Add(dv);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e) { }
                TempData["addservice"] = "Thêm dịch vụ thành công.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["addservice"] = "Nhập thông tin không hợp lệ.";
                return RedirectToAction("ThemDichVu");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.DICHVUs.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveEdit(DICHVU model, HttpPostedFileBase fileImages)
        {
            if (model.TENDICHVU != null && model.TENDICHVU.Length > 0 && model.GIADICHVU > 0 && model.THOIGIANTHUCHIEN > 0)
            {
                var updateModel = db.DICHVUs.Find(model.DICHVUID);
                if (fileImages != null && fileImages.ContentLength > 0)
                {
                    string rootFolder = Server.MapPath("/Content/images/serviceimg/");
                    string pathImg = rootFolder + fileImages.FileName;
                    fileImages.SaveAs(pathImg);
                    updateModel.URLHINHANH = fileImages.FileName;
                }
                updateModel.TENDICHVU = model.TENDICHVU;
                updateModel.THOIGIANTHUCHIEN = model.THOIGIANTHUCHIEN;
                updateModel.GIADICHVU = model.GIADICHVU;
                updateModel.MOTA = model.MOTA;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e) { }
                TempData["editservice"] = "Sửa dịch vụ thành công.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["editservice"] = "Nhập thông tin không hợp lệ.";
                return RedirectToAction("Edit", new { id = model.DICHVUID });
            }
        }

        [HttpGet]
        public ActionResult Xoa(int id)
        {
            var model = db.DICHVUs.Find(id);

            db.DICHVUs.Remove(model);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e) { }
            return RedirectToAction("Index");
        }
    }
}