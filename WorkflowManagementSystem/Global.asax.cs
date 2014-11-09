using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using MyEntityFramework.Transaction;

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
        }
    }
}
