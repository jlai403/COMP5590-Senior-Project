using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.Search
{
    public class WorkflowItemIndex : IIndex
    {
        public int Id { get; set; }
        public virtual WorkflowItem Entity { get; set; }
        public virtual IndexKey Key { get; set; }

        public void Update(IndexKey indexKey, IIndexable indexedEntity)
        {
            Key = indexKey;
            Entity = indexedEntity as WorkflowItem;
        }
    }
}