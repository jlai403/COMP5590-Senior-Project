using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Controllers
{
    public class CommentController : Controller
    {
        public void AddComment(string userEmail, CommentInputViewModel commentInputViewModel, WorkflowItemTypes workflowItemType)
        {
            if (commentInputViewModel == null || commentInputViewModel.Text.IsNullOrWhiteSpace()) return;

            FacadeFactory.GetDomainFacade().AddComment(userEmail, commentInputViewModel, workflowItemType);
        }
    }
}