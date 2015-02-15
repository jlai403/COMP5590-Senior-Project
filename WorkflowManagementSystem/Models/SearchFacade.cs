using System.Collections.Generic;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.Search;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models
{
    public class SearchFacade
    {
        public List<WorkflowItemViewModel> Search(string searchQuery)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var searchResults = SearchRepository.SearchWorkflowItem(searchQuery);
                return WorkflowAssembler.AssembleWorkflowItems(searchResults);
            });
        }
    }
}