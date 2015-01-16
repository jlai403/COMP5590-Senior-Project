using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.ApprovalChains
{
    public class ApprovalChainViewModel
    {
        public string Name { get; set; }
        public List<ApprovalChainStepViewModel> ApprovalChainSteps { get; set; }

        public ApprovalChainViewModel()
        {
            ApprovalChainSteps = new List<ApprovalChainStepViewModel>();
        }
    }
}