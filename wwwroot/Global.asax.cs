using ShoppingCartApplication.BusinessLogic;
using ShoppingCartApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShoppingCartApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            if (this.Session.SessionID != null)
            {
                VisitDestroyerRepo visitDestroyerRepo = new VisitDestroyerRepo();
                visitDestroyerRepo.RemoveCartStartupCheck(this.Session.SessionID);
            }

        }

        protected void Session_End(object sender, EventArgs e)
        {
            VisitDestroyerRepo visitDestroyerRepo = new VisitDestroyerRepo();
            visitDestroyerRepo.RemoveCartBySession(this.Session.SessionID);
        }

    }
}
