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
        public virtual WorkflowStates State { get; set; }
        public virtual WorkflowData PreviousWorkflowData { get; set; }
        public virtual User User { get; set; }

        public void Update(ApprovalChainStep approvalChainStep, WorkflowStates states, WorkflowData previousWorkflowData)
        {
            ApprovalChainStep = approvalChainStep;
            State = states;
            PreviousWorkflowData = previousWorkflowData;
        }

        public void UpdateStatus(User user, WorkflowStates state)
        {
            User = user;
            State = state;
        }

        public string GetUserDisplayName()
        {
            return User == null ? "" : User.GetDisplayName();
        }

        public bool IsLastWorkflowStep()
        {
            return ApprovalChainStep.ApprovalChain.ApprovalChainSteps.Max(x => x.Sequence) == ApprovalChainStep.Sequence;
        }
    }
}