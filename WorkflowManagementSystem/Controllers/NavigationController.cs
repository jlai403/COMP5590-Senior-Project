using System.Web.Mvc;
using WorkflowManagementSystem.Models;

namespace WorkflowManagementSystem.Controllers
{
    public class NavigationController : Controller
    {
        public ActionResult RenderNavigationHeader()
        {
            ViewBag.IsAdmin = FacadeFactory.GetDomainFacade().IsAdmin(User.Identity.Name);
            return PartialView("_NavigationHeaderPartial");
        }
    }
}