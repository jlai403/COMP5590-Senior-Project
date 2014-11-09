using System.Web.Mvc;

namespace WorkflowManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Account");

            return RedirectToAction("Index", "Dashboard");
        }
	}
}