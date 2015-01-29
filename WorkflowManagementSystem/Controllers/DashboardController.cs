using System.Collections.Generic;
using System.Web.Mvc;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Programs;

namespace WorkflowManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            var awaitingRequests = FacadeFactory.GetDomainFacade().FindAllProgramRequestsAwaitingForAction(User.Identity.Name);
            var requests = FacadeFactory.GetDomainFacade().FindAllProgramsRequestedByUser(User.Identity.Name);
            var dashboardViewModel = new DashboardViewModel(requests, awaitingRequests);
            return View(dashboardViewModel);
        }
    }

    public class DashboardViewModel
    {
        public List<ProgramViewModel> UserRequests { get; set; }
        public List<ProgramViewModel> ActionableRequests { get; set; }

        public DashboardViewModel(List<ProgramViewModel> userRequests, List<ProgramViewModel> actionableRequests)
        {
            UserRequests = userRequests;
            ActionableRequests = actionableRequests;
        }
    }
}