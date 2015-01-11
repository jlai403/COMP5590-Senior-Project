using System.Collections.Generic;
using System.Linq;

namespace WorkflowManagementSystem.Models.ApprovalChains
{
    public class ApprovalChainAssembler
    {
        public ApprovalChain ApprovalChain { get; set; }

        public ApprovalChainAssembler(ApprovalChain approvalChain)
        {
            ApprovalChain = approvalChain;
        }

        public List<ApprovalChainStepViewModel> Assemble()
        {
            return ApprovalChain.ApprovalChainSteps.Select(approvalChainStep => AssembleApprovalChainStep(approvalChainStep)).ToList();
        }

        private ApprovalChainStepViewModel AssembleApprovalChainStep(ApprovalChainStep approvalChainStep)
        {
            var approvalChainStepViewModel = new ApprovalChainStepViewModel();
            approvalChainStepViewModel.RoleName = approvalChainStep.Role.Name;
            approvalChainStepViewModel.Sequence = approvalChainStep.Sequence;
            return approvalChainStepViewModel;
        }
    }
}