using Barbershopmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barbershopmanagement.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        Helpers.Authorization auth = new Helpers.Authorization();
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                USER user = (USER)Session["user"];
                if(auth.isAdmin(user.ROLE) == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "HomePage");
                }
            }
            return Redirect("/Security/Login");
        }
    }
}