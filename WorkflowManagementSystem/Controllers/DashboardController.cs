using System.Collections.Generic;
using System.Web.Mvc;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            var actionItems = FacadeFactory.GetDomainFacade().FindWorkflowItemsAwaitingForAction(User.Identity.Name);
            var requests = FacadeFactory.GetDomainFacade().FindWorkflowItemsRequestedByUser(User.Identity.Name);
            var dashboardViewModel = new DashboardViewModel(requests, actionItems);
            return View(dashboardViewModel);
        }
    }

    public class DashboardViewModel
    {
        public List<WorkflowItemViewModel> UserRequests { get; set; }
        public List<WorkflowItemViewModel> ActionItems { get; set; }

        public DashboardViewModel(List<WorkflowItemViewModel> userRequests, List<WorkflowItemViewModel> actionItems)
        {
            UserRequests = userRequests;
            ActionItems = actionItems;
        }
    }
}