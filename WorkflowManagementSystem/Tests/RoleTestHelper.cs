using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Roles;

namespace WorkflowManagementSystem.Tests
{
    public class RoleTestHelper
    {
        public const string FACULTY_MEMBER = "Faculty Member";
        public const string FACULTY_CURRICULUMN_MEMBER = "Faculty Curriculumn Member";
        public const string FACULTY_COUNCIL_MEMBER = "Faculty Council Member";
        public const string APPC_MEMBER = "APPC Member";
        public const string GFC_MEMBER = "GFC Member";

        public void CreateTestRoles()
        {
            FacadeFactory.GetDomainFacade().CreateRole(new RoleInputViewModel(FACULTY_MEMBER));
            FacadeFactory.GetDomainFacade().CreateRole(new RoleInputViewModel(FACULTY_CURRICULUMN_MEMBER));
            FacadeFactory.GetDomainFacade().CreateRole(new RoleInputViewModel(FACULTY_COUNCIL_MEMBER));
            FacadeFactory.GetDomainFacade().CreateRole(new RoleInputViewModel(APPC_MEMBER));
            FacadeFactory.GetDomainFacade().CreateRole(new RoleInputViewModel(GFC_MEMBER));
        }
    }
}