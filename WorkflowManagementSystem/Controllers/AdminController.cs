using System;
using System.Web.Mvc;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.Roles;

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

        [HttpGet]
        public ActionResult CreateApprovalChain()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateApprovalChain(ApprovalChainInputViewModel approvalChainInputViewModel)
        {
            FacadeFactory.GetDomainFacade().CreateApprovalChain(approvalChainInputViewModel);
            return RedirectToAction("Index");
        }

        public ActionResult FindApprovalChains(string approvalChainType)
        {
            var approvalChains = FacadeFactory.GetDomainFacade().FindAllApprovalChains(approvalChainType);
            return Json(approvalChains);
        }

        [HttpPost]
        public void SetActiveApprovalChain(int approvalChainId)
        {
            FacadeFactory.GetDomainFacade().SetActiveApprovalChain(approvalChainId);
        }

        [HttpPost]
        public ActionResult CreateRole(RoleInputViewModel roleInputViewModel)
        {
            string result;
            try
            {
                FacadeFactory.GetDomainFacade().CreateRole(roleInputViewModel);
                result = "Role created";
            }
            catch(Exception e)
            {
                result = e.Message;
            }
            return Json(new {result});
        }
    }
}