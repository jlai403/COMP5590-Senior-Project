using WorkflowManagementSystem.Models.ApprovalChains;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowData : IEntity
    {
        public virtual int Id { get; set; }
        public ApprovalChainStep ApprovalChainStep { get; set; }
        public WorkflowStatus Status { get; set; }
        public WorkflowData PreviousWorkflowData { get; set; }

        public void Update(ApprovalChainStep approvalChainStep, WorkflowStatus status, WorkflowData previousWorkflowData)
        {
            ApprovalChainStep = approvalChainStep;
            Status = status;
            PreviousWorkflowData = previousWorkflowData;
        }

    }
}