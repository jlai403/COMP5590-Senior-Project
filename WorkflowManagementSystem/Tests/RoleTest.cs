using System;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Roles;

namespace WorkflowManagementSystem.Tests
{
    public class RoleTest : WorkflowManagementSystemTest
    {
        [Test]
        public void CreateRole()
        {
            // assemble
            var roleInputViewModel = new RoleInputViewModel
            {
                Name = "Test Role"
            };

            // act
            FacadeFactory.GetDomainFacade().CreateRole(roleInputViewModel);

            // assert
            var roles = FacadeFactory.GetDomainFacade().FindAllRoles();
            roles.Find(x => x.Name.Equals(roleInputViewModel.Name)).Should().NotBeNull();
        }

        [Test]
        public void CreateRole_RoleExists()
        {
            // assemble
            var roleInputViewModel = new RoleInputViewModel
            {
                Name = "Test Role"
            };
            FacadeFactory.GetDomainFacade().CreateRole(roleInputViewModel);

            // act
            Action act = ()=> FacadeFactory.GetDomainFacade().CreateRole(roleInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage(string.Format("The role '{0}' already exists.", roleInputViewModel.Name));
        }
    }
}