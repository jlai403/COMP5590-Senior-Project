using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.ApprovalChains
{
    public class ApprovalChainViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ApprovalChainStepViewModel> ApprovalChainSteps { get; set; }
        public bool IsActive { get; set; }

        public ApprovalChainViewModel()
        {
            ApprovalChainSteps = new List<ApprovalChainStepViewModel>();
        }
    }
}