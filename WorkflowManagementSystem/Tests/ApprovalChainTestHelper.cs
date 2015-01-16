using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ApprovalChains;

namespace WorkflowManagementSystem.Tests
{
    public class ApprovalChainTestHelper
    {
        public void CreateProgramApprovalChain()
        {
            var approvalChainInputViewModel = new ApprovalChainInputViewModel();
            approvalChainInputViewModel.Name = "Program";
            approvalChainInputViewModel.Roles.Add(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            approvalChainInputViewModel.Roles.Add(RoleTestHelper.FACULTY_COUNCIL_MEMBER);
            approvalChainInputViewModel.Roles.Add(RoleTestHelper.APPC_MEMBER);
            approvalChainInputViewModel.Roles.Add(RoleTestHelper.GFC_MEMBER);

            FacadeFactory.GetDomainFacade().CreateApprovalChain(approvalChainInputViewModel);
        }
    }
}