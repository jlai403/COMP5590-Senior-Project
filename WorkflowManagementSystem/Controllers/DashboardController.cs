using System.Collections.Generic;
using System.Web.Mvc;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Programs;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            var actionItems = FacadeFactory.GetDomainFacade().FindWorkflowItemsAwaitingForAction(User.Identity.Name);
            var requests = FacadeFactory.GetDomainFacade().FindAllProgramsRequestedByUser(User.Identity.Name);
            var dashboardViewModel = new DashboardViewModel(requests, actionItems);
            return View(dashboardViewModel);
        }
    }

    public class DashboardViewModel
    {
        public List<ProgramViewModel> UserRequests { get; set; }
        public List<ActionableWorkflowItemViewModel> ActionItems { get; set; }

        public DashboardViewModel(List<ProgramViewModel> userRequests, List<ActionableWorkflowItemViewModel> actionItems)
        {
            UserRequests = userRequests;
            ActionItems = actionItems;
        }
    }
}