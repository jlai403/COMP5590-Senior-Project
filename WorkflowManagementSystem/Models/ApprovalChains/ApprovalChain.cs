using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;
using WorkflowManagementSystem.Models.ErrorHandling;

namespace WorkflowManagementSystem.Models.ApprovalChains
{
    public class ApprovalChain : IEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual List<ApprovalChainStep> ApprovalChainSteps { get; set; }
        public bool Active { get; set; }

        public ApprovalChain()
        {
            ApprovalChainSteps = new List<ApprovalChainStep>();
        }

        public void Update(ApprovalChainInputViewModel approvalChainInputViewModel)
        {
            Type = approvalChainInputViewModel.Type;
            var sequence = 1;
            foreach (var roleName in approvalChainInputViewModel.Roles)
            {
                ApprovalChainSteps.Add(ApprovalChainRepository.CreateApprovalChainStep(this, roleName, sequence++));
            }
            SetActive(approvalChainInputViewModel.Active);

            AssertApprovalChainName();
            AssertApprovalChainHasSteps();
        }

        private void AssertApprovalChainName()
        {
            if (Type.IsNullOrWhiteSpace() || ApprovalChainTypes.TYPES.Contains(Type) == false)
            {
                throw new WMSException("Approval chain type is invalid.");
            }
        }

        private void AssertApprovalChainHasSteps()
        {
            if (ApprovalChainSteps.Count == 0)
            {
                throw new WMSException("Approval chain '{0}' does not have any steps specified.", Type);
            }
        }

        public void SetActive(bool isActive)
        {
            if (isActive)
            {
                var currentActiveApprovalChain = ApprovalChainRepository.FindActiveApprovalChain(Type);

                if (currentActiveApprovalChain != null)
                    currentActiveApprovalChain.SetActive(false);
                Active = true;
            }
            else
                Active = false;
            
        }
    }
}