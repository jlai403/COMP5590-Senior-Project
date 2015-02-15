using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.Search
{
    public class IndexFactory
    {
        public IIndex InitializeNewIndex(IIndexable indexable)
        {
            if(indexable is WorkflowItem)
            {
                return new WorkflowItemIndex();
            }
            throw new WMSException("Cannot find index for type '{0}'", indexable.GetType());
        }
    }
}