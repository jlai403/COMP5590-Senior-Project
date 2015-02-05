using System;
using System.Web.Mvc;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Programs;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Controllers
{
    public class ProgramController : Controller
    {
        [HttpGet]
        public ActionResult CreateRequest()
        {
            ViewBag.UsersFullName = FacadeFactory.GetDomainFacade().FindUser(User.Identity.Name).DisplayName;
            ViewBag.Semesters = FacadeFactory.GetDomainFacade().FindAllSemesters();
            ViewBag.Faculties = FacadeFactory.GetDomainFacade().FindAllFaculties();
            ViewBag.Disciplines = FacadeFactory.GetDomainFacade().FindAllDisciplines();
            return View();
        }

        [HttpPost]
        public ActionResult CreateRequest(ProgramRequestInputViewModel programRequestInputViewModel)
        {
            try
            {
                FacadeFactory.GetDomainFacade().CreateProgramRequest(User.Identity.Name, programRequestInputViewModel);
            }
            catch (WMSException e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View("CreateRequest", programRequestInputViewModel);
            }
            return RedirectToAction("Requested", "Program", new { name = programRequestInputViewModel.Name });
        }

        public ActionResult Requested(String name)
        {
            ViewBag.ProgramName = name;
            return View("Requested");
        }

        public ActionResult Summary(string name)
        {
            var program = FacadeFactory.GetDomainFacade().FindProgram(name);
            return View(program);
        }

        public ActionResult UpdateStatus(string name)
        {
            var program = FacadeFactory.GetDomainFacade().FindProgram(name);
            return View(program);
        }

        public ActionResult Approve(string name)
        {
            if (FacadeFactory.GetDomainFacade().IsProgramRequestCurrentlyOnLastWorkflowStep(name))
                FacadeFactory.GetDomainFacade().CompleteProgramRequest(User.Identity.Name, name);
            else
                FacadeFactory.GetDomainFacade().ApproveProgramRequest(User.Identity.Name, name);
            return RedirectToAction("Summary", new {name});
        }

        public ActionResult Reject(string name)
        {
            FacadeFactory.GetDomainFacade().RejectProgramRequest(User.Identity.Name, name);
            return RedirectToAction("Summary", new { name });
        }

        public ActionResult AddComment(CommentInputViewModel commentInputViewModel)
        {
            var commentViewModel = FacadeFactory.GetDomainFacade().AddComment(User.Identity.Name, commentInputViewModel, WorkflowItemTypes.Program);
            return PartialView("_WorkflowItemCommentPartial", commentViewModel);
        }
    }
}