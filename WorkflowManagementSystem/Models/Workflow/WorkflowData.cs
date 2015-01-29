using System.Linq;
using NUnit.Framework;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowData : IEntity
    {
        public virtual int Id { get; set; }
        public virtual ApprovalChainStep ApprovalChainStep { get; set; }
        public virtual WorkflowStatus Status { get; set; }
        public virtual WorkflowData PreviousWorkflowData { get; set; }
        public virtual User User { get; set; }

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

        public bool IsLastWorkflowStep()
        {
            return ApprovalChainStep.ApprovalChain.ApprovalChainSteps.Last().Sequence == ApprovalChainStep.Sequence;
        }
    }
}