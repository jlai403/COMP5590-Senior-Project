using System;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Tests
{
    public class UserTestHelper
    {
        private static Random Random = new Random();

        public UserViewModel CreateUserWithTestRoles()
        {
            var userSignUpViewModel = new UserSignUpViewModel();
            userSignUpViewModel.FirstName = "Some";
            userSignUpViewModel.LastName = "Dude";
            userSignUpViewModel.Email = string.Format("{0}@someawesomeemailprovider.com", Random.Next(0, 99999));
            userSignUpViewModel.Password = "123456";
            userSignUpViewModel.Roles.Add(RoleTestHelper.FACULTY_MEMBER);
            userSignUpViewModel.Roles.Add(RoleTestHelper.FACULTY_COUNCIL_MEMBER);
            userSignUpViewModel.Roles.Add(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            userSignUpViewModel.Roles.Add(RoleTestHelper.APPC_MEMBER);
            userSignUpViewModel.Roles.Add(RoleTestHelper.GFC_MEMBER);

            FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);

            return FacadeFactory.GetDomainFacade().FindUser(userSignUpViewModel.Email);
        }

        public UserViewModel CreateUser(params string[] roles)
        {
            var userSignUpViewModel = new UserSignUpViewModel();
            userSignUpViewModel.FirstName = "Some";
            userSignUpViewModel.LastName = "Dude";
            userSignUpViewModel.Email = string.Format("{0}@someawesomeemailprovider.com", Random.Next(0, 99999));
            userSignUpViewModel.Password = "123456";
            foreach (var role in roles)
            {
                userSignUpViewModel.Roles.Add(role);
            }

            FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);

            return FacadeFactory.GetDomainFacade().FindUser(userSignUpViewModel.Email);
        }
    }
}