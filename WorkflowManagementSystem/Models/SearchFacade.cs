using System.Collections.Generic;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.Search;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models
{
    public class SearchFacade
    {
        public List<WorkflowItemViewModel> SearchWorkflowItems(string searchQuery)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var searchResults = InvertedIndexRepository.SearchWorkflowItem(searchQuery);
                return WorkflowAssembler.AssembleWorkflowItems(searchResults);
            });
        }

        public HashSet<string> SearchForProgramNames(string keywords)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                return InvertedIndexRepository.SearchForProgramNames(keywords);
            });
        }

        public HashSet<string> SearchForCourseNames(string keywords)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                return InvertedIndexRepository.SearchForCourseNames(keywords);
            });
        }
    }
}