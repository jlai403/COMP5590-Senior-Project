using System.Linq;
using System.Web.Mvc;
using WorkflowManagementSystem.Models;

namespace WorkflowManagementSystem.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Search(string keywords, bool async = false)
        {
            var workflowItems = FacadeFactory.GetSearchFacade().SearchWorkflowItems(keywords);
            if(async)
            {
                var resultsJson = workflowItems.Select(x =>
                    new
                    {
                        uri = Url.Action("Index", x.Type.ToString(), new { name = x.Name }),
                        name = x.Name,
                        requester = x.Requester,
                        type = x.Type.ToString()
                    }).ToList();

                return Json(resultsJson);   
            }

            ViewBag.Keywords = keywords;
            return View("SearchResults", workflowItems);
        }

        [HttpPost]
        public ActionResult ProgramNames(string keywords)
        {
            var programNames = FacadeFactory.GetSearchFacade().SearchForProgramNames(keywords);
            return Json(programNames);
        }

        [HttpPost]
        public ActionResult CourseNames(string keywords)
        {
            var courseNames = FacadeFactory.GetSearchFacade().SearchForCourseNames(keywords);
            return Json(courseNames);
        }

        public ActionResult RoleNames(string keywords)
        {
            var roleNames = FacadeFactory.GetSearchFacade().SearchForRoleNames(keywords);
            return Json(roleNames);
        }
    }
}