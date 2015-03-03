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
            ViewBag.UsersFullName = FacadeFactory.GetDomainFacade().FindUser(User.Identity.Name).DisplayName;
            ViewBag.Semesters = FacadeFactory.GetDomainFacade().FindAllSemesters();
            ViewBag.Faculties = FacadeFactory.GetDomainFacade().FindAllFaculties();
            ViewBag.Disciplines = FacadeFactory.GetDomainFacade().FindAllDisciplines();
            ViewBag.Credits = CourseConstants.AVAILABLE_CREDITS;
            ViewBag.Gradings = CourseConstants.AVAILABLE_GRADINGS;
        }

        public ActionResult Index(string name)
        {
            var courseViewModel = FacadeFactory.GetDomainFacade().FindCourse(name);
            return View(courseViewModel);
        }

        [HttpGet]
        public ActionResult CreateRequest()
        {
            return View();
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