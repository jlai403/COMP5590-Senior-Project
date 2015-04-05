using System.Web.Mvc;
using WorkflowManagementSystem.Models;

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

        public ActionResult FindApprovalChains(string approvalChainType)
        {
            var approvalChains = FacadeFactory.GetDomainFacade().FindAllApprovalChains(approvalChainType);
            return Json(approvalChains);
        }
    }
}