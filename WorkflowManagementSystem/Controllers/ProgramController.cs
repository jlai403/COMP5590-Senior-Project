using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Programs;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Controllers
{
    public class ProgramController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        private void SetViewBagsForCreateRequest()
        {
            ViewBag.UsersFullName = FacadeFactory.GetDomainFacade().FindUser(User.Identity.Name).DisplayName;
            ViewBag.Semesters = FacadeFactory.GetDomainFacade().FindAllSemesters();
            ViewBag.Faculties = FacadeFactory.GetDomainFacade().FindAllFaculties();
        }

        [HttpGet]
        public ActionResult CreateRequest()
        {
            SetViewBagsForCreateRequest();
            return View();
        }

        [HttpPost]
        public ActionResult CreateRequest(ProgramRequestInputViewModel programRequestInputViewModel, List<HttpPostedFileBase> files)
        {
            try
            {
                FacadeFactory.GetDomainFacade().CreateProgramRequest(User.Identity.Name, programRequestInputViewModel);
                
                new FileController().UploadAttachments(User.Identity.Name, programRequestInputViewModel.Name, files, WorkflowItemTypes.Program);
            }
            catch (WMSException e)
            {
                SetViewBagsForCreateRequest();
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

        public ActionResult Index(string name)
        {
            var program = FacadeFactory.GetDomainFacade().FindProgram(name);
            return View(program);
        }

        [HttpGet]
        public ActionResult UpdateStatus(string name)
        {
            var program = FacadeFactory.GetDomainFacade().FindProgram(name);
            return View(program);
        }

        [HttpPost]
        public ActionResult UpdateStatus(string workflowItemName, string submit, CommentInputViewModel commentInputViewModel, List<HttpPostedFileBase> files)
        {
            TempData["commentInputViewModel"] = commentInputViewModel;
            TempData["files"] = files;
            switch (submit)
            {
                case "approve":
                    return RedirectToAction("Approve", new { workflowItemName });
                case "reject":
                    return RedirectToAction("Reject", new { workflowItemName});
                default:
                    TempData.Remove("commentInputViewModel");
                    TempData.Remove("files");
                    throw new WMSException("Unknown/unimplemented action '{0}", submit);
            }
        }

        public ActionResult Approve(string workflowItemName)
        {
            if (FacadeFactory.GetDomainFacade().IsWorkflowItemCurrentlyOnLastWorkflowStep(workflowItemName, WorkflowItemTypes.Program))
                FacadeFactory.GetDomainFacade().CompleteWorkflowItem(User.Identity.Name, workflowItemName, WorkflowItemTypes.Program);
            else
                FacadeFactory.GetDomainFacade().ApproveWorkflowItem(User.Identity.Name, workflowItemName, WorkflowItemTypes.Program);

            new CommentController().AddComment(User.Identity.Name, (CommentInputViewModel)TempData["commentInputViewModel"], WorkflowItemTypes.Program);
            new FileController().UploadAttachments(User.Identity.Name, workflowItemName, (List<HttpPostedFileBase>)TempData["files"], WorkflowItemTypes.Program);

            return RedirectToAction("Index", new { name = workflowItemName });
        }

        public ActionResult Reject(string workflowItemName)
        {
            FacadeFactory.GetDomainFacade().RejectWorkflowItem(User.Identity.Name, workflowItemName, WorkflowItemTypes.Program);

            new CommentController().AddComment(User.Identity.Name, (CommentInputViewModel)TempData["commentInputViewModel"], WorkflowItemTypes.Program);
            new FileController().UploadAttachments(User.Identity.Name, workflowItemName, (List<HttpPostedFileBase>)TempData["files"], WorkflowItemTypes.Program);

            return RedirectToAction("Index", new { name = workflowItemName });
        }

        public ActionResult AddComment(CommentInputViewModel commentInputViewModel)
        {
            var commentViewModel = FacadeFactory.GetDomainFacade().AddComment(User.Identity.Name, commentInputViewModel, WorkflowItemTypes.Program);
            return PartialView("_WorkflowItemCommentPartial", commentViewModel);
        }
    }
}