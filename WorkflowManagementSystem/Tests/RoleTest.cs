using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
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
    }
}