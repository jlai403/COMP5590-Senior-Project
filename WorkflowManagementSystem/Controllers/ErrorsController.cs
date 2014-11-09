using System;
using System.Web.Mvc;

namespace WorkflowManagementSystem.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult Index(Exception exception)
        {
            return View(exception);
        }
	}
}