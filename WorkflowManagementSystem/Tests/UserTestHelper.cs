using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Tests
{
    public class UserTestHelper
    {
        public UserViewModel CreateUserWithTestRoles()
        {
            new RoleTestHelper().LoadTestRoles();
            
            var userSignUpViewModel = new UserSignUpViewModel();
            userSignUpViewModel.FirstName = "Some";
            userSignUpViewModel.LastName = "Dude";
            userSignUpViewModel.Email = "somedude@someawesomeemailprovider.com";
            userSignUpViewModel.Password = "123456";
            userSignUpViewModel.Roles.Add(RoleTestHelper.FACULTY_MEMBER);
            userSignUpViewModel.Roles.Add(RoleTestHelper.FACULTY_COUNCIL_MEMBER);
            userSignUpViewModel.Roles.Add(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            userSignUpViewModel.Roles.Add(RoleTestHelper.APPC_MEMBER);
            userSignUpViewModel.Roles.Add(RoleTestHelper.GFC_MEMBER);

            FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);

            return FacadeFactory.GetDomainFacade().FindUser(userSignUpViewModel.Email);
        }
    }
}