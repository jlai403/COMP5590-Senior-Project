using System.Web.Mvc;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Views
{
    public static class WorkflowHtmlHelper
    {
        public static string GetWorkflowStatusDisplay(this HtmlHelper htmlhelper, WorkflowStates state)
        {
            switch (state)
            {
                case WorkflowStates.APPROVED:
                    return "Approved";
                case WorkflowStates.REJECTED:
                    return "Rejected";
                case WorkflowStates.COMPLETED:
                    return "Completed";
                default:
                    return "Pending Approval";
            }
        }
    }
}