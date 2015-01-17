using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowData : IEntity
    {
        public virtual int Id { get; set; }
        public ApprovalChainStep ApprovalChainStep { get; set; }
        public WorkflowStatus Status { get; set; }
        public WorkflowData PreviousWorkflowData { get; set; }
        public User User { get; set; }

        public void Update(ApprovalChainStep approvalChainStep, WorkflowStatus status, WorkflowData previousWorkflowData)
        {
            ApprovalChainStep = approvalChainStep;
            Status = status;
            PreviousWorkflowData = previousWorkflowData;
        }

        public void UpdateStatus(User user, WorkflowStatus status)
        {
            User = user;
            Status = status;
        }

        public string GetUserDisplayName()
        {
            return User == null ? "" : User.GetDisplayName();
        }
    }
}