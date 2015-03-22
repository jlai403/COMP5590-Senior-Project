using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using WorkflowManagementSystem.Models.ErrorHandling;

namespace WorkflowManagementSystem.Models.ApprovalChains
{
    public class ApprovalChain : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<ApprovalChainStep> ApprovalChainSteps { get; set; }
        public bool Active { get; set; }
        public int Version { get; set; }

        public ApprovalChain()
        {
            ApprovalChainSteps = new List<ApprovalChainStep>();
        }

        public void Update(ApprovalChainInputViewModel approvalChainInputViewModel)
        {
            Name = approvalChainInputViewModel.Name;
            var sequence = 1;
            foreach (var roleName in approvalChainInputViewModel.Roles)
            {
                ApprovalChainSteps.Add(ApprovalChainRepository.CreateApprovalChainStep(this, roleName, sequence++));
            }
            Active = approvalChainInputViewModel.Active;

            AssertApprovalChainName();
            AssertApprovalChainHasSteps();
        }

        private void AssertApprovalChainName()
        {
            if (Name.IsNullOrWhiteSpace())
            {
                throw new WMSException("Approval chain name is required.");
            }
        }

        private void AssertApprovalChainHasSteps()
        {
            if (ApprovalChainSteps.Count == 0)
            {
                throw new WMSException("Approval chain '{0}' does not have any steps specified.", Name);
            }
        }
    }
}