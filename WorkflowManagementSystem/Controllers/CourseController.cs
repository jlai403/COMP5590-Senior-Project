using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Course;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public ActionResult Index(string name)
        {
            var courseViewModel = FacadeFactory.GetDomainFacade().FindCourse(name);
            return View(courseViewModel);
        }

        [HttpGet]
        public ActionResult CreateRequest()
        {
            SetViewBagsForCreateRequest();
            return View();
        }

        private void SetViewBagsForCreateRequest()
        {
            ViewBag.UsersFullName = FacadeFactory.GetDomainFacade().FindUser(User.Identity.Name).DisplayName;
            ViewBag.Semesters = FacadeFactory.GetDomainFacade().FindAllSemesters();
            ViewBag.Faculties = FacadeFactory.GetDomainFacade().FindAllFaculties();
            ViewBag.Disciplines = FacadeFactory.GetDomainFacade().FindAllDisciplines();
            ViewBag.Credits = CourseConstants.AVAILABLE_CREDITS;
            ViewBag.Gradings = CourseConstants.AVAILABLE_GRADINGS;
        }

        [HttpGet]
        public ActionResult UpdateStatus(string name)
        {
            var course = FacadeFactory.GetDomainFacade().FindCourse(name);
            return View(course);
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
                    return RedirectToAction("Reject", new { workflowItemName });
                default:
                    TempData.Remove("commentInputViewModel");
                    TempData.Remove("files");
                    throw new WMSException("Unknown/unimplemented action '{0}", submit);
            }
        }

        public ActionResult Approve(string workflowItemName)
        {
            if (FacadeFactory.GetDomainFacade().IsWorkflowItemCurrentlyOnLastWorkflowStep(workflowItemName, WorkflowItemTypes.Course))
                FacadeFactory.GetDomainFacade().CompleteWorkflowItem(User.Identity.Name, workflowItemName, WorkflowItemTypes.Course);
            else
                FacadeFactory.GetDomainFacade().ApproveWorkflowItem(User.Identity.Name, workflowItemName, WorkflowItemTypes.Course);

            new CommentController().AddComment(User.Identity.Name, (CommentInputViewModel)TempData["commentInputViewModel"], WorkflowItemTypes.Course);
            new FileController().UploadAttachments(User.Identity.Name, workflowItemName, (List<HttpPostedFileBase>)TempData["files"], WorkflowItemTypes.Course);

            return RedirectToAction("Index", new { name = workflowItemName });
        }

        public ActionResult Reject(string workflowItemName)
        {
            FacadeFactory.GetDomainFacade().RejectWorkflowItem(User.Identity.Name, workflowItemName, WorkflowItemTypes.Course);

            new CommentController().AddComment(User.Identity.Name, (CommentInputViewModel)TempData["commentInputViewModel"], WorkflowItemTypes.Course);
            new FileController().UploadAttachments(User.Identity.Name, workflowItemName, (List<HttpPostedFileBase>)TempData["files"], WorkflowItemTypes.Course);

            return RedirectToAction("Index", new { name = workflowItemName });
        }

        [HttpPost]
        public ActionResult CreateRequest(CourseRequestInputViewModel courseRequestInputViewModel, List<HttpPostedFileBase> files)
        {
            try
            {
                FacadeFactory.GetDomainFacade().CreateCourseRequest(User.Identity.Name, courseRequestInputViewModel);
                new FileController().UploadAttachments(User.Identity.Name, courseRequestInputViewModel.Name, files, WorkflowItemTypes.Course);
            }
            catch (WMSException e)
            {
                SetViewBagsForCreateRequest();
                ViewBag.ErrorMessage = e.Message;
                return View("CreateRequest", courseRequestInputViewModel);
            }
            return RedirectToAction("Requested", "Course", new { name = courseRequestInputViewModel.Name });
        }

        public ActionResult Requested(string name)
        {
            ViewBag.CourseName = name;
            return View();
        }

        public ActionResult AddComment(CommentInputViewModel commentInputViewModel)
        {
            var commentViewModel = FacadeFactory.GetDomainFacade().AddComment(User.Identity.Name, commentInputViewModel, WorkflowItemTypes.Course);
            return PartialView("_WorkflowItemCommentPartial", commentViewModel);
        }
    }
}