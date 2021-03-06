using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Roles
{
    public class Role : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }
        
        public Role()
        {
            Users = new List<User>();
        }

        public void Update(RoleInputViewModel roleInputViewModel)
        {
            Name = roleInputViewModel.Name;

            AssertNameIsValid();
            AssertRoleDoesNotAlreadyExist();
        }

        private void AssertRoleDoesNotAlreadyExist()
        {
            Role role = RoleRepository.FindRole(Name);
            if (role != null)
                throw new WMSException("The role '{0}' already exists.", Name);
        }

        private void AssertNameIsValid()
        {
            if (Name.IsNullOrWhiteSpace())
                throw new WMSException("Role name is required.");
        }
    }
}