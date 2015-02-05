using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Roles;

namespace WorkflowManagementSystem.Models.Users
{
    public class User : IEntity
    {
        public User()
        {
            Roles = new List<Role>();
        }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<Role> Roles { get; set; }
        public int Id { get; set; }

        public void Update(UserSignUpViewModel userSignUpViewModel)
        {
            FirstName = userSignUpViewModel.FirstName;
            LastName = userSignUpViewModel.LastName;
            Email = userSignUpViewModel.Email;
            UpdateRoles(userSignUpViewModel.Roles);

            AssertEmailIsNotTaken();
            AssertFirstNameIsValid();
            AssertLastNameIsValid();
            AssertAtLeastOneRoleIsSelected();
            AssertPasswordIsValid(userSignUpViewModel.Password);
        }

        private void AssertPasswordIsValid(string password)
        {
            if (password.Length < 6)
                throw new WMSException("Password must be at least 6 characters long.");
        }

        private void AssertEmailIsNotTaken()
        {
            if (UserRepository.FindUser(Email) != null)
                throw new WMSException("Email '{0}' has already been taken.", Email);
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