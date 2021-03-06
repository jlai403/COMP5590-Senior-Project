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

        public List<ApprovalChainStepViewModel> AssembleApprovalChainSteps()
        {
            return ApprovalChain.ApprovalChainSteps.Select(AssembleApprovalChainStep).ToList();
        }

        private ApprovalChainStepViewModel AssembleApprovalChainStep(ApprovalChainStep approvalChainStep)
        {
            var approvalChainStepViewModel = new ApprovalChainStepViewModel();
            approvalChainStepViewModel.RoleName = approvalChainStep.Role.Name;
            approvalChainStepViewModel.Sequence = approvalChainStep.Sequence;
            return approvalChainStepViewModel;
        }

        public static List<ApprovalChainViewModel> AssembleAll(List<ApprovalChain> approvalChains)
        {
            return approvalChains.Select(approvalChain => new ApprovalChainAssembler(approvalChain).AssembleApprovalChainViewModel()).ToList();
        }

        public ApprovalChainViewModel AssembleApprovalChainViewModel()
        {
            if (ApprovalChain == null) return null;

            var approvalChainViewModel = new ApprovalChainViewModel();
            approvalChainViewModel.Id = ApprovalChain.Id;
            approvalChainViewModel.Type = ApprovalChain.Type;
            approvalChainViewModel.ApprovalChainSteps = AssembleApprovalChainSteps();
            approvalChainViewModel.IsActive = ApprovalChain.Active;
            return approvalChainViewModel;
        }
    }
}