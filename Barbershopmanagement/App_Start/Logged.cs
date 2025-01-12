using Barbershopmanagement.Models;
using System.Web;
using System.Web.Mvc;

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