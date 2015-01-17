namespace WorkflowManagementSystem.Models.Workflow
{
    public interface IHaveWorkflow
    {
        WorkflowData CurrentWorkflowData { get; set; }
        string APPROVAL_CHAIN_NAME { get; }
    }
}