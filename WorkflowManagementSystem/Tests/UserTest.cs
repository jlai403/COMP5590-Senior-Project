using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.User;

namespace WorkflowManagementSystem.Tests
{
    public class UserTest : WorkflowManagementSystemTest
    {
        [Test]
        public void CreateUser()
        {
            // assemble
            var userSignUpViewModel = new UserSignUpViewModel();
            userSignUpViewModel.FirstName = "Some";
            userSignUpViewModel.LastName = "Dude";
            userSignUpViewModel.Email = "somedude@someawesomeemailprovider.com";
            userSignUpViewModel.Password = "123456";
   
            // act
            FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);

            // assert
            var users = FacadeFactory.GetDomainFacade().FindAllUsers();
            users.Count.ShouldBeEquivalentTo(1);
            var userViewModel = users.First();
            userViewModel.FirstName.ShouldBeEquivalentTo(userSignUpViewModel.FirstName);
            userViewModel.LastName.ShouldBeEquivalentTo(userSignUpViewModel.LastName);
            userViewModel.Email.ShouldBeEquivalentTo(userSignUpViewModel.Email);
        }
    }
}