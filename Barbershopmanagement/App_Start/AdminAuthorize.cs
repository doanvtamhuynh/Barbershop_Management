using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Barbershopmanagement.Models;

namespace Barbershopmanagement.App_Start
{
    public class AdminAuthorize:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            USER adminSession = (USER)HttpContext.Current.Session["user"];
            if (adminSession != null)
            {
                if (adminSession.ROLE == "admin")
                {
                    return;
                }
                else
                {
                    filterContext.Result = new RedirectResult("/HomePage/Index");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("/Security/Login");
            }
            
        }

    }
}