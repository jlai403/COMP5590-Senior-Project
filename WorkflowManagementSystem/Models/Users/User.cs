using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Ajax.Utilities;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Roles;

namespace WorkflowManagementSystem.Models.Users
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<Role> Roles { get; set; }

        public User()
        {
            Roles = new List<Role>();
        }

        public void Update(UserSignUpViewModel userSignUpViewModel)
        {
            FirstName = userSignUpViewModel.FirstName;
            LastName = userSignUpViewModel.LastName;
            Email = userSignUpViewModel.Email;
            UpdateRoles(userSignUpViewModel.Roles);

            AssertFirstNameIsValid();
            AssertLastNameIsValid();
            AssertAtLeastOneRoleIsSelected();
        }

        private void UpdateRoles(List<string> roleNames)
        {
            foreach (var roleName in roleNames)
            {
                var role = RoleRepository.FindRole(roleName);
                Roles.Add(role);
            }
        }

        private void AssertAtLeastOneRoleIsSelected()
        {
            if (Roles.Count < 1)
                throw new WMSException("At least one role is required.");
        }

        private void AssertLastNameIsValid()
        {
            if (LastName.IsNullOrWhiteSpace())
                throw new WMSException("Last name is required.");
        }

        private void AssertFirstNameIsValid()
        {
            if (FirstName.IsNullOrWhiteSpace())
                throw new WMSException("First name is required.");
        }

        public string GetDisplayName()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}