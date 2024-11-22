using Barbershopmanagement.App_Start;
using Barbershopmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarbershopManagement.Controllers
{
    [Logged]
    public class ProfileController : Controller
    {
        BarbershopManagementEntities db = new BarbershopManagementEntities();
        public ActionResult Profile()
        {
                USER model = (USER)Session["user"];
                return View(model);
        }

        public ActionResult EditProfile()
        {
            USER model = (USER)Session["user"];
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveEdit(USER model, HttpPostedFileBase fileImages)
        {
            if (model.TENDANGNHAP != null && model.TENDANGNHAP.Length > 0 && model.PASSWORD != null && model.PASSWORD.Length > 4
                && model.HOTEN != null && model.HOTEN.Length > 0 && model.SODIENTHOAI != null && model.SODIENTHOAI.Length == 10)
            {
                var updateModel = db.USERS.Find(model.USERID);

                if (model.TENDANGNHAP != updateModel.TENDANGNHAP)
                {
                    var findUser = db.USERS.SingleOrDefault(m => m.TENDANGNHAP == model.TENDANGNHAP);
                    if (findUser == null)
                    {
                        if (fileImages != null && fileImages.ContentLength > 0)
                        {
                            string rootFolder = Server.MapPath("/Content/images/avatar/");
                            string pathImg = rootFolder + fileImages.FileName;
                            fileImages.SaveAs(pathImg);
                            updateModel.URLHINHANH = fileImages.FileName;
                        }
                        updateModel.HOTEN = model.HOTEN;
                        updateModel.TENDANGNHAP = model.TENDANGNHAP;
                        updateModel.PASSWORD = model.PASSWORD;
                        updateModel.SODIENTHOAI = model.SODIENTHOAI;
                        updateModel.EMAIL = model.EMAIL;
                        updateModel.DIACHI = model.DIACHI;
                        try
                        {
                            db.SaveChanges();
                            Session["user"] = updateModel;
                        }
                        catch (Exception e) { }
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        TempData["EditProfile"] = true;
                        return RedirectToAction("EditProfile", new { id = model.USERID });
                    }
                }
                else
                {
                    TempData["EditProfile"] = null;
                    if (fileImages != null && fileImages.ContentLength > 0)
                    {
                        string rootFolder = Server.MapPath("/Content/images/avatar/");
                        string pathImg = rootFolder + fileImages.FileName;
                        fileImages.SaveAs(pathImg);
                        updateModel.URLHINHANH = fileImages.FileName;
                    }
                    updateModel.HOTEN = model.HOTEN;
                    updateModel.TENDANGNHAP = model.TENDANGNHAP;
                    updateModel.PASSWORD = model.PASSWORD;
                    updateModel.SODIENTHOAI = model.SODIENTHOAI;
                    updateModel.EMAIL = model.EMAIL;
                    updateModel.DIACHI = model.DIACHI;
                    try
                    {
                        db.SaveChanges();
                        Session["user"] = updateModel;
                    }
                    catch (Exception e) { }
                    return RedirectToAction("Profile");
                }
            }
            else
            {
                TempData["EditProfile"] = true;
                return RedirectToAction("EditProfile", new { id = model.USERID });
            }
        }


    }
}