using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Tests
{
    public class UserTest : WorkflowManagementSystemTest
    {
        [Test]
        public void CreateUser()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();

            var userSignUpViewModel = new UserSignUpViewModel();
            userSignUpViewModel.FirstName = "Some";
            userSignUpViewModel.LastName = "Dude";
            userSignUpViewModel.Email = "somedude@someawesomeemailprovider.com";
            userSignUpViewModel.Password = "123456";
            userSignUpViewModel.Roles.Add(RoleTestHelper.FACULTY_MEMBER);
   
            // act
            FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);

            // assert
            var users = FacadeFactory.GetDomainFacade().FindAllUsers();
            users.Count.ShouldBeEquivalentTo(1);
            var userViewModel = users.First();
            userViewModel.FirstName.ShouldBeEquivalentTo(userSignUpViewModel.FirstName);
            userViewModel.LastName.ShouldBeEquivalentTo(userSignUpViewModel.LastName);
            userViewModel.Email.ShouldBeEquivalentTo(userSignUpViewModel.Email);
            userViewModel.Roles.Count.ShouldBeEquivalentTo(1);
            userViewModel.Roles.Should().Contain(RoleTestHelper.FACULTY_MEMBER);
            userViewModel.DisplayName.ShouldBeEquivalentTo("Some Dude");
            userViewModel.IsAdmin.Should().BeFalse();
        }

        [Test]
        public void IsAdmin()
        {
            // assemble
            FacadeFactory.GetDomainFacade().CreateDefaultAdmin();

            // act
            var isAdmin = FacadeFactory.GetDomainFacade().IsAdmin(UserConstants.DEFAULT_ADMIN_EMAIL);

            // assert
            isAdmin.Should().BeTrue();
        }

        [Test]
        public void IsAdmin_RegularUser()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();

            var userSignUpViewModel = new UserSignUpViewModel();
            userSignUpViewModel.FirstName = "Some";
            userSignUpViewModel.LastName = "Dude";
            userSignUpViewModel.Email = "somedude@someawesomeemailprovider.com";
            userSignUpViewModel.Password = "123456";
            userSignUpViewModel.Roles.Add(RoleTestHelper.FACULTY_MEMBER);
            FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);

            // act
            var isAdmin = FacadeFactory.GetDomainFacade().IsAdmin(userSignUpViewModel.Email);

            // assert
            isAdmin.Should().BeFalse();
        }

        [Test]
        public void CreateDefaultAdmin()
        {
            // assemble
            // act
            FacadeFactory.GetDomainFacade().CreateDefaultAdmin();

            // assert
            var users = FacadeFactory.GetDomainFacade().FindAllUsers(true);
            users.Count.ShouldBeEquivalentTo(1);
            var userViewModel = users.First();
            userViewModel.FirstName.ShouldBeEquivalentTo(UserConstants.DEFAULT_ADMIN_EMAIL);
            userViewModel.LastName.ShouldBeEquivalentTo(string.Empty);
            userViewModel.Email.ShouldBeEquivalentTo(UserConstants.DEFAULT_ADMIN_EMAIL);
            userViewModel.Roles.Count.ShouldBeEquivalentTo(0);
            userViewModel.IsAdmin.Should().BeTrue();
        }


        [Test]
        public void CreateUser_EmailTaken()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();

            var userSignUpViewModel = new UserSignUpViewModel();
            userSignUpViewModel.FirstName = "Some";
            userSignUpViewModel.LastName = "Dude";
            userSignUpViewModel.Email = "somedude@someawesomeemailprovider.com";
            userSignUpViewModel.Password = "123456";
            userSignUpViewModel.Roles.Add(RoleTestHelper.FACULTY_MEMBER);
            FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);

            // act
            Action act = ()=> FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage(string.Format("Email '{0}' has already been taken.", userSignUpViewModel.Email));
            FacadeFactory.GetDomainFacade().FindAllUsers().Count.ShouldBeEquivalentTo(1);
        }

        [Test]
        public void CreateUser_MultipleRolesSelected()
        {
            // assemble
            FacadeFactory.GetDomainFacade().FindAllUsers().Count.ShouldBeEquivalentTo(0);

            new RoleTestHelper().CreateTestRoles();

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

            // act
            FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);

            // assert
            var users = FacadeFactory.GetDomainFacade().FindAllUsers();
            users.Count.ShouldBeEquivalentTo(1);
            var userViewModel = users.First();
            userViewModel.FirstName.ShouldBeEquivalentTo(userSignUpViewModel.FirstName);
            userViewModel.LastName.ShouldBeEquivalentTo(userSignUpViewModel.LastName);
            userViewModel.Email.ShouldBeEquivalentTo(userSignUpViewModel.Email);
            userViewModel.DisplayName.ShouldBeEquivalentTo("Some Dude");
            userViewModel.Roles.Count.ShouldBeEquivalentTo(5);
            userViewModel.Roles.Should().Contain(RoleTestHelper.FACULTY_MEMBER);
            userViewModel.Roles.Should().Contain(RoleTestHelper.FACULTY_COUNCIL_MEMBER);
            userViewModel.Roles.Should().Contain(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            userViewModel.Roles.Should().Contain(RoleTestHelper.APPC_MEMBER);
            userViewModel.Roles.Should().Contain(RoleTestHelper.GFC_MEMBER);
        }

        [Test]
        public void FindUser()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();

            var userSignUpViewModel = new UserSignUpViewModel();
            userSignUpViewModel.FirstName = "Some";
            userSignUpViewModel.LastName = "Dude";
            userSignUpViewModel.Email = "somedude@someawesomeemailprovider.com";
            userSignUpViewModel.Password = "123456";
            userSignUpViewModel.Roles.Add(RoleTestHelper.FACULTY_MEMBER);

            FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);

            // act
            var user = FacadeFactory.GetDomainFacade().FindUser(userSignUpViewModel.Email);

            // assert
            user.Should().NotBeNull();
            user.FirstName.ShouldBeEquivalentTo(userSignUpViewModel.FirstName);
            user.LastName.ShouldBeEquivalentTo(userSignUpViewModel.LastName);
            user.Email.ShouldBeEquivalentTo(userSignUpViewModel.Email);
            user.Roles.Count.ShouldBeEquivalentTo(1);
            user.Roles.Should().Contain(RoleTestHelper.FACULTY_MEMBER);
        }

        [Test]
        public void CreateUser_NoFirstName()
        {
            // assemble
            var userSignUpViewModel = new UserSignUpViewModel();
            userSignUpViewModel.LastName = "Dude";
            userSignUpViewModel.Email = "somedude@someawesomeemailprovider.com";
            userSignUpViewModel.Password = "123456";

            // act
            Action act = ()=> FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("First name is required.");
            FacadeFactory.GetDomainFacade().FindAllUsers().Count.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void CreateUser_NoLastName()
        {
            // assemble
            var userSignUpViewModel = new UserSignUpViewModel();
            userSignUpViewModel.FirstName = "Dude";
            userSignUpViewModel.Email = "somedude@someawesomeemailprovider.com";
            userSignUpViewModel.Password = "123456";

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Last name is required.");
            FacadeFactory.GetDomainFacade().FindAllUsers().Count.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void CreateUser_NoRoleSelected()
        {
            // assemble
            var userSignUpViewModel = new UserSignUpViewModel();
            userSignUpViewModel.FirstName = "Some";
            userSignUpViewModel.LastName = "Dude";
            userSignUpViewModel.Email = "somedude@someawesomeemailprovider.com";
            userSignUpViewModel.Password = "123456";

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("At least one role is required.");
            FacadeFactory.GetDomainFacade().FindAllUsers().Count.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void CreateUser_PasswordInvalidLength()
        {
            // assemble
            var userSignUpViewModel = new UserSignUpViewModel();
            userSignUpViewModel.FirstName = "Some";
            userSignUpViewModel.LastName = "Dude";
            userSignUpViewModel.Email = "somedude@someawesomeemailprovider.com";
            userSignUpViewModel.Password = "12345";
            userSignUpViewModel.Roles.Add(RoleTestHelper.FACULTY_MEMBER);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Password must be at least 6 characters long.");
            FacadeFactory.GetDomainFacade().FindAllUsers().Count.ShouldBeEquivalentTo(0);
        }
    }
}