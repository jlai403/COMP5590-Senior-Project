using System.Web.Mvc;

namespace WorkflowManagementSystem.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Search()
        {
            return View("SearchResults");
        }
    }
}