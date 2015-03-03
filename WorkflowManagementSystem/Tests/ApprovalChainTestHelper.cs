using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ApprovalChains;

namespace WorkflowManagementSystem.Tests
{
    public class ApprovalChainTestHelper
    {
        public void CreateProgramApprovalChain()
        {
            CreateApprovalChain("Program", RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, RoleTestHelper.FACULTY_COUNCIL_MEMBER, RoleTestHelper.APPC_MEMBER, RoleTestHelper.GFC_MEMBER);
        }

        public void CreateCourseApprovalChain()
        {
            CreateApprovalChain("Course", RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, RoleTestHelper.FACULTY_COUNCIL_MEMBER, RoleTestHelper.APPC_MEMBER, RoleTestHelper.GFC_MEMBER);
        }

        public void CreateApprovalChain(string name, params string[] roles)
        {
            var approvalChainInputViewModel = new ApprovalChainInputViewModel();
            approvalChainInputViewModel.Name = name;
            foreach (var role in roles)
            {
                approvalChainInputViewModel.Roles.Add(role);   
            }

            FacadeFactory.GetDomainFacade().CreateApprovalChain(approvalChainInputViewModel);
        }
    }
}