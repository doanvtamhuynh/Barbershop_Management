using Barbershopmanagement.App_Start;
using Barbershopmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barbershopmanagement.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class QLTaiKhoanController : Controller
    {
        BarbershopManagementEntities db = new BarbershopManagementEntities();
        // GET: Admin/QLTaiKhoan
        public ActionResult Index()
        {
            List<USER> dsTaiKhoan = db.USERS.ToList();
            return View(dsTaiKhoan);
        }

        public ActionResult TimKiem(string role)
        {
            if (role == "all")
            {
                return RedirectToAction("Index");
            }
            List<USER> dsTaiKhoan = db.USERS.Where(m => m.ROLE == role).ToList();
            return View(dsTaiKhoan);
        }
        public ActionResult TaoTaiKhoan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TaoTaiKhoan(USER model, string role, HttpPostedFileBase fileImages)
        {

            if (model.TENDANGNHAP != null && model.TENDANGNHAP.Length > 0 && model.PASSWORD != null && model.PASSWORD.Length > 4 && model.HOTEN != null && model.HOTEN.Length > 0
                && model.SODIENTHOAI != null && model.SODIENTHOAI.Length == 10 && model.EMAIL != null)
            {
                var findUser = db.USERS.FirstOrDefault(m => m.TENDANGNHAP == model.TENDANGNHAP);

                if (findUser == null)
                {
                    USER newUser = new USER();
                    newUser.TENDANGNHAP = model.TENDANGNHAP;
                    newUser.PASSWORD = model.PASSWORD;
                    newUser.HOTEN = model.HOTEN;
                    newUser.SODIENTHOAI = model.SODIENTHOAI;
                    newUser.EMAIL = model.EMAIL;
                    newUser.DIACHI = model.DIACHI;
                    newUser.ROLE = role;
                    if (fileImages != null)
                    {
                        string rootFolder = Server.MapPath("/Content/images/avatar/");
                        string pathImg = rootFolder + fileImages.FileName;
                        fileImages.SaveAs(pathImg);
                        newUser.URLHINHANH = fileImages.FileName;
                    }
                    else
                    {
                        newUser.URLHINHANH = "default-avatar.jpg";
                    }

                    db.USERS.Add(newUser);
                    if(newUser.ROLE == "nhanvien")
                    {
                        NHANVIEN newNV = new NHANVIEN();
                        newNV.USERID = newUser.USERID;
                        db.NHANVIENs.Add(newNV);
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) { }
                    TempData["addTaiKhoan"] = "Tạo tài khoản thành công!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["addTaiKhoan"] = "Tên đăng nhập đã tồn tại!";
                    return RedirectToAction("TaoTaiKhoan");
                }
            }
            else
            {
                TempData["addTaiKhoan"] = "Nhập thông tin không chính xác!";
                return RedirectToAction("TaoTaiKhoan");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.USERS.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveEdit(USER model, string role, HttpPostedFileBase fileImages)
        {
            if (model.TENDANGNHAP != null && model.TENDANGNHAP.Length > 0 && model.PASSWORD != null && model.PASSWORD.Length > 4 && model.HOTEN != null && model.HOTEN.Length > 0
                && model.SODIENTHOAI != null && model.SODIENTHOAI.Length == 10 && model.EMAIL != null)
            {
                var updateModel = db.USERS.Find(model.USERID);
                if (updateModel.TENDANGNHAP != model.TENDANGNHAP)
                {
                    var findUser = db.USERS.Where(m => m.TENDANGNHAP == model.TENDANGNHAP).FirstOrDefault();
                    if (findUser == null)
                    {
                        updateModel.TENDANGNHAP = model.TENDANGNHAP;
                        updateModel.PASSWORD = model.PASSWORD;
                        updateModel.HOTEN = model.HOTEN;
                        updateModel.SODIENTHOAI = model.SODIENTHOAI;
                        updateModel.EMAIL = model.EMAIL;
                        updateModel.ROLE = role;
                        updateModel.DIACHI = model.DIACHI;
                        if (fileImages != null && fileImages.ContentLength > 0)
                        {
                            string rootFolder = Server.MapPath("/Content/Images/avatar/");
                            string pathImg = rootFolder + fileImages.FileName;
                            fileImages.SaveAs(pathImg);
                            updateModel.URLHINHANH = fileImages.FileName;
                        }
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception e) { }
                        TempData["edituser"] = true;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["edituser"] = "Tên đăng nhập đã tồn tại!";
                        return RedirectToAction("Edit", new { id = model.USERID });
                    }
                }
                else
                {
                    updateModel.TENDANGNHAP = model.TENDANGNHAP;
                    updateModel.PASSWORD = model.PASSWORD;
                    updateModel.HOTEN = model.HOTEN;
                    updateModel.SODIENTHOAI = model.SODIENTHOAI;
                    updateModel.EMAIL = model.EMAIL;
                    updateModel.ROLE = role;
                    updateModel.DIACHI = model.DIACHI;
                    if (fileImages != null && fileImages.ContentLength > 0)
                    {
                        string rootFolder = Server.MapPath("/Content/Images/avatar");
                        string pathImg = rootFolder + fileImages.FileName;
                        fileImages.SaveAs(pathImg);
                        updateModel.URLHINHANH = fileImages.FileName;
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) { }
                    TempData["edituser"] = true;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["edituser"] = "Nhập thông tin không hợp lệ!";
                return RedirectToAction("Edit", new { id = model.USERID });
            }
        }

        [HttpGet]
        public ActionResult Xoa(int id)
        {
            var model = db.USERS.Find(id);
            if(model.ROLE == "nhanvien")
            {
                var nhanVien = db.NHANVIENs.First(m => m.USERID == model.USERID);
                db.NHANVIENs.Remove(nhanVien);
            }
            db.USERS.Remove(model);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e) { }

            return RedirectToAction("Index");
        }

       
    }
}