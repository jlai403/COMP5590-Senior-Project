using System.Web.Mvc;
using WorkflowManagementSystem.Models;

namespace WorkflowManagementSystem.Controllers
{
    public class ProgramController : Controller
    {
        public ActionResult CreateRequest()
        {
            ViewBag.UsersFullName = FacadeFactory.GetDomainFacade().FindUser(User.Identity.Name).DisplayName;
            ViewBag.Semesters = FacadeFactory.GetDomainFacade().FindAllSemesters();
            ViewBag.Faculties = FacadeFactory.GetDomainFacade().FindAllFaculties();
            ViewBag.Disciplines = FacadeFactory.GetDomainFacade().FindAllDisciplines();
            return View();
        }
    }
}