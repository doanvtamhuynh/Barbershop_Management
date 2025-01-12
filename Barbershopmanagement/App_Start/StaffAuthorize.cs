using Barbershopmanagement.Models;
using System.Web;
using System.Web.Mvc;

namespace Barbershopmanagement.App_Start
{
    public class StaffAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            USER adminSession = (USER)HttpContext.Current.Session["user"];
            if (adminSession != null)
            {
                if (adminSession.ROLE == "nhanvien")
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