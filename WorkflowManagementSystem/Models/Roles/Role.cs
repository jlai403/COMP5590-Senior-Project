using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using MyEntityFramework.Entity;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Roles
{
    public class Role : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual List<User> Users { get; set; }
        
        public Role()
        {
            Users = new List<User>();
        }

        public void Update(RoleInputViewModel roleInputViewModel)
        {
            Name = roleInputViewModel.Name;

            AssertNameIsValid();
        }

        private void AssertNameIsValid()
        {
            if (Name.IsNullOrWhiteSpace())
                throw new WMSException("Role name is required.");
        }
    }
}