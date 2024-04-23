using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Barbershopmanagement.Models;

namespace Barbershopmanagement.App_Start
{
    public class Logged : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            USER userSession = (USER)HttpContext.Current.Session["user"];
            if (userSession != null)
            {
                return;
            }
            else
            {
                filterContext.Result = new RedirectResult("/Security/Login");
            }

        }
    }
}