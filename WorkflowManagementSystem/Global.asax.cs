using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WorkflowManagementSystem.Controllers;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var connectionString = ConfigurationManager.ConnectionStrings["WorkflowManagementSystemDbContext"].ConnectionString;
            DatabaseManager.Instance.Initialize(new WorkflowManagementSystemDbContext(connectionString));

            WebMembershipInitializer.Initialize();

            FacadeFactory.GetDomainFacade().CreateDefaultAdmin();
            SecurityManager.CreateAccount(UserConstants.DEFAULT_ADMIN_EMAIL, UserConstants.DEFAULT_ADMIN_PASSWORD);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            IController errorController = new ErrorsController();
            var routeData = new RouteData();
            routeData.Values.Add("controller","Errors");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("exception", exception);
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            Response.End();
        }
    }
}
