using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Barbershopmanagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            // Kiểm tra nếu lỗi là lỗi 404 (không tìm thấy)
            if (exception is HttpException httpException && httpException.GetHttpCode() == 404)
            {
                // Chuyển hướng đến trang lỗi 404
                Response.Redirect("~/HomePage/Index");
            }

            // Clear error on server
            Server.ClearError();
        }
    }
}
