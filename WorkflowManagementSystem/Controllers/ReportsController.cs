using System.Web.Mvc;
using RazorPDF;
using WorkflowManagementSystem.Models;

namespace WorkflowManagementSystem.Controllers
{
    public class ReportsController : Controller
    {
        public ActionResult Course(string name)
        {
            var course = FacadeFactory.GetDomainFacade().FindCourse(name);
            return new PdfActionResult(course);
        }
	}
}