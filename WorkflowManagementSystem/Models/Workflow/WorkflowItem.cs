namespace WorkflowManagementSystem.Models.Workflow
{
    public abstract class WorkflowItem : IEntity
    {
        public int Id { get; set; }
        public virtual WorkflowData CurrentWorkflowData { get; set; }
        public abstract string APPROVAL_CHAIN_NAME { get; }
    }
}