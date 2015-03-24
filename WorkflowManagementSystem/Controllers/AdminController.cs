using System.Collections.Generic;
using System.Web.Mvc;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FindUsers(string emailPartial)
        {
            var users = FacadeFactory.GetSearchFacade().SearchForUsers(emailPartial);
            return Json(users);
        }

        [HttpPost]
        public void UpdateIsAdmin(string email, bool isAdmin)
        {
            FacadeFactory.GetDomainFacade().UpdateIsAdmin(email, isAdmin);
        }

        public ActionResult CreateApprovalChain()
        {
            return View();
        }
    }
}